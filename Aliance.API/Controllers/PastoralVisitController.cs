using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PastoralVisitController : ControllerBase
{
    private readonly IPastoralVisitService _service;

    public PastoralVisitController(IPastoralVisitService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllVisits();
        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var result = await _service.GetPastoralVisitByGuid(guid);


        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PastoralVisitDTO dto)
    {
        var result = await _service.AddPastoralVisit(dto);

        return CreatedAtAction(nameof(GetByGuid), new { guid = result.Result.Guid }, result.Result);
    }

    [HttpPut("{guid:guid}")]
    public async Task<IActionResult> Update([FromBody] PastoralVisitDTO dto)
    {
        var result = await _service.UpdatePastoralVisit(dto);

        return Ok(result.Result);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _service.DeletePastoralVisit(guid);

        return Ok(result.Result);
    }

    [HttpPatch("{guid:guid}/status/{status}")]
    public async Task<IActionResult> ToggleStatus(Guid guid, string status)
    {
        if (!Enum.TryParse(status, true, out Aliance.Domain.Enums.MeetingStatus meetingStatus))
            return BadRequest("Status inválido");

        var result = await _service.TogglePastoralVisitStatus(guid, meetingStatus);

        return Ok(result.Result);
    }
}
