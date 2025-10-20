using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _service;

    public ServiceController(IServiceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var result = await _service.GetServices();
        return Ok(result);
    }

    [HttpGet]
    [Route("{serviceGuid}")]
    public async Task<IActionResult> GetServiceByGuid(Guid serviceGuid)
    {
        var result = await _service.GetServiceByGuid(serviceGuid);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddService([FromBody] ServiceDTO serviceDTO)
    {
        var result = await _service.AddService(serviceDTO);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateService([FromBody] ServiceDTO serviceDTO)
    {
        var result = await _service.UpdateService(serviceDTO);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{serviceGuid}")]
    public async Task<IActionResult> DeleteService(Guid serviceGuid)
    {
        var result = await _service.DeleteService(serviceGuid);
        return Ok(result);
    }

    [HttpPatch]
    [Route("{serviceGuid}/status/{status}")]
    public async Task<IActionResult> ToggleStatus(Guid serviceGuid, Domain.Enums.ServiceStatus status)
    {
        var result = await _service.ToggleStatus(serviceGuid, status);
        return Ok(result);
    }
}
