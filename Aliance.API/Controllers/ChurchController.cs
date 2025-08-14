using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChurchController : ControllerBase
    {

        private readonly IChurchService _service;

        public ChurchController(IChurchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChurches()
        {
            var churches = await _service.GetAllChurches();
            return Ok(churches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChurchById(int id)
        {
            var church = await _service.GetChurchById(id);
            if (church == null)
            {
                return NotFound();
            }
            return Ok(church);
        }

        [HttpPost]
        public async Task<IActionResult> InsertChurch([FromBody] ChurchDTO church)
        {
            if (church == null)
            {
                return BadRequest("Church data is null");
            }
            var createdChurch = await _service.InsertChurch(church);
            return CreatedAtAction(nameof(GetChurchById), new { id = createdChurch.Id }, createdChurch);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChurch([FromBody] ChurchDTO church)
        {
            if (church == null)
            {
                return BadRequest("Church data is null");
            }
            var result = await _service.UpdateChurch(church);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChurch(int id)
        {
            var result = await _service.DeleteChurch(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
