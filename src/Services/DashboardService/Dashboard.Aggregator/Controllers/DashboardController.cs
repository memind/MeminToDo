using Dashboard.Aggregator.Models;
using Dashboard.Aggregator.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("GetAdminDashboardInfos")]
        public async Task<AdminDashboardModel> GetAdmin()
        {
            return await _dashboardService.GetAdminDashboardInfos();
        }

        [HttpGet("GetUserDashboardInfos")]
        public async Task<UserDashboardModel> GetUser(string id)
        {
            return await _dashboardService.GetUserDashboardInfos(id);
        }
    }
}
