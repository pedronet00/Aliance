using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PatrimonyMaintenanceController : ControllerBase
{
    private readonly IPatrimonyMaintenanceService _service;

    public PatrimonyMaintenanceController(IPatrimonyMaintenanceService service)
    {
        _service = service;
    }

    [HttpGet]
    
    public async Task<IActionResult> GetAllMaintenances()
    {
        var maintenances = await _service.GetAllMaintenances();
        return Ok(maintenances);
    }

    [HttpGet("{guid}")]
    
    public async Task<IActionResult> GetMaintenanceByGuid(Guid guid)
    {
        var result = await _service.GetMaintenanceByGuid(guid);

        return Ok(result.Result);
    }

    [HttpPost]
    
    public async Task<IActionResult> InsertMaintenance(PatrimonyMaintenanceDTO dto)
    {
        var result = await _service.InsertMaintenance(dto);

        if (result.Notifications.Any())
            return BadRequest(result.Notifications);


        return Ok(result.Result);
    }

    [HttpPut]
    
    public async Task<IActionResult> UpdateMaintenance([FromBody] Application.DTOs.PatrimonyMaintenanceDTO maintenance)
    {
        var result = await _service.UpdateMaintenance(maintenance);

        return Ok(result);
    }

    [HttpDelete("{guid}")]
    
    public async Task<IActionResult> DeleteMaintenance(Guid guid)
    {
        var result = await _service.DeleteMaintenance(guid);

        return Ok(result);
    }

    [HttpGet("patrimony/{patrimonyGuid}")]
    
    public async Task<IActionResult> GetMaintenancesByPatrimonyGuid(Guid patrimonyGuid)
    {
        var result = await _service.GetMaintenancesByPatrimonyGuid(patrimonyGuid);
        return Ok(result);
    }

    [HttpPost("{guid}/documents")]
    
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadDocument(IFormFile file, Guid guid)
    {
        var result = await _service.UploadDocumentAsync(guid, file);

        return Ok(result);
    }

    [HttpGet("{guid}/documents")]
    
    public async Task<IActionResult> GetDocuments(Guid guid)
    {
        var result = await _service.GetDocumentsByMaintenance(guid);
        return Ok(result);
    }
}
