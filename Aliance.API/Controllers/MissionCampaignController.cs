using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MissionCampaignController : ControllerBase
{
    private readonly IMissionCampaignService _service;

    public MissionCampaignController(IMissionCampaignService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 5)
    {
        var result = await _service.GetAllAsync(pageNumber, pageSize);

        return Ok(result.Result);
    }

    [HttpGet("{guid:guid}")]
    
    public async Task<IActionResult> GetById(Guid guid)
        {
        var campaign = await _service.GetByGuidAsync(guid);

        if (campaign is null)
            return NotFound();

        return Ok(campaign);
    }

    [HttpPost]
    
    public async Task<IActionResult> Add([FromBody] MissionCampaignDTO missionCampaign)
    {
        if (missionCampaign is null)
            return BadRequest("Mission campaign data is required.");
        var addedCampaign = await _service.AddAsync(missionCampaign);
        return Ok(missionCampaign);
    }


    [HttpPut]
    
    public async Task<IActionResult> Update([FromBody] MissionCampaignDTO missionCampaign)
    {
        var updatedCampaign = await _service.UpdateAsync(missionCampaign);
        return Ok(updatedCampaign);
    }

    [HttpDelete("{guid:guid}")]
    
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _service.DeleteAsync(guid);

        return NoContent();
    }
}
