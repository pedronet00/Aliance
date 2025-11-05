using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SundaySchoolClassroomController : ControllerBase
{
    private readonly ISundaySchoolClassroomService _service;

    public SundaySchoolClassroomController(ISundaySchoolClassroomService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<IActionResult> Get([FromQuery] int pageNumber, int pageSize)
    {
        var locations = await _service.GetClassrooms(pageNumber, pageSize);

        return Ok(locations);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] SundaySchoolClassroomDTO classroom)
    {
        var result = await _service.Insert(classroom);
        if (result.Notifications.Any())
            return BadRequest(result);
        return Ok(result);
    }
}
