using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime.Internal.Util;
using Common.Caching.Services;
using Common.Logging;
using Hangfire;
using Hangfire.Logging;
using Log.API.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;

namespace Log.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        readonly IDynamoDBContext _dynamoDBContext;
        private IDatabase _cache;
        private List<LogBackUp> _logList = new List<LogBackUp>();

        public LogController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
            _cache = RedisService.GetRedisMasterDatabase();
            RecurringJob.AddOrUpdate(() => Job(), "0 0 * * *");
        }

        [HttpGet("{logId}")]
        public async Task<IActionResult> GetById(Guid logId)
        {
            var cacheKey = CacheConsts.GetById(logId);
            var cachedLogBackUp = await _cache.StringGetAsync(cacheKey);

            if (!cachedLogBackUp.IsNull)
                return Ok(JsonConvert.DeserializeObject<LogBackUp>(cachedLogBackUp));

            var logBackUp = await _dynamoDBContext.LoadAsync<LogBackUp>(logId);

            if (logBackUp == null)
                return NotFound();

            var serializedLog = JsonConvert.SerializeObject(logBackUp);
            await _cache.StringSetAsync(cacheKey, serializedLog);

            return Ok(logBackUp);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            var cacheKey = CacheConsts.GetAllLogs();
            var cachedLogs = await _cache.StringGetAsync(cacheKey);

            if (!cachedLogs.IsNull)
                return Ok(JsonConvert.DeserializeObject<List<LogBackUp>>(cachedLogs));

            var logBackUps = await _dynamoDBContext.ScanAsync<LogBackUp>(default).GetRemainingAsync();

            if (logBackUps == null || !logBackUps.Any())
                return NotFound();

            var serializedLogs = JsonConvert.SerializeObject(logBackUps);
            await _cache.StringSetAsync(cacheKey, serializedLogs);

            return Ok(logBackUps);
        }

        [HttpPost("/createBackUp")]
        public async Task CreateLogBackUp(LogBackUp logBackUpRequest)
        {
            _logList.Add(logBackUpRequest);
            await Job();
        }

        [HttpDelete("{logBackUpId}")]
        public async Task<IActionResult> DeleteLog(Guid logBackUpId)
        {
            var logBackUp = await _dynamoDBContext.LoadAsync<LogBackUp>(logBackUpId);

            if (logBackUp == null)
                return NotFound();

            await _dynamoDBContext.DeleteAsync(logBackUp);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLog(LogBackUp logBackUpRequest)
        {
            var logBackUp = await _dynamoDBContext.LoadAsync<LogBackUp>(logBackUpRequest.Id);

            if (logBackUp == null)
                return NotFound();

            await _dynamoDBContext.SaveAsync(logBackUpRequest);

            return Ok(logBackUpRequest);
        }

        [HttpGet("/job")]
        public async Task Job()
        {
            if (_logList.Count > 0)
                foreach (var log in _logList.ToList())
                {
                    log.Id = Guid.NewGuid();
                    log.IsBackedUp = true;

                    await _dynamoDBContext.SaveAsync(log);
                    _logList.Remove(log);
                }
        }
    }
}
