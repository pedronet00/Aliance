using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Aliance.Application.ViewModel;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MissionCampaignDonationController : ControllerBase
    {
        private readonly IMissionCampaignDonationService _service;

        public MissionCampaignDonationController(IMissionCampaignDonationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista doações por campanha
        /// </summary>
        [HttpGet("campaign/paged/{campaignGuid:guid}")]
        public async Task<ActionResult<IEnumerable<MissionCampaignDonationViewModel>>> ListByCampaign(Guid campaignGuid, [FromQuery] int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.ListByCampaign(campaignGuid, pageNumber, pageSize);

            if (result.HasNotifications)
                return NotFound(result.Notifications);

            return Ok(result.Result);
        }

        /// <summary>
        /// Lista doações por usuário
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MissionCampaignDonationViewModel>>> ListByUser(string userId)
        {
            var result = await _service.ListByUser(userId);

            if (result.HasNotifications)
                return NotFound(result.Notifications);

            return Ok(result.Result);
        }

        /// <summary>
        /// Adiciona uma doação para campanha missionária
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<MissionCampaignDonationViewModel>> AddDonation([FromBody] MissionCampaignDonationDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddDonation(dto);

            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return CreatedAtAction(nameof(GetByGuid), new { guid = result.Result.Guid }, result.Result);
        }

        /// <summary>
        /// Busca doação pelo Guid
        /// </summary>
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<MissionCampaignDonationViewModel>> GetByGuid(Guid guid)
        {
            // Não tem método específico no service, então reaproveitando ListByUser/ListByCampaign não faz sentido.
            // Se quiser buscar individual, adicione no service + repo. Por enquanto, retorna NotImplemented para evitar ruído.
            return StatusCode(501, "Endpoint não implementado pois o service não possui método para buscar por Guid.");
        }

        /// <summary>
        /// Remove doação
        /// </summary>
        [HttpDelete("{guid:guid}")]
        public async Task<ActionResult> Delete(Guid guid)
        {
            var result = await _service.DeleteDonation(guid);

            if (result.HasNotifications)
                return NotFound(result.Notifications);

            return NoContent();
        }
    }
}
