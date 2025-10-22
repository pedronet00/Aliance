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

    [HttpGet("paged")]
    
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        var centers = await _service.GetAllCenters(pageNumber, pageSize);
        return Ok(centers);
    }

    [HttpGet("{guid:guid}")]
    
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var center = await _service.GetByGuid(guid);

        return Ok(center);
    }

    [HttpPost]
    
    public async Task<IActionResult> Add([FromBody] CostCenterDTO costCenterDTO)
    {
        var addedCenter = await _service.Add(costCenterDTO);

        return Ok(costCenterDTO);
    }

    [HttpPut("{guid:guid}")]
    
    public async Task<IActionResult> Update([FromBody] CostCenterDTO costCenterDTO)
    {
        var updatedCenter = await _service.Update(costCenterDTO);
        return Ok(updatedCenter);
    }

    [HttpDelete("{guid:guid}")]

    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _service.Delete(guid);

        return NoContent();

    }

    [HttpPatch("deactivate/{guid:guid}")]
    
    public async Task<IActionResult> Deactivate(Guid guid)
    {
        var result = await _service.Deactivate(guid);
        return Ok(result);
    }

    [HttpPatch("activate/{guid:guid}")]
    
    public async Task<IActionResult> Activate(Guid guid)
    {
        var result = await _service.Activate(guid);
        return Ok(result);
    }

}

