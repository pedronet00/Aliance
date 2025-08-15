using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CostCenterController : ControllerBase
{

    private readonly ICostCenterService _service;

    public CostCenterController(ICostCenterService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var centers = await _service.GetAllCenters();
        return Ok(centers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var center = await _service.GetById(id);
        if (center is null)
            return NotFound();
        return Ok(center);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CostCenterDTO costCenterDTO)
    {
        if (costCenterDTO is null)
            return BadRequest("Cost center data is required.");
        var addedCenter = await _service.Add(costCenterDTO);
        return CreatedAtAction(nameof(GetById), new { id = addedCenter.Id }, addedCenter);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CostCenterDTO costCenterDTO)
    {
        if (costCenterDTO is null || costCenterDTO.Id != id)
            return BadRequest("Cost center data is invalid.");

        var updatedCenter = await _service.Update(costCenterDTO);
        return Ok(updatedCenter);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        if (!result)
            return NotFound();
        return NoContent();

    }

}

