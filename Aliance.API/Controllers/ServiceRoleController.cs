using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ServiceRoleController : ControllerBase
{
    private readonly IServiceRoleService _serviceRoleService;

    public ServiceRoleController(IServiceRoleService serviceRoleService)
    {
        _serviceRoleService = serviceRoleService;
    }

    [HttpGet("service/{serviceGuid}")]
    public async Task<IActionResult> GetServiceRoles(Guid serviceGuid)
    {
        var result = await _serviceRoleService.List(serviceGuid);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<IActionResult> AddServiceRole([FromBody] Aliance.Application.DTOs.ServiceRoleDTO serviceRoleDTO)
    {
        var result = await _serviceRoleService.Add(serviceRoleDTO);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpDelete("{serviceRoleGuid}")]
    public async Task<IActionResult> DeleteServiceRole(Guid serviceRoleGuid)
    {
        var result = await _serviceRoleService.Delete(serviceRoleGuid);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }
}
