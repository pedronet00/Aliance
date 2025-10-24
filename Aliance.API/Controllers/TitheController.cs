using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TitheController : ControllerBase
{
    private readonly ITitheService _service;

    public TitheController(ITitheService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 5)
    {
        var result = await _service.GetTithes(pageNumber, pageSize);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var result = await _service.GetTitheByGuid(guid);
        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var result = await _service.GetTithesByUser(userId);
        return Ok(result.Result);
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotal()
    {
        var result = await _service.GetTotalTithes();
        return Ok(result.Result);
    }

    [HttpGet("total/user/{userId:guid}")]
    public async Task<IActionResult> GetTotalByUser(Guid userId)
    {
        var result = await _service.GetTotalTithesByUser(userId);
        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TitheDTO dto)
    {
        var result = await _service.InsertTithe(dto);
        return CreatedAtAction(nameof(GetByGuid), new { guid = result.Result.Guid }, result.Result);
    }

    [HttpPut("{guid:guid}")]
    public async Task<IActionResult> Update([FromBody] TitheDTO dto)
    {
        var result = await _service.UpdateTithe(dto);
        return Ok(result.Result);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _service.DeleteTithe(guid);
        return Ok(result.Result);
    }
}
