using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoutineController : ControllerBase
{
    private readonly IRoutineService _service;

    public RoutineController(IRoutineService service)
    {
        _service = service;
    }

    [HttpPost("routine-accounts-expiring")]
    public async Task<IActionResult> RunAccountsExpiringRoutine()
    {
        var result = await _service.UpdateExpiringAccountsStatus();

        return result.HasNotifications
            ? BadRequest(result.Notifications)
            : Ok(result.Result);
    }

    [HttpPost("routine-automatic-accounts")]
    public async Task<IActionResult> RunAutomaticAccountsRotine()
    {
        var result = await _service.InsertAutomaticAccounts();

        return result.HasNotifications
            ? BadRequest(result.Notifications)
            : Ok(result.Result);
    }

}
