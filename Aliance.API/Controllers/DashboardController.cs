using Aliance.Application.Interfaces;
using Aliance.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aliance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("{year:int}")]
        public async Task<IActionResult> GetDashboard(int year)
        {
            var result = await _dashboardService.GetDashboardData(year);

            if (result.HasNotifications)
                return BadRequest(result.Notifications);

            return Ok(result.Result);
        }
    }
}
