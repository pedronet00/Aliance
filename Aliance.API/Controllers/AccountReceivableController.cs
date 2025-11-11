using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountReceivableController : ControllerBase
{
    private readonly IAccountReceivableService _service;

    public AccountReceivableController(IAccountReceivableService service)
    {
        _service = service;
    }

    [HttpGet("paged")]

    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 5)
    {
        var accountReceivables = await _service.GetAllAsync(pageNumber, pageSize);

        return Ok(accountReceivables);
    }

    [HttpGet("deployyyyy")]

    public async Task<IActionResult> Deploy([FromQuery] int pageNumber = 1, int pageSize = 5)
    {
        var accountReceivables = await _service.GetAllAsync(pageNumber, pageSize);

        return Ok(accountReceivables);
    }

    [HttpGet("{guid:guid}")]

    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var accountReceivable = await _service.GetByGuidAsync(guid);

        if (accountReceivable == null)
            return NotFound();

        return Ok(accountReceivable);
    }

    [HttpPost]

    public async Task<IActionResult> Add([FromBody] AccountReceivableDTO accountReceivable)
    { 
        var addedAccountReceivable = await _service.AddAsync(accountReceivable);

        return Ok(addedAccountReceivable);
    }

    [HttpPut("{guid:guid}")]

    public async Task<IActionResult> Update(int id, [FromBody] AccountReceivableDTO accountReceivable)
    {
        var updatedAccountReceivable = await _service.UpdateAsync(accountReceivable);

        if (updatedAccountReceivable == null)
            return NotFound();

        return Ok(updatedAccountReceivable);
    }

    [HttpDelete("{guid:guid}")]

    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _service.DeleteAsync(guid);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{guid:guid}/status/{status}")]
    public async Task<IActionResult> ToggleStatus(Guid guid, string status)
    {
        if (!Enum.TryParse(status, true, out AccountStatus accountStatus))
            return BadRequest("Status inválido");

        var result = await _service.ToggleStatus(guid, accountStatus);

        return Ok(result);
    }
}
