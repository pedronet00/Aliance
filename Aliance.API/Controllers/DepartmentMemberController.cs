using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentMemberController : ControllerBase
    {
        private readonly IDepartmentMemberService _service;

        public DepartmentMemberController(IDepartmentMemberService service)
        {
            _service = service;
        }

        [HttpGet("{departmentGuid:guid}")]
        public async Task<IActionResult> GetDepartmentMembers(Guid departmentGuid, int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetDepartmentMembers(departmentGuid, pageNumber, pageSize);
            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return Ok(result.Result);
        }

        [HttpPost("{departmentGuid:guid}/member/{memberId}")]
        public async Task<IActionResult> InsertDepartmentMember(Guid departmentGuid, string memberId)
        {
            var result = await _service.InsertDepartmentMember(departmentGuid, memberId);
            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return Ok(result.Result);
        }

        [HttpDelete("{departmentGuid:guid}/member/{memberId}")]
        public async Task<IActionResult> DeleteDepartmentMember(Guid departmentGuid, string memberId)
        {
            var result = await _service.DeleteDepartmentMember(departmentGuid, memberId);
            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return NoContent();
        }

        [HttpPatch("{departmentGuid:guid}/member/{memberId}/status")]
        public async Task<IActionResult> ToggleMemberStatus(Guid departmentGuid, string memberId)
        {
            var result = await _service.ToggleMemberStatus(departmentGuid, memberId);
            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return Ok(result.Result);
        }
    }
}
