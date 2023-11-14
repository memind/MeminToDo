using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Application.DTOs.WalletDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMoneyFlowService _money;
        private readonly IBudgetAccountService _budget;
        private readonly IWalletService _wallet;

        public TestController(IMoneyFlowService money, IBudgetAccountService budget, IWalletService wallet)
        {
            _money = money;
            _budget = budget;
            _wallet = wallet;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return null;
        }


        [HttpPost]
        public int Post()
        {
            Guid userId = Guid.NewGuid();
            int result = 0;

            //BudgetAccountCreateDto budget = new BudgetAccountCreateDto()
            //{
            //    UserId = userId
            //};

            //result += _budget.CreateBudgetAccount(budget);

            WalletCreateDto wallet1_1 = new WalletCreateDto()
            {
                WalletName = "Test1.1",
                Currency = Domain.Enums.Currency.TL,
            };

            result += _wallet.CreateWallet(wallet1_1);


            WalletCreateDto wallet1_2 = new WalletCreateDto()
            {
                WalletName = "Test1.2",
                Currency = Domain.Enums.Currency.TL
            };

            result += _wallet.CreateWallet(wallet1_2);

            MoneyFlowCreateDto flow1_1 = new MoneyFlowCreateDto()
            {
                UserId = userId,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Income,
                Amount = 1000,
                Description = "First flow for test!"
            };

            result += _money.CreateMoneyFlow(flow1_1);


            MoneyFlowCreateDto flow1_2 = new MoneyFlowCreateDto()
            {
                UserId = userId,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Expense,
                Amount = 100,
                Description = "Second flow for test!"
            };

            result += _money.CreateMoneyFlow(flow1_2);


            MoneyFlowCreateDto flow1_3 = new MoneyFlowCreateDto()
            {
                UserId = userId,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Expense,
                Amount = 150,
                Description = "Third flow for test!"
            };

            result += _money.CreateMoneyFlow(flow1_3);

            return result;
        }


        [HttpPut]
        public IActionResult Put()
        {
            return null;
        }


        [HttpDelete]
        public IActionResult Delete()
        {

            return null;
        }
    }
}
