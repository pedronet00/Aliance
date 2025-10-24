using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Application.ViewModels;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Aliance.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<IncomeViewModel>>> GetAll([FromQuery] int pageNumber = 1, int pageSize = 5)
    {
        var incomes = await _incomeService.GetAllIncomes(pageNumber, pageSize);
        return Ok(incomes);
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<IncomeViewModel>> GetByGuid(Guid guid)
    {
        var result = await _incomeService.GetIncomeByGuid(guid);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<IncomeViewModel>>> GetByCategory(FinancialIncomingCategory category)
    {
        var result = await _incomeService.GetIncomesByCategory(category);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("monthly-totals/{year:int}")]
    public async Task<ActionResult<IEnumerable<IncomeMonthlyTotalViewModel>>> GetMonthlyTotals(int year)
    {
        var result = await _incomeService.GetMonthlyTotals(year);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpGet("range-total")]
    public async Task<ActionResult<IncomeRangeTotalViewModel>> GetTotalByDateRange(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var result = await _incomeService.GetTotalByDateRange(start, end);

        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

    [HttpPost]
    public async Task<ActionResult<IncomeViewModel>> Insert([FromBody] IncomeDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _incomeService.InsertIncome(dto);

        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return CreatedAtAction(nameof(GetByGuid), new { guid = result.Result.Guid }, result.Result);
    }

    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<IncomeViewModel>> Update(Guid guid, [FromBody] IncomeDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (dto.Guid != guid)
            return BadRequest("Guid do corpo não corresponde ao da rota.");

        var result = await _incomeService.UpdateIncome(dto);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return Ok(result.Result);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        var result = await _incomeService.DeleteIncome(guid);

        if (result.HasNotifications)
            return NotFound(result.Notifications);

        return NoContent();
    }
}
