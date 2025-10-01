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

    [HttpGet]
    
    public async Task<IActionResult> GetAllCells()
    {
        var cells = await _service.GetAllCells();
        return Ok(cells);
    }

    [HttpGet]
    [Route("{id:int}")]
    
    public async Task<IActionResult> GetCellById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid cell ID");
        var cell = await _service.GetCellById(id);
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
    [Route("{id:int}")]
    [ValidateActiveUsers("LeaderID")]
    
    public async Task<IActionResult> UpdateCell(int id, [FromBody] CellDTO cell)
    {
        if (id <= 0 || cell == null || id != cell.Id)
            return BadRequest("Invalid cell data");

        var updated = await _service.UpdateCell(cell);


        return NoContent();
    }

    [HttpDelete]
    [Route("{id:int}")]
    
    public async Task<IActionResult> DeleteCell(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid cell ID");

        var deleted = await _service.DeleteCell(id);

        return NoContent();
    }

}
