using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountPayableController : ControllerBase
{
    private readonly IAccountPayableService _service;

    public AccountPayableController(IAccountPayableService service)
    {
        _service = service;
    }

    [HttpGet]
    
    public async Task<IActionResult> GetAll()
    {
        var accountPayables = await _service.GetAllAsync();
        
        return Ok(accountPayables);
    }
    
    [HttpGet("{guid:guid}")]

    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var accountPayable = await _service.GetByGuidAsync(guid);
        
        if (accountPayable == null)
            return NotFound();
        
        return Ok(accountPayable);
    }

    [HttpPost]
    
    public async Task<IActionResult> Add([FromBody] AccountPayableDTO accountPayable)
    {
        if (accountPayable == null)
            return BadRequest("Account payable cannot be null.");
        
        var addedAccountPayable = await _service.AddAsync(accountPayable);
        
        return Ok(addedAccountPayable);
    }

    [HttpPut("{guid:guid}")]

    public async Task<IActionResult> Update(int id, [FromBody] AccountPayableDTO accountPayable)
    {
        var updatedAccountPayable = await _service.UpdateAsync(accountPayable);
        
        if (updatedAccountPayable == null)
            return NotFound();
        
        return Ok(updatedAccountPayable);
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