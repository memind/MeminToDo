using Common.Caching.Services;
using Dashboard.Aggregator.Consts;
using Dashboard.Aggregator.Models;
using Dashboard.Aggregator.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Dashboard.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private IDatabase _cache;

        public DashboardController(IDashboardService dashboardService)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _dashboardService = dashboardService;
        }

        [HttpGet("GetAdminDashboardInfos")]
        [Authorize(Policy = "DashboardRead")]
        public async Task<AdminDashboardModel> GetAdmin()
        {
            var cacheKey = CacheConsts.GetAdmin();
            var cachedAdminInfos = await _cache.StringGetAsync(cacheKey);

            if (!cachedAdminInfos.IsNull)
                return (JsonConvert.DeserializeObject<AdminDashboardModel>(cachedAdminInfos));

            var adminDashboardInfos = _dashboardService.GetAdminDashboardInfos();

            var serializedAdminDashboardInfos = JsonConvert.SerializeObject(adminDashboardInfos);
            await _cache.StringSetAsync(cacheKey, serializedAdminDashboardInfos);

            return await _dashboardService.GetAdminDashboardInfos();
        }

        [HttpGet("GetUserDashboardInfos")]
        [Authorize(Policy = "DashboardRead")]
        public async Task<UserDashboardModel> GetUser(string id)
        {
            var cacheKey = CacheConsts.GetUser();
            var cachedUserInfos = await _cache.StringGetAsync(cacheKey);

            if (!cachedUserInfos.IsNull)
                return (JsonConvert.DeserializeObject<UserDashboardModel>(cachedUserInfos));

            var userDashboardInfos = _dashboardService.GetUserDashboardInfos(id);

            var serializedUserDashboardInfos = JsonConvert.SerializeObject(userDashboardInfos);
            await _cache.StringSetAsync(cacheKey, serializedUserDashboardInfos);

            return await _dashboardService.GetUserDashboardInfos(id);
        }
    }
}
