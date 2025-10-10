using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
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

    [HttpGet]
    
    public async Task<IActionResult> GetAll()
    {
        var campaigns = await _service.GetAllAsync();
        return Ok(campaigns);
    }

    [HttpGet("{id:int}")]
    
    public async Task<IActionResult> GetById(int id)
        {
        var campaign = await _service.GetByIdAsync(id);
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
        return CreatedAtAction(nameof(GetById), new { id = addedCampaign.Id }, addedCampaign);
    }


    [HttpPut("{id:int}")]
    
    public async Task<IActionResult> Update(int id, [FromBody] MissionCampaignDTO missionCampaign)
    {
        if (missionCampaign is null || missionCampaign.Id != id)
            return BadRequest("Mission campaign data is invalid.");
        
        var updatedCampaign = await _service.UpdateAsync(missionCampaign);
        return Ok(updatedCampaign);
    }

    [HttpDelete("{id:int}")]
    
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}
