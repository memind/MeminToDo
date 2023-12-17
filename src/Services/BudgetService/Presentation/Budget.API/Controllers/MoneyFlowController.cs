using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.MoneyFlowDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyFlowController : ControllerBase
    {
        private readonly IMoneyFlowService _moneyFlow;

        public MoneyFlowController(IMoneyFlowService moneyFlow) => _moneyFlow = moneyFlow;

        [HttpGet]
        public List<MoneyFlowDto> GetAllMoneyFlows() => _moneyFlow.GetAllMoneyFlows();

        [HttpGet("/user/{userId}")]
        public List<MoneyFlowDto> GetUsersAllMoneyFlows(Guid userId) => _moneyFlow.GetUsersAllMoneyFlows(userId);

        [HttpGet("/noTracking/{moneyFlowId}")]
        public MoneyFlowDto GetMoneyFlowByIdAsNoTracking(Guid moneyFlowId) => _moneyFlow.GetMoneyFlowByIdAsNoTracking(moneyFlowId);

        [HttpGet("/{moneyFlowId}")]
        public MoneyFlowDto GetMoneyFlowById(Guid moneyFlowId) => _moneyFlow.GetMoneyFlowById(moneyFlowId);

        [HttpPost]
        public int CreateMoneyFlow(MoneyFlowDto model) => _moneyFlow.CreateMoneyFlow(model);

        [HttpPut]
        public int UpdateMoneyFlow(MoneyFlowDto model) => _moneyFlow.UpdateMoneyFlow(model);

        [HttpDelete]
        public int DeleteMoneyFlow(Guid moneyFlowId) => _moneyFlow.DeleteMoneyFlow(moneyFlowId);
    }
}
