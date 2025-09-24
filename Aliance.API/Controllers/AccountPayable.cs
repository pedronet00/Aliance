using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountPayable : ControllerBase
{
    private readonly IAccountPayableService _service;

    public AccountPayable(IAccountPayableService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var accountPayables = await _service.GetAllAsync();
        
        return Ok(accountPayables);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var accountPayable = await _service.GetByIdAsync(id);
        
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
        
        return CreatedAtAction(nameof(GetById), new { id = addedAccountPayable.Id }, addedAccountPayable);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AccountPayableDTO accountPayable)
    {
        if (accountPayable == null)
            return BadRequest("Account payable cannot be null.");
        
        if (id != accountPayable.Id)
            return BadRequest("ID mismatch.");
        
        var updatedAccountPayable = await _service.UpdateAsync(accountPayable);
        
        if (updatedAccountPayable == null)
            return NotFound();
        
        return Ok(updatedAccountPayable);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }
}