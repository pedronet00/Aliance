using Aliance.Application.DTOs;
using Aliance.Application.DTOs.Auth;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aliance.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserContextService _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IUserContextService context, IMapper mapper, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
        _config = config;
    }

    public async Task<DomainNotificationsResult<bool>> ActivateUser(string userId)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        user.Status = true;

        var updateResult = await _userManager.UpdateAsync(user);

        if (updateResult != null)
        {
            foreach (var error in updateResult.Errors)
            {
                result.Notifications.Add(error.Description);
            }
            return result;
        }
        await _unitOfWork.Commit();

        result.Result = true;

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> AssignRoleAsync(string userId, string role)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var addRoleResult = await _userManager.AddToRoleAsync(user, role);

        if (!addRoleResult.Succeeded)
        {
            foreach (var error in addRoleResult.Errors)
            {
                result.Notifications.Add(error.Description);
            }
            return result;
        }

        await _unitOfWork.Commit();
        result.Result = true;

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                result.Notifications.Add(error.Description);
            }
            return result;
        }

        await _unitOfWork.Commit();

        result.Result = true;

        return result;
    }

    public async Task<DomainNotificationsResult<int>> CountUsers()
    {
        var result = new DomainNotificationsResult<int>();
        var churchId = _context.GetChurchId();

        var totalUsers = await _userManager.Users
            .Where(u => u.ChurchId == churchId)
            .CountAsync();

        result.Result = totalUsers;

        return result;
    }

    public async Task<DomainNotificationsResult<UserViewModel>> CreateUserAsync(UserDTO user)
    {
        var result = new DomainNotificationsResult<UserViewModel>();
        var apiKey = _config["SendGrid:ApiKey"];

        // Verifica se email já existe
        var existingUser = await _userManager.FindByEmailAsync(user.Email!);
        if (existingUser != null)
        {
            result.Notifications.Add("Já existe um usuário com esse email.");
            return result;
        }

        // Monta manualmente o ApplicationUser
        var newUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(), // garante que nunca será null
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            ChurchId = user.ChurchId,
            Status = user.Status
        };

        // Cria usuário com Identity
        var createResult = await _userManager.CreateAsync(newUser, user.Password!);

        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
                result.Notifications.Add(error.Description);

            return result;
        }

        // Adiciona role se existir
        if (!string.IsNullOrEmpty(user.Role))
        {
            var roleResult = await _userManager.AddToRoleAsync(newUser, user.Role);
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                    result.Notifications.Add(error.Description);

                return result;
            }
        }

        await _unitOfWork.Commit();

        string template = File.ReadAllText("Mailing/Templates/RedefinePassword.cshtml");
        template = template.Replace("{{Id}}", newUser.Id);

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("suporte@aliance.app.br", "Aliance | ERP para igrejas");
        var subject = "Defina sua senha";
        var to = new EmailAddress(newUser.Email);
        var plainTextContent = "Clique aqui para definir sua senha: <link>";
        var htmlContent = template;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        var userViewModel = _mapper.Map<UserViewModel>(newUser);
        result.Result = userViewModel;

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeactivateUser(string userId)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        user.Status = false;

        var updateResult = await _userManager.UpdateAsync(user);

        if (updateResult != null)
        {
            foreach (var error in updateResult.Errors)
            {
                result.Notifications.Add(error.Description);
            }
            return result;
        }
        await _unitOfWork.Commit();

        result.Result = true;

        return result;
    }

    public async Task<DomainNotificationsResult<bool>> DeleteUserAsync(string userId)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var deleteResult = await _userManager.DeleteAsync(user);

        if (!deleteResult.Succeeded)
        {
            foreach (var error in deleteResult.Errors)
            {
                result.Notifications.Add(error.Description);
            }
            return result;
        }

        await _unitOfWork.Commit();

        result.Result = true;
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<UserViewModel>>> GetAllActivePastors()
    {
        var result = new DomainNotificationsResult<IEnumerable<UserViewModel>>();

        var churchId = _context.GetChurchId();

        var users = await _userManager.Users
            .Where(u => u.Status && u.ChurchId == churchId)
            .AsNoTracking()
            .ToListAsync();

        var pastors = new List<ApplicationUser>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Pastor"))
                pastors.Add(user);
        }

        result.Result = _mapper.Map<IEnumerable<UserViewModel>>(pastors);
        return result;
    }

    public async Task<DomainNotificationsResult<IEnumerable<UserViewModel>>> GetAllActiveUsers()
    {
        var result = new DomainNotificationsResult<IEnumerable<UserViewModel>>();

        var churchId = _context.GetChurchId();

        var users = await _userManager.Users
            .Where(u => u.Status == true && u.ChurchId == churchId)
            .AsNoTracking()
            .ToListAsync();

        var userViewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

        result.Result = userViewModel;

        return result;
    }

    public async Task<DomainNotificationsResult<UserViewModel>> GetUserByEmailAsync(string email)
    {
        var result = new DomainNotificationsResult<UserViewModel>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var userViewModel = _mapper.Map<UserViewModel>(user);

        result.Result = userViewModel;

        return result;
    }

    public async Task<DomainNotificationsResult<UserViewModel>> GetUserByIdAsync(string userId)
    {
        var result = new DomainNotificationsResult<UserViewModel>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var userViewModel = _mapper.Map<UserViewModel>(user);

        result.Result = userViewModel;

        return result;
    }

    public async Task<DomainNotificationsResult<List<string>>> GetUserRolesAsync(string userId)
    {
        var result = new DomainNotificationsResult<List<string>>();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var roles = await _userManager.GetRolesAsync(user);

        result.Result = roles.ToList();

        return result;
    }

    public async Task<DomainNotificationsResult<PagedResult<UserViewModel>>> GetUsersByChurchAsync(int pageNumber, int pageSize)
    {
        var result = new DomainNotificationsResult<PagedResult<UserViewModel>>();
        var churchId = _context.GetChurchId();

        var query = _userManager.Users.Where(u => u.ChurchId == churchId);
        var totalCount = await query.CountAsync();

        var users = await query
        .OrderBy(u => u.UserName) // opcional: ordena para garantir consistência
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        var userViewModels = new List<UserViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = _mapper.Map<UserViewModel>(user);
            userVm.Role = roles.FirstOrDefault();
            userVm.Phone = user.PhoneNumber;
            userViewModels.Add(userVm);
        }

        var pagedResult = new PagedResult<UserViewModel>(
        userViewModels,
        totalCount,
        pageNumber,
        pageSize
    );

        result.Result = pagedResult;
        return result;
    }

    public async Task<DomainNotificationsResult<bool>> RemoveRoleAsync(string userId, string role)
    {
        var result = new DomainNotificationsResult<bool>();
        var churchId = _context.GetChurchId();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role);

        if (!removeRoleResult.Succeeded)
        {
            foreach (var error in removeRoleResult.Errors)
            {
                result.Notifications.Add(error.Description);
            }
            return result;
        }

        await _unitOfWork.Commit();

        result.Result = true;

        return result;
    }

    public async Task<DomainNotificationsResult<UserViewModel>> UpdateUserAsync(UserDTO user)
    {
        var result = new DomainNotificationsResult<UserViewModel>();
        var churchId = _context.GetChurchId();

        var existingUser = await _userManager.FindByIdAsync(user.Id!);

        if (existingUser == null || existingUser.ChurchId != churchId)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        // Check if the email is being changed and if the new email already exists
        if (existingUser.Email != user.Email)
        {
            var emailUser = await _userManager.FindByEmailAsync(user.Email!);
            if (emailUser != null && emailUser.Id != existingUser.Id)
            {
                result.Notifications.Add("Já existe um usuário com esse email.");
                return result;
            }
        }

        existingUser.UserName = user.UserName;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.Phone;
        existingUser.Status = user.Status;
        existingUser.ChurchId = user.ChurchId;


        if (!string.IsNullOrEmpty(user.Role))
        {
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (!userRoles.Contains(user.Role))
            {
                var removeRolesResult = await _userManager.RemoveFromRolesAsync(existingUser, userRoles);
                if (!removeRolesResult.Succeeded)
                {
                    foreach (var error in removeRolesResult.Errors)
                    {
                        result.Notifications.Add(error.Description);
                    }
                    return result;
                }
                var addRoleResult = await _userManager.AddToRoleAsync(existingUser, user.Role);
                if (!addRoleResult.Succeeded)
                {
                    foreach (var error in addRoleResult.Errors)
                    {
                        result.Notifications.Add(error.Description);
                    }
                    return result;
                }
            }
        }

        await _unitOfWork.Commit();

        var userViewModel = _mapper.Map<UserViewModel>(existingUser);

        result.Result = userViewModel;

        return result;
    }

    public async Task<string> GeneratePasswordResetUrl(string churchAsaasId)
    {
        var user = await _userManager.Users
            .Where(u => u.Church.AsaasCustomerId == churchAsaasId)
            .FirstOrDefaultAsync();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user!);

        var urlEncodedEmail = Uri.EscapeDataString(user.Email);
        var urlEncodedToken = Uri.EscapeDataString(token);

        var callbackUrl = $"http://localhost:5173/definir-senha?email={urlEncodedEmail}&token={urlEncodedToken}";

        return callbackUrl;
    }

    public async Task<DomainNotificationsResult<bool>> DefinePassword(DefinePasswordDTO dto)
    {
        var result = new DomainNotificationsResult<bool>();
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
        {
            result.Notifications.Add("Usuário não encontrado.");
            return result;
        }

        var resetPassword = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

        if (!resetPassword.Succeeded)
            throw new Exception("Erro ao redefinir senha do usuário.");

        // Opcional: confirmar email após definir senha
        if (!user.EmailConfirmed)
        {
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
        }

        result.Result = true;

        return result;
    }

}
