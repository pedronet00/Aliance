using Aliance.Application.Interfaces;
using Aliance.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _service;

    public EventController(IEventService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        var result = await _service.GetEvents(pageNumber, pageSize);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpGet("{guid}")]
    public async Task<IActionResult> GetEventByGuid(Guid guid)
    {
        var result = await _service.GetEventByGuid(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] Application.DTOs.EventDTO newEvent)
    {
        var result = await _service.AddEvent(newEvent);
        if (result.Notifications.Any())
            return BadRequest(result);
        return Ok(result.Result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEvent([FromBody] Application.DTOs.EventDTO eventUpdated)
    {
        var result = await _service.UpdateEvent(eventUpdated);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpDelete("{guid}")]
    public async Task<IActionResult> DeleteEvent(Guid guid)
    {
        var result = await _service.DeleteEvent(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpGet]
    [Route("date-range")]
    public async Task<IActionResult> GetEventsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _service.GetEventsByDateRange(startDate, endDate);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpGet]
    [Route("next")]
    public async Task<IActionResult> GetNextEvent()
    {
        var result = await _service.GetNextEvent();
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPatch("{guid:guid}/status/{status}")]
    public async Task<IActionResult> ToggleStatus(Guid guid, string status)
    {
        if (!Enum.TryParse(status, true, out MeetingStatus meetingStatus))
            return BadRequest("Status inválido");

        var result = await _service.ToggleStatus(guid, meetingStatus);

        return Ok(result);
    }

}
