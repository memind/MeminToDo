using AutoMapper;
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
        private readonly IMapper _mapper;

        public TestController(IMoneyFlowService money, IBudgetAccountService budget, IWalletService wallet, IMapper mapper)
        {
            _money = money;
            _budget = budget;
            _wallet = wallet;
            _mapper = mapper;
        }

        [HttpGet]
        public void Get()
        {
            Guid userId1 = Guid.Parse("95e685e6-c39c-4550-90a6-fc1d8b27f7b5"); // BA = 5c6f961f-1100-4824-9979-08dbe55b146d
            Guid userId2 = Guid.Parse("e318626c-4ce2-41fd-8c1a-aefd526570e6"); // BA = 35533212-c2cb-4a38-997a-08dbe55b146d

            var x = _money.GetAllMoneyFlows();
            var y = _budget.GetAllBudgetAccounts();
            var z = _wallet.GetAllWallets();

            var a = _money.GetMoneyFlowById(Guid.Parse("4c19ceae-5d2f-4c96-bf94-08dbe61eda35")); //
            var b = _budget.GetBudgetAccountById(Guid.Parse("35533212-c2cb-4a38-997a-08dbe55b146d"));
            var c = _wallet.GetWalletById(Guid.Parse("b0ee7872-1cd8-46e4-6e58-08dbe61eda17")); //

            var e1 = _money.GetUsersAllMoneyFlows(userId1);
            var g1 = _wallet.GetUsersAllWallets(userId1);
            var f1 = _budget.GetUsersAllBudgetAccounts(userId1);

            var e2 = _money.GetUsersAllMoneyFlows(userId2);
            var g2 = _wallet.GetUsersAllWallets(userId2);
            var f2 = _budget.GetUsersAllBudgetAccounts(userId2);

            var date = DateTime.Now;
        }


        [HttpPost]
        public int Post()
        {
            Guid userId1 = Guid.Parse("95e685e6-c39c-4550-90a6-fc1d8b27f7b5"); // BA = 5c6f961f-1100-4824-9979-08dbe55b146d
            Guid userId2 = Guid.Parse("e318626c-4ce2-41fd-8c1a-aefd526570e6"); // BA = 35533212-c2cb-4a38-997a-08dbe55b146d

            int result = 0;

            

            WalletDto wallet1_1 = new WalletDto()
            {
                WalletName = "Test1.1",
                Currency = Domain.Enums.Currency.USD,
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _wallet.CreateWallet(wallet1_1);


            WalletDto wallet1_2 = new WalletDto()
            {
                WalletName = "Test1.2",
                Currency = Domain.Enums.Currency.TL,
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _wallet.CreateWallet(wallet1_2);

            WalletDto wallet2_1 = new WalletDto()
            {
                WalletName = "Test2.1",
                Currency = Domain.Enums.Currency.BTC,
                BudgetAccountId = Guid.Parse("35533212-c2cb-4a38-997a-08dbe55b146d")
            };

            result += _wallet.CreateWallet(wallet2_1);







            MoneyFlowDto flow1_1 = new MoneyFlowDto()
            {
                UserId = userId1,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Income,
                Amount = 1000,
                Description = "First flow for test!",
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _money.CreateMoneyFlow(flow1_1);


            MoneyFlowDto flow1_2 = new MoneyFlowDto()
            {
                UserId = userId1,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Expense,
                Amount = 100,
                Description = "Second flow for test!",
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _money.CreateMoneyFlow(flow1_2);


            MoneyFlowDto flow1_3 = new MoneyFlowDto()
            {
                UserId = userId2,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Expense,
                Amount = 150,
                Description = "Third flow for test!",
                BudgetAccountId = Guid.Parse("35533212-c2cb-4a38-997a-08dbe55b146d")
            };

            result += _money.CreateMoneyFlow(flow1_3);

            return result;
        }


        [HttpPut]
        public int Put()
        {
            int result = 0;

            //var updateMoney = _money.GetMoneyFlowByIdAsNoTracking(Guid.Parse("6c490135-d647-40ba-bf93-08dbe61eda35"));

            //updateMoney.Amount = 150;
            //var mapMoney = _mapper.Map<MoneyFlowDto>(updateMoney);



            var updateWallet = _wallet.GetWalletByIdAsNoTracking(Guid.Parse("b0ee7872-1cd8-46e4-6e58-08dbe61eda17"));

            updateWallet.Total = 100000;
            updateWallet.Currency = Domain.Enums.Currency.BTC;
            var mapWallet = _mapper.Map<WalletDto>(updateWallet);

            //result += _money.UpdateMoneyFlow(mapMoney);
            result += _wallet.UpdateWallet(mapWallet);

            return result;
        }


        [HttpDelete]
        public int Delete()
        {
            int result = 0;

            result += _money.DeleteMoneyFlow(Guid.Parse("36e506eb-57e0-4343-b9c5-08dbe61f2a01"));
            result += _money.DeleteMoneyFlow(Guid.Parse("15fb511b-be1f-4c76-b9c6-08dbe61f2a01"));
            result += _money.DeleteMoneyFlow(Guid.Parse("f86a2776-24e8-457a-b9c7-08dbe61f2a01"));

            result += _wallet.DeleteWallet(Guid.Parse("2316dca8-674d-4292-a142-08dbe61f29e7"));
            result += _wallet.DeleteWallet(Guid.Parse("0b114a9e-090c-4028-a143-08dbe61f29e7"));
            result += _wallet.DeleteWallet(Guid.Parse("849b3da6-ae5e-48b8-a144-08dbe61f29e7"));

            result += _budget.DeleteBudgetAccount(Guid.Parse("661ca148-0ebd-4dce-4fe6-08dbe55cfcdf"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("24103926-46cc-4286-4fe5-08dbe55cfcdf"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("3b88dcf1-a12e-4d14-4fe4-08dbe55cfcdf"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("7a3c23f3-832c-4b51-144e-08dbe55c63c1"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("888a60c3-e905-4346-40bd-08dbe55c2ec8"));

            return result;
        }
    }
}
