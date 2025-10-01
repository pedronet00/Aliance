﻿using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Notifications;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserContextService _context;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IUserContextService context, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
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

    public async Task<DomainNotificationsResult<UserViewModel>> CreateUserAsync(UserDTO user)
    {
        var result = new DomainNotificationsResult<UserViewModel>();

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

        // Mapeia apenas para a ViewModel (agora pode usar mapper tranquilo)
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

    public async Task<DomainNotificationsResult<List<UserViewModel>>> GetUsersByChurchAsync()
    {
        var result = new DomainNotificationsResult<List<UserViewModel>>();
        var churchId = _context.GetChurchId();

        var users = _userManager.Users.Where(u => u.ChurchId == churchId).ToList();
        var userViewModels = new List<UserViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user); 
            var userVm = _mapper.Map<UserViewModel>(user);
            userVm.Role = roles.FirstOrDefault(); 
            userVm.Phone = user.PhoneNumber; 
            userViewModels.Add(userVm);
        }

        result.Result = userViewModels;
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
}
