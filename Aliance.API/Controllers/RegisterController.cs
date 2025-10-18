using Aliance.Application.DTOs.Auth;
using Aliance.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        /// <summary>
        /// Cria uma nova igreja, usuário e inicia a assinatura no Asaas.
        /// </summary>
        [HttpPost("new-client")]
        public async Task<IActionResult> RegisterNewClient([FromBody] NewClientDTO dto)
        {
            var result = await _registerService.Register(dto);

            if (result.Notifications.Any())
                return BadRequest(new { errors = result.Notifications });

            return Ok(new
            {
                success = true,
                asaasCustomerId = result.Result?.AsaasCustomerId,
                checkoutUrl = result.Result?.CheckoutUrl 
            });
        }

    }
}
