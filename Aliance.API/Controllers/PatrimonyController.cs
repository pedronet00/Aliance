using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PatrimonyController : ControllerBase
{
    private readonly IPatrimonyService _service;

    public PatrimonyController(IPatrimonyService service)
    {
        _service = service;
    }

    [HttpGet("paged")]
    
    public async Task<IActionResult> GetAllPatrimonies([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        var patrimonies = await _service.GetAllPatrimonies(pageNumber, pageSize);
        return Ok(patrimonies);
    }

    [HttpGet("{guid}")]
    
    public async Task<IActionResult> GetPatrimonyByGuid(Guid guid)
    {
        var result = await _service.GetPatrimonyByGuid(guid);

        return Ok(result.Result);
    }

    [HttpPost]
    
    public async Task<IActionResult> InsertPatrimony([FromBody] PatrimonyDTO patrimony)
    {
        var result = await _service.InsertPatrimony(patrimony);
        return Ok(result.Result);
    }

    [HttpPut]
    
    public async Task<IActionResult> UpdatePatrimony([FromBody] PatrimonyDTO patrimony)
    {
        var result = await _service.UpdatePatrimony(patrimony);
        return Ok(result.Result);
    }

    [HttpDelete]
    [Route("/{id:int}")]
    
    public async Task<IActionResult> DeletePatrimony(Guid guid)
    {
        var result = await _service.DeletePatrimony(guid);
        return Ok(result.Result);
    }

    [HttpPost("{guid}/documents")]
    
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadDocument(IFormFile file, Guid guid)
    {
        var result = await _service.UploadDocumentAsync(guid, file);

        return Ok(result.Result);
    }

    [HttpGet("{guid}/documents")]
    
    public async Task<IActionResult> GetDocuments(Guid guid)
    {
        var result = await _service.GetDocumentsByPatrimony(guid);
        return Ok(result);
    }

}
