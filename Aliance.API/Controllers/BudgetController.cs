using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _service;

    public BudgetController(IBudgetService service)
    {
        _service = service;
    }

    [HttpGet]
    
    public async Task<IActionResult> GetAllBudgetsAsync()
    {
        var budgets = await _service.GetAllBudgetsAsync();
        return Ok(budgets);
    }

    [HttpGet("{guid}")]
    
    public async Task<IActionResult> GetBudgetByIdAsync(Guid guid)
    {
        var result = await _service.GetBudgetByIdAsync(guid);

        return Ok(result);
    }

    [HttpPost]
    
    public async Task<IActionResult> AddBudgetAsync([FromBody] BudgetDTO budget)
    {
        var result = await _service.AddBudgetAsync(budget);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPut]
    
    public async Task<IActionResult> UpdateBudgetAsync([FromBody] BudgetDTO budget)
    {
        var result = await _service.UpdateBudgetAsync(budget);

        if (result.Notifications.Any())
            return BadRequest(result.Notifications);

        return Ok(result);
    }

    [HttpDelete("{guid}")]
    
    public async Task<IActionResult> DeleteBudgetAsync(Guid guid)
    {
        var result = await _service.DeleteBudgetAsync(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPatch("{guid}/approve")]
    
    public async Task<IActionResult> ApproveBudget(Guid guid)
    {
        var result = await _service.ApproveBudget(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPatch("{guid}/reject")]
    
    public async Task<IActionResult> RejectBudget(Guid guid)
    {
        var result = await _service.RejectBudget(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPatch("{guid}/contest")]
    
    public async Task<IActionResult> ContestBudget(Guid guid)
    {
        var result = await _service.ContestBudget(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }

    [HttpPatch("{guid}/end")]
    
    public async Task<IActionResult> EndBudget(Guid guid)
    {
        var result = await _service.EndBudget(guid);
        if (result.Notifications.Any())
            return BadRequest(result.Notifications);
        return Ok(result.Result);
    }
}
