using Aliance.Application.DTOs.Auth;
using Aliance.Application.Interfaces.Auth;
using Aliance.Application.ViewModel.Auth;
using Aliance.Application.Integration.Asaas;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Identity;
using Aliance.Domain.Enums;

namespace Aliance.Application.Services.Auth;

public class RegisterService : IRegisterService
{
    private readonly IChurchRepository _churchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAsaasService _asaasService;

    public RegisterService(
        IChurchRepository churchRepository,
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IAsaasService asaasService)
    {
        _churchRepository = churchRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _asaasService = asaasService;
    }

    private string GenerateRandomPassword(int length = 32)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public async Task<DomainNotificationsResult<NewClientViewModel>> Register(NewClientDTO newClientDTO)
    {
        var result = new DomainNotificationsResult<NewClientViewModel>();
        string generatedPassword = GenerateRandomPassword();

        try
        {
            //Validar se igreja já existe
            var churchExists = await _churchRepository.ChurchAlreadyExists(newClientDTO.ChurchCNPJ);
            
            if (churchExists)
            {
                result.Notifications.Add("Já existe uma igreja cadastrada com este CNPJ.");
                return result;
            }

            // 1 | Criar igreja
            var church = new Church(
                newClientDTO.ChurchName,
                newClientDTO.ChurchEmail,
                newClientDTO.ChurchPhone,
                newClientDTO.ChurchAddress,
                newClientDTO.ChurchCity,
                newClientDTO.ChurchState,
                newClientDTO.ChurchCountry,
                newClientDTO.ChurchCNPJ,
                PaymentStatus.Pending,
                null, // AsaasCustomerId
                null, // SubscriptionId
                null, // NextDueDate
                null, // LastPaymentDate
                newClientDTO.PaymentMethod,
                59.90m,
                null, // DateActivated
                null, // DateCanceled
                true  // Status
            );

            await _churchRepository.InsertChurch(church);
            await _unitOfWork.Commit(); // ChurchId disponível

            //Validar se já existe usuário com email
            var existingUser = await _userManager.FindByEmailAsync(newClientDTO.UserEmail);

            if (existingUser != null)
            {
                // Remover igreja criada
                await _churchRepository.DeleteChurch(church.Id);
                result.Notifications.Add("Já existe um usuário cadastrado com este email.");
                return result;
            }

            // 2️ | Criar usuário
            var user = new ApplicationUser
            {
                UserName = newClientDTO.UserName,
                Email = newClientDTO.UserEmail,
                PhoneNumber = newClientDTO.UserPhone,
                ChurchId = church.Id,
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var userCreationResult = await _userManager.CreateAsync(user, generatedPassword);
            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                    result.Notifications.Add(error.Description);
                return result;
            }

            // 3️ | Criar cliente no Asaas
            string asaasCustomerId;
            try
            {
                asaasCustomerId = await _asaasService.CreateCustomerAsync(church, user);
            }
            catch (Exception ex)
            {
                // Remove usuário se falhar no Asaas
                await _userManager.DeleteAsync(user);
                await _churchRepository.DeleteChurch(church.Id);
                result.Notifications.Add($"Falha ao criar cliente no Asaas: {ex.Message}");
                return result;
            }

            church.AsaasCustomerId = asaasCustomerId;
            _churchRepository.UpdateChurch(church);

            await _unitOfWork.Commit();

            // 4️ | Criar checkout no Asaas (gerar o link de pagamento)
            string checkoutUrl;
            try
            {
                checkoutUrl = await _asaasService.CreateCheckoutAsync(
                    asaasCustomerId,
                    newClientDTO.Plan,
                    newClientDTO.PaymentMethod
                );
            }
            catch (Exception ex)
            {
                // Se falhar na criação do checkout, remove o cliente do Asaas e o usuário
                
                await _churchRepository.DeleteChurch(church.Id);
                await _asaasService.DeleteCustomerAsync(asaasCustomerId);
                await _userManager.DeleteAsync(user);
                result.Notifications.Add("Falha ao criar o checkout no Asaas: " + ex.Message);
                return result;
            }

            result.Result = new NewClientViewModel
            {
                Plan = newClientDTO.Plan,
                PaymentMethod = newClientDTO.PaymentMethod,
                AsaasCustomerId = asaasCustomerId,
                CheckoutUrl = checkoutUrl
            };

            return result;
        }
        catch (Exception ex)
        {
            result.Notifications.Add("Erro ao registrar novo cliente: " + ex.Message);
            return result;
        }
    }


}
