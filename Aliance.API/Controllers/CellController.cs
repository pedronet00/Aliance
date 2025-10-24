using Aliance.API.Filters;
using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CellController : ControllerBase
{

    private readonly ICellService _service;

    public CellController(ICellService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    
    public async Task<IActionResult> GetAllCells([FromQuery] int pageNumber = 1, int pageSize = 5)
    {
        var cells = await _service.GetAllCells(pageNumber, pageSize);
        return Ok(cells);
    }

    [HttpGet]
    [Route("{guid:guid}")]
    
    public async Task<IActionResult> GetCellById(Guid guid)
    {
        var cell = await _service.GetCellById(guid);
        if (cell == null)
            return NotFound();
        return Ok(cell);
    }

    [HttpPost]
    [ValidateActiveUsers("LeaderID")]
    
    public async Task<IActionResult> AddCell([FromBody] CellDTO cell)
    {
        if (cell == null)
            return BadRequest("Cell data is required");
        
        var createdCell = await _service.AddCell(cell);
        return Ok(createdCell);
    }

    [HttpPut]
    [Route("{guid:guid}")]
    [ValidateActiveUsers("LeaderID")]
    public async Task<IActionResult> UpdateCell(Guid guid, [FromBody] CellDTO cell)
    {
        cell.Guid = guid;

        var result = await _service.UpdateCell(cell);

        if (result.Notifications.Any())
            return BadRequest(result.Notifications);

        return NoContent();
    }


    [HttpDelete]
    [Route("{guid:guid}")]
    
    public async Task<IActionResult> DeleteCell(Guid guid)
    {

        var deleted = await _service.DeleteCell(guid);

        return NoContent();
    }

}
