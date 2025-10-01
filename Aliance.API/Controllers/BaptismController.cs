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
public class BaptismController : ControllerBase
{

    private readonly IBaptismService _service;

    public BaptismController(IBaptismService service)
    {
        _service = service;
    }

    [HttpGet]
    
    public async Task<IActionResult> GetAllBaptisms()
    {
        var baptisms = await _service.GetAllBaptisms();

        if (baptisms is null || !baptisms.Any())
            return NotFound("No baptisms found.");

        return Ok(baptisms);
    }

    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetBaptismById(int id)
    {
        var baptism = await _service.GetBaptismById(id);

        if (baptism is null)
            return NotFound($"Baptism with ID {id} not found.");

        return Ok(baptism);
    }

    [HttpPost]
    [ValidateActiveUsers("PastorId", "UserId")]
    
    public async Task<IActionResult> InsertBaptism([FromBody] BaptismDTO baptismDTO)
    {

        if(baptismDTO is null)
            return BadRequest("BaptismDTO cannot be null");

        await _service.InsertBaptism(baptismDTO);

        return Ok(new { message = "Baptism successfully created." });
    }

    [HttpPut]
    [ValidateActiveUsers("PastorId", "UserId")]
    
    public async Task<IActionResult> UpdateBaptism([FromBody] BaptismDTO baptismDTO)
    {
        if (baptismDTO is null)
            return BadRequest("BaptismDTO cannot be null");

        var result = await _service.UpdateBaptism(baptismDTO);

        if (!result)
            return NotFound($"Baptism with ID {baptismDTO.Id} not found or could not be updated.");

        return Ok(new { message = "Baptism successfully updated." });
    }

    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteBaptism(int id)
    {
        var result = await _service.DeleteBaptism(id);

        if (!result)
            return NotFound($"Baptism with ID {id} not found or could not be deleted.");

        return Ok(new { message = "Baptism successfully deleted." });
    }
}
