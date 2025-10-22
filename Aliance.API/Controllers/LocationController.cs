using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LocationController : ControllerBase
{
    private readonly ILocationService _service;

    public LocationController(ILocationService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<IActionResult> Get([FromQuery] int pageNumber, int pageSize)
    {
        var locations = await _service.GetLocations(pageNumber, pageSize);

        return Ok(locations);
    }

    [HttpPost]
    public async Task<IActionResult> InsertLocation([FromBody] LocationDTO location)
    {
        var result = await _service.Insert(location);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }
}
