using Aliance.Application.DTOs;
using Aliance.Application.DTOs.Auth;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    [Authorize]
    public async Task<IActionResult> GetUsersByChurch([FromQuery] int pageNumber = 1, int pageSize = 5)
    {
        var result = await _service.GetUsersByChurchAsync(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetById")]
    [Authorize]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var result = await _service.GetUserByIdAsync(userId);
        return Ok(result);
    }

    [HttpGet]
    [Route("Ativos")]
    [Authorize]
    public async Task<IActionResult> GetAllActiveUsers()
    {
        var result = await _service.GetAllActiveUsers();

        return Ok(result);
    }

    [HttpGet]
    [Route("Pastores")]
    [Authorize]
    public async Task<IActionResult> GetAllActivePastors()
    {
        var result = await _service.GetAllActivePastors();

        return Ok(result);
    }

    [HttpGet]
    [Route("GetByEmail")]
    [Authorize]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var result = await _service.GetUserByEmailAsync(email);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
    {
        var result = await _service.CreateUserAsync(user);
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
    {
        var result = await _service.UpdateUserAsync(user);
        return Ok(result);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await _service.DeleteUserAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    [Route("ChangePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(string userId, string currentPassword, string newPassword)
    {
        var result = await _service.ChangePasswordAsync(userId, currentPassword, newPassword);
        return Ok(result);
    }

    [HttpPost]
    [Route("DefineFirstPassword")]
    public async Task<IActionResult> DefineFirstPassword(DefinePasswordDTO dto)
    {
        var result = await _service.DefinePassword(dto);
        return Ok(result);
    }

    [HttpPost]
    [Route("AssignRole")]
    [Authorize]
    public async Task<IActionResult> AssignRole(string userId, string role)
    {
        var result = await _service.AssignRoleAsync(userId, role);
        return Ok(result);
    }

    [HttpPost]
    [Route("RemoveRole")]
    [Authorize]
    public async Task<IActionResult> RemoveRole(string userId, string role)
    {
        var result = await _service.RemoveRoleAsync(userId, role);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetRoles")]
    [Authorize]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var result = await _service.GetUserRolesAsync(userId);
        return Ok(result);
    }

    [HttpPatch("deactivate/{id}")]
    [Authorize]
    public async Task<IActionResult> DeactivateUser(string id)
    {
        var result = await _service.DeactivateUser(id);
        return Ok(result);
    }

    [HttpPatch("activate/{id}")]
    [Authorize]
    public async Task<IActionResult> ActivateUser(string id)
    {
        var result = await _service.ActivateUser(id);
        return Ok(result);
    }


}
