using Amazon.DynamoDBv2.DataModel;
using Common.Logging;
using Hangfire;
using Hangfire.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Log.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        readonly IDynamoDBContext _dynamoDBContext;
        private List<LogBackUp> _logList = new List<LogBackUp>();

        public LogController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
            RecurringJob.AddOrUpdate(() => Job(), "0 0 * * *");
        }

        [HttpGet("{logId}")]
        public async Task<IActionResult> GetById(Guid logId)
        {
            var logBackUp = await _dynamoDBContext.LoadAsync<LogBackUp>(logId);

            if (logBackUp == null) 
                return NotFound();

            return Ok(logBackUp);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            var logBackUp = await _dynamoDBContext.ScanAsync<LogBackUp>(default).GetRemainingAsync();

            return Ok(logBackUp);
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
            if(_logList.Count > 0)
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
