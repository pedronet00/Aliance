using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AutomaticAccountsController : ControllerBase
{
    private readonly IAutomaticAccountsService _service;

    public AutomaticAccountsController(IAutomaticAccountsService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 5)
    {
        var result = await _service.GetAll(pageNumber, pageSize);

        return result.HasNotifications
            ? BadRequest(result.Notifications)
            : Ok(result.Result);
    }

    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var result = await _service.GetByGuid(guid);

        return result.HasNotifications
            ? NotFound(result.Notifications)
            : Ok(result.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] AutomaticAccountsDTO account)
    {
        if (account == null)
            return BadRequest("Dados inválidos.");

        var result = await _service.Insert(account);

        return result.HasNotifications
            ? BadRequest(result.Notifications)
            : Ok(result.Result);
    }

    [HttpPut("{guid:guid}")]
    public async Task<IActionResult> Update(Guid guid, [FromBody] AutomaticAccountsDTO account)
    {
        if (account == null)
            return BadRequest("Dados inválidos.");

        account.Guid = guid; // garante atualização do correto

        var result = await _service.Update(account);

        return result.HasNotifications
            ? BadRequest(result.Notifications)
            : Ok(result.Result);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _service.Delete(guid);

        return result.HasNotifications
            ? NotFound(result.Notifications)
            : Ok(result.Result);
    }

    // Rotina automática manual (opcional exposta via API)
    [HttpPost("routine")]
    public async Task<IActionResult> RunRoutine()
    {
        var result = await _service.Routine();

        return result.HasNotifications
            ? BadRequest(result.Notifications)
            : Ok(result.Result);
    }
}
