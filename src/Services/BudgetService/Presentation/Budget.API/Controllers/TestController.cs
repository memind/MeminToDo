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
        public void Get()
        {
            var x = _money.GetAllMoneyFlows();
            var y = _budget.GetAllBudgetAccounts();
            var z = _wallet.GetAllWallets();

            var a = _money.GetMoneyFlowById(Guid.Parse("11be816d-b1fb-437a-2e26-08dbe56048ec"));
            var b = _budget.GetBudgetAccountById(Guid.Parse("7a3c23f3-832c-4b51-144e-08dbe55c63c1"));
            var c = _wallet.GetWalletById(Guid.Parse("7d445be0-110f-4264-7529-08dbe55fb389"));

            var e = _money.GetUsersAllMoneyFlows(Guid.Parse("f8b5f86c-710f-4fde-bcaf-2add21116b27"));
            var f = _budget.GetUsersAllBudgetAccounts(Guid.Parse("95e685e6-c39c-4550-90a6-fc1d8b27f7b5"));

            var date = DateTime.Now;
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
