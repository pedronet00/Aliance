using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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

        return Ok(center);
    }

    [HttpPost]
    
    public async Task<IActionResult> Add([FromBody] CostCenterDTO costCenterDTO)
    {
        var addedCenter = await _service.Add(costCenterDTO);

        return Ok(costCenterDTO);
    }

    [HttpPut("{id:int}")]
    
    public async Task<IActionResult> Update(int id, [FromBody] CostCenterDTO costCenterDTO)
    {
        var updatedCenter = await _service.Update(costCenterDTO);
        return Ok(updatedCenter);
    }

    [HttpDelete("{id:int}")]
    
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);

        return NoContent();

    }

    [HttpPatch("deactivate/{id:int}")]
    
    public async Task<IActionResult> Deactivate(int id)
    {
        var result = await _service.Deactivate(id);
        return Ok(result);
    }

    [HttpPatch("activate/{id:int}")]
    
    public async Task<IActionResult> Activate(int id)
    {
        var result = await _service.Activate(id);
        return Ok(result);
    }

}

