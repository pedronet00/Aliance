using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Enums;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CellMeetingController : ControllerBase
    {
        private readonly ICellMeetingService _service;

        public CellMeetingController(ICellMeetingService service)
        {
            _service = service;
        }

        [HttpGet("cell/{guid:guid}")]
        public async Task<IActionResult> GetAll(Guid guid)
        {
            var result = await _service.GetCellMeetings(guid);

            return Ok(result.Result);
        }

        [HttpGet("meeting/{guid:guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid)
        {
            var result = await _service.GetCellMeetingByGuid(guid);

            if (result.HasNotifications)
                return NotFound(result.Notifications);

            return Ok(result.Result);
        }

        [HttpGet("next")]
        public async Task<IActionResult> GetNext()
        {
            var result = await _service.GetNextCellMeeting();

            if (result.HasNotifications)
                return NotFound(result.Notifications);

            return Ok(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CellMeetingDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.InsertCellMeeting(dto);

            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return CreatedAtAction(nameof(GetByGuid), new { guid = result.Result.Guid }, result.Result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CellMeetingDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateCellMeeting(dto);

            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return Ok(result.Result);
        }

        [HttpDelete("{guid:guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await _service.DeleteCellMeeting(guid);

            if (result.HasNotifications)
                return NotFound(result.Notifications);

            return Ok(result.Result);
        }

        [HttpPatch("{guid:guid}/status/{status}")]
        public async Task<IActionResult> ToggleStatus(Guid guid, MeetingStatus status)
        {
            var result = await _service.ToggleStatus(guid, status);

            if (result.HasNotifications)
                return NotFound(result);

            return Ok(result);
        }
    }
}
