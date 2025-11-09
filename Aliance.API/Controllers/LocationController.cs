using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Retorna todas as localizações paginadas.
    /// </summary>
    [HttpGet("paged")]
    public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var locations = await _service.GetLocations(pageNumber, pageSize);
        if (locations.Notifications.Any())
            return BadRequest(locations.Notifications);

        return Ok(locations);
    }

    /// <summary>
    /// Retorna uma localização pelo seu Guid.
    /// </summary>
    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var result = await _service.GetByGuid(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);

        if (result.Result is null)
            return NotFound("Localização não encontrada.");

        return Ok(result.Result);
    }

    /// <summary>
    /// Cria uma nova localização.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> InsertLocation([FromBody] LocationDTO location)
    {
        var result = await _service.Insert(location);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    /// <summary>
    /// Atualiza uma localização existente.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLocation([FromBody] LocationDTO location)
    {
        var result = await _service.UpdateLocation(location);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    /// <summary>
    /// Alterna o status (ativo/inativo) de uma localização.
    /// </summary>
    [HttpPatch("{guid:guid}/status")]
    public async Task<IActionResult> ToggleStatus(Guid guid)
    {
        var result = await _service.ToggleStatus(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }
}
