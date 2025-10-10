using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseViewModel>>> GetAll()
    {
        var Expenses = await _expenseService.GetAllExpenses();
        return Ok(Expenses);
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<ExpenseViewModel>> GetByGuid(Guid guid)
    {
        var result = await _expenseService.GetExpenseByGuid(guid);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<ExpenseViewModel>>> GetByCategory(FinancialExpenseCategory category)
    {
        var result = await _expenseService.GetExpensesByCategory(category);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("monthly-totals/{year:int}")]
    public async Task<ActionResult<IEnumerable<ExpenseMonthlyTotalViewModel>>> GetMonthlyTotals(int year)
    {
        var result = await _expenseService.GetMonthlyTotals(year);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("range-total")]
    public async Task<ActionResult<ExpenseRangeTotalViewModel>> GetTotalByDateRange(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var result = await _expenseService.GetTotalByDateRange(start, end);

        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseViewModel>> Insert([FromBody] ExpenseDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _expenseService.InsertExpense(dto);

        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return CreatedAtAction(nameof(GetByGuid), new { guid = result.Result.Guid }, result.Result);
    }

    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<ExpenseViewModel>> Update(Guid guid, [FromBody] ExpenseDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (dto.Guid != guid)
            return BadRequest("Guid do corpo não corresponde ao da rota.");

        var result = await _expenseService.UpdateExpense(dto);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        var result = await _expenseService.DeleteExpense(guid);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return NoContent();
    }
}
