using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.MoneyFlowDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoneyFlowController : ControllerBase
    {
        private readonly IMoneyFlowService _moneyFlow;

        public MoneyFlowController(IMoneyFlowService moneyFlow) => _moneyFlow = moneyFlow;

        [HttpGet]
        [Authorize(Policy = "BudgetRead")]
        public List<MoneyFlowDto> GetAllMoneyFlows() => _moneyFlow.GetAllMoneyFlows();

        [HttpGet("/user/{userId}")]
        [Authorize(Policy = "BudgetRead")]
        public List<MoneyFlowDto> GetUsersAllMoneyFlows(Guid userId) => _moneyFlow.GetUsersAllMoneyFlows(userId);

        [HttpGet("/{moneyFlowId}")]
        [Authorize(Policy = "BudgetRead")]
        public MoneyFlowDto GetMoneyFlowById(Guid moneyFlowId) => _moneyFlow.GetMoneyFlowById(moneyFlowId);

        [HttpPost]
        [Authorize(Policy = "BudgetWrite")]
        public int CreateMoneyFlow(MoneyFlowDto model) => _moneyFlow.CreateMoneyFlow(model);

        [HttpPut]
        [Authorize(Policy = "BudgetWrite")]
        public int UpdateMoneyFlow(MoneyFlowDto model) => _moneyFlow.UpdateMoneyFlow(model);

        [HttpDelete("/{moneyFlowId}")]
        [Authorize(Policy = "BudgetWrite")]
        public int DeleteMoneyFlow(Guid moneyFlowId) => _moneyFlow.DeleteMoneyFlow(moneyFlowId);
    }
}
