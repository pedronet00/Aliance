using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CalendarController : ControllerBase
{
    private readonly ICalendarActivitiesService _calendarService;

    public CalendarController(ICalendarActivitiesService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCalendarItems()
    {
        var items = await _calendarService.GetCalendarItems();

        return Ok(items);
    }

}
