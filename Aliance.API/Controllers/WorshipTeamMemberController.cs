using Aliance.Application.Interfaces;
using Aliance.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorshipTeamMemberController : ControllerBase
    {
        private readonly IWorshipTeamMemberService _service;
        public WorshipTeamMemberController(IWorshipTeamMemberService worshipTeamMemberService)
        {
            _service = worshipTeamMemberService;
        }

        [HttpGet("{teamGuid:guid}")]
        public async Task<IActionResult> GetWorshipTeamMembers(Guid teamGuid, int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetWorshipTeamMembers(teamGuid, pageNumber, pageSize);
            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return Ok(result.Result);
        }

        [HttpPost("{teamGuid:guid}/member/{memberId}")]
        public async Task<IActionResult> InsertMember(Guid teamGuid, string memberId)
        {
            var result = await _service.InsertWorshipTeamMember(teamGuid, memberId);
            if (result.HasNotifications)
                return Ok(result);

            return Ok(result.Result);
        }

        [HttpDelete("{teamGuid:guid}/member/{memberId}")]
        public async Task<IActionResult> DeleteCellMember(Guid teamGuid, string memberId)
        {
            var result = await _service.DeleteWorshipTeamMember(teamGuid, memberId);
            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return NoContent();
        }

        [HttpPut("{teamGuid:guid}/member/{memberId}/status/{status:bool}")]
        public async Task<IActionResult> ToggleMemberStatus(Guid teamGuid, string memberId, bool status)
        {
            var result = await _service.ToggleMemberStatus(teamGuid, memberId, status);
            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return Ok(result.Result);
        }


    }
}
