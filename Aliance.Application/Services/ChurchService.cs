using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Aliance.Application.Services;

public class ChurchService : IChurchService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IChurchRepository _churchRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public ChurchService(IUnitOfWork unitOfWork, IChurchRepository churchRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _churchRepository = churchRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<bool> DeleteChurch(int id)
    {
        var church = await _churchRepository.GetChurchById(id);

        await _churchRepository.DeleteChurch(id);

        await _unitOfWork.Commit();

        return true;
    }

    public async Task AtualizarPagamentoRecebidoAsync(string customerId, DateTime dataRecebimento, DateTime? proximaCobranca, decimal valorPago)
    {
        var church = await _churchRepository.GetChurchByAsaasCustomerId(customerId);
        if (church == null)
            throw new InvalidOperationException($"Igreja não encontrada para o customerId: {customerId}");

        church.PaymentStatus = Domain.Enums.PaymentStatus.Active;
        church.LastPaymentDate = dataRecebimento;
        church.NextDueDate = proximaCobranca;
        church.Status = true;

        _churchRepository.UpdateChurch(church);

        await _unitOfWork.Commit();
    }

    public async Task AtualizarPagamentoAtrasadoAsync(string customerId, DateTime dataVencimento)
    {
        var church = await _churchRepository.GetChurchByAsaasCustomerId(customerId);
        if (church == null)
            throw new InvalidOperationException($"Igreja não encontrada para o customerId: {customerId}");

        church.PaymentStatus = Domain.Enums.PaymentStatus.Expired;
        church.NextDueDate = dataVencimento;
        church.Status = false;

        _churchRepository.UpdateChurch(church);

        await _unitOfWork.Commit();
    }

    public async Task<IEnumerable<ChurchViewModel>> GetAllChurches()
    {
        var churches = await _churchRepository.GetChurches();

        var churchesViewModel = _mapper.Map<IEnumerable<ChurchViewModel>>(churches);

        return churchesViewModel;
    }

    public async Task<ChurchViewModel> GetChurchByAsaasCustomerId(string asaasCustomerId)
    {
        var church = await _churchRepository.GetChurchByAsaasCustomerId(asaasCustomerId);

        if (church is null)
            throw new ArgumentNullException(nameof(church), "Igreja não encontrada.");

        var churchViewModel = _mapper.Map<ChurchViewModel>(church);

        return churchViewModel;
    }

    public async Task<ChurchViewModel> GetChurchById(int id)
    {
        var church = await _churchRepository.GetChurchById(id);

        if (church is null)
            throw new ArgumentNullException(nameof(church), "Church not found");

        var churchViewModel = _mapper.Map<ChurchViewModel>(church);

        return churchViewModel;
    }

    public async Task<ChurchViewModel> InsertChurch(ChurchDTO church)
    {
        var churchEntity = _mapper.Map<Church>(church);
        
        await _churchRepository.InsertChurch(churchEntity);

        await _unitOfWork.Commit();

        var churchViewModel = _mapper.Map<ChurchViewModel>(churchEntity);

        return churchViewModel;
    }

    public async Task<bool> UpdateChurch(ChurchDTO church)
    {
        var churchEntity = _mapper.Map<Church>(church);

        _churchRepository.UpdateChurch(churchEntity);

        await _unitOfWork.Commit();

        return true;
    }

    public async Task<string> GetChurchesFirstUser(string churchAsaasId)
    {
        var email = await _userManager.Users
            .Where(u => u.Church.AsaasCustomerId == churchAsaasId)
            .Select(u => u.Email)
            .FirstOrDefaultAsync();

        return email!;
    }
}
