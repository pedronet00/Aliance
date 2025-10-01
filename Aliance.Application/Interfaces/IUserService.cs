﻿using Aliance.Application.DTOs;
using Aliance.Application.ViewModel;
using Aliance.Domain.Entities;
using Aliance.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.Interfaces;

public interface IUserService
{
    Task<DomainNotificationsResult<UserViewModel>> CreateUserAsync(UserDTO user);

    Task<DomainNotificationsResult<UserViewModel>> UpdateUserAsync(UserDTO user);

    Task<DomainNotificationsResult<bool>> DeleteUserAsync(string userId);

    Task<DomainNotificationsResult<List<UserViewModel>>> GetUsersByChurchAsync();

    Task<DomainNotificationsResult<UserViewModel>> GetUserByEmailAsync(string email);

    Task<DomainNotificationsResult<UserViewModel>> GetUserByIdAsync(string userId);

    Task<DomainNotificationsResult<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

    Task<DomainNotificationsResult<bool>> AssignRoleAsync(string userId, string role);

    Task<DomainNotificationsResult<bool>> RemoveRoleAsync(string userId, string role);

    Task<DomainNotificationsResult<List<string>>> GetUserRolesAsync(string userId);

    Task<DomainNotificationsResult<bool>> DeactivateUser(string userId);

    Task<DomainNotificationsResult<bool>> ActivateUser(string userId);

}
