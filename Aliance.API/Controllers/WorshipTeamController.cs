using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorshipTeamController : ControllerBase
    {
        private readonly IWorshipTeamService _service;

        public WorshipTeamController(IWorshipTeamService service)
        {
            _service = service;
        }

        [HttpGet("paged")]

        public async Task<IActionResult> GetAllWorshipTeams([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var WorshipTeams = await _service.GetWorshipTeamsPaged(pageNumber, pageSize);

            return Ok(WorshipTeams);
        }

        [HttpPost]

        public async Task<IActionResult> InsertWorshipTeam([FromBody] WorshipTeamDTO WorshipTeam)
        {
            var createdWorshipTeam = await _service.InsertWorshipTeam(WorshipTeam);
            return Created("created", createdWorshipTeam);
        }

        [HttpGet("{guid}")]

        public async Task<IActionResult> GetWorshipTeamByGuid(Guid guid)
        {
            var WorshipTeam = await _service.GetWorshipTeamByGuid(guid);

            return Ok(WorshipTeam);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateWorshipTeam([FromBody] WorshipTeamDTO WorshipTeam)
        {
            var updated = await _service.UpdateWorshipTeam(WorshipTeam);

            return Ok(updated);
        }

        [HttpDelete("{guid}")]

        public async Task<IActionResult> DeleteWorshipTeam(Guid guid)
        {
            var result = await _service.DeleteWorshipTeam(guid);

            return Ok(result);
        }

        [HttpPatch("activate/{guid}")]

        public async Task<IActionResult> ActivateWorshipTeam(Guid guid)
        {
            var result = await _service.ActivateWorshipTeam(guid);
            return Ok(result);
        }

        [HttpPatch("deactivate/{guid}")]

        public async Task<IActionResult> DeactivateWorshipTeam(Guid guid)
        {
            var result = await _service.DeactivateWorshipTeam(guid);
            return Ok(result);
        }
    }
}
