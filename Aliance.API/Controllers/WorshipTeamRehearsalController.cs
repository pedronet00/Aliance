using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WorshipTeamRehearsalController : ControllerBase
{
    private readonly IWorshipTeamRehearsalService _service;

    public WorshipTeamRehearsalController(IWorshipTeamRehearsalService service)
    {
        _service = service;
    }

    [HttpGet("team/{guid:guid}")]
    public async Task<IActionResult> GetAll(Guid guid)
    {
        var result = await _service.GetWorshipTeamRehearsals(guid);

        return Ok(result.Result);
    }

    [HttpGet("rehearsal/{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var result = await _service.GetWorshipTeamRehearsalByGuid(guid);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("team/{guid:guid}/next")]
    public async Task<IActionResult> GetNext(Guid guid)
    {
        var result = await _service.GetNextWorshipTeamRehearsal(guid);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorshipTeamRehearsalDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.InsertWorshipTeamRehearsal(dto);

        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return CreatedAtAction(nameof(GetByGuid), new { guid = result.Result.Guid }, result.Result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] WorshipTeamRehearsalDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.UpdateWorshipTeamRehearsal(dto);

        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _service.DeleteWorshipTeamRehearsal(guid);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpPatch("{guid:guid}/status/{status}")]
    public async Task<IActionResult> ToggleStatus(Guid guid, MeetingStatus status)
    {
        var result = await _service.ToggleStatus(guid, status);

        if (result.HasNotifications)
            return NotFound(result);

        return Ok(result);
    }
}
