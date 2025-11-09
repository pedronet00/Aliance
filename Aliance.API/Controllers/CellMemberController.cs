using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CellMemberController : ControllerBase
{
    private readonly ICellMemberService _service;

    public CellMemberController(ICellMemberService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna todos os membros de uma célula.
    /// </summary>
    [HttpGet("{cellGuid:guid}")]
    public async Task<IActionResult> GetCellMembers(Guid cellGuid)
    {
        var result = await _service.GetCellMembers(cellGuid);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

    /// <summary>
    /// Adiciona um membro a uma célula.
    /// </summary>
    [HttpPost("{cellGuid:guid}/member/{memberId}")]
    public async Task<IActionResult> InsertCellMember(Guid cellGuid, string memberId)
    {
        var result = await _service.InsertCellMember(cellGuid, memberId);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }

    /// <summary>
    /// Remove um membro de uma célula.
    /// </summary>
    [HttpDelete("{cellGuid:guid}/member/{memberId}")]
    public async Task<IActionResult> DeleteCellMember(Guid cellGuid, string memberId)
    {
        var result = await _service.DeleteCellMember(cellGuid, memberId);
        if (result.HasNotifications)
            return Ok(result);

        return NoContent();
    }

    /// <summary>
    /// Alterna o status (ativo/inativo) de um membro.
    /// </summary>
    [HttpPatch("{cellGuid:guid}/member/{memberId}/status")]
    public async Task<IActionResult> ToggleMemberStatus(Guid cellGuid, string memberId)
    {
        var result = await _service.ToggleMemberStatus(cellGuid, memberId);
        if (result.HasNotifications)
            return BadRequest(result.Notifications);

        return Ok(result.Result);
    }
}
