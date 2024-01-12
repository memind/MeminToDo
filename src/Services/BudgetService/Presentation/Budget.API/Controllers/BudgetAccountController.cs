using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetAccountController : ControllerBase
    {
        private readonly IBudgetAccountService _budgetAccount;

        public BudgetAccountController(IBudgetAccountService budgetAccount) => _budgetAccount = budgetAccount;

        [HttpGet]
        [Authorize(Policy = "BudgetRead")]
        public List<BudgetAccountDto> GetAllBudgetAccounts() => _budgetAccount.GetAllBudgetAccounts();

        [HttpGet("/userAccounts/{userId}")]
        [Authorize(Policy = "BudgetRead")]
        public List<BudgetAccountDto> GetUsersAllBudgetAccounts(Guid userId) => _budgetAccount.GetUsersAllBudgetAccounts(userId);

        [HttpGet("/{budgetAccountId}")]
        [Authorize(Policy = "BudgetRead")]
        public BudgetAccountDto GetBudgetAccountById(Guid budgetAccountId) => _budgetAccount.GetBudgetAccountById(budgetAccountId);
        
        [HttpPost]
        [Authorize(Policy = "BudgetWrite")]
        public int CreateBudgetAccount(BudgetAccountDto model) => _budgetAccount.CreateBudgetAccount(model);

        [HttpPut]
        [Authorize(Policy = "BudgetWrite")]
        public int UpdateBudgetAccount(BudgetAccountDto model) => _budgetAccount.UpdateBudgetAccount(model);

        [HttpDelete("/{budgetAccountId}")]
        [Authorize(Policy = "BudgetWrite")]
        public int DeleteBudgetAccount(Guid budgetAccountId) => _budgetAccount.DeleteBudgetAccount(budgetAccountId);

        [HttpGet("/account/consumeBackup")]
        [Authorize(Policy = "BudgetRead")]
        public void ConsumeBackUpInfo() => _budgetAccount.ConsumeBackUpInfo();

        [HttpGet("/account/consumeTest")]
        [Authorize(Policy = "BudgetRead")]
        public void ConsumeTestInfo() => _budgetAccount.ConsumeTestInfo();
    }
}
