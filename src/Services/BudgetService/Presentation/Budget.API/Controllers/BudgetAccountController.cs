using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetAccountController : ControllerBase
    {
        private readonly IBudgetAccountService _budgetAccount;

        public BudgetAccountController(IBudgetAccountService budgetAccount) => _budgetAccount = budgetAccount;

        [HttpGet]
        public List<BudgetAccountDto> GetAllBudgetAccounts() => _budgetAccount.GetAllBudgetAccounts();

        [HttpGet("/user/{userId}")]
        public List<BudgetAccountDto> GetUsersAllBudgetAccounts(Guid userId) => _budgetAccount.GetUsersAllBudgetAccounts(userId);

        [HttpGet("/{budgetAccountId}")]
        public BudgetAccountDto GetBudgetAccountById(Guid budgetAccountId) => _budgetAccount.GetBudgetAccountById(budgetAccountId);

        [HttpPost]
        public int CreateBudgetAccount(BudgetAccountDto model) => _budgetAccount.CreateBudgetAccount(model);

        [HttpPut]
        public int UpdateBudgetAccount(BudgetAccountDto model) => _budgetAccount.UpdateBudgetAccount(model);

        [HttpDelete]
        public int DeleteBudgetAccount(Guid budgetAccountId) => _budgetAccount.DeleteBudgetAccount(budgetAccountId);
    }
}
