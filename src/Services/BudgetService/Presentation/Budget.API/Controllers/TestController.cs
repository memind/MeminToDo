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

            var a = _money.GetMoneyFlowById(Guid.Parse("48775b94-79f6-43e5-f1ba-08dbe603ed73")); //
            var b = _budget.GetBudgetAccountById(Guid.Parse("35533212-c2cb-4a38-997a-08dbe55b146d"));
            var c = _wallet.GetWalletById(Guid.Parse("51c9ad6b-f48c-43aa-99ff-08dbe603ed5b")); //

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

            

            WalletCreateDto wallet1_1 = new WalletCreateDto()
            {
                WalletName = "Test1.1",
                Currency = Domain.Enums.Currency.USD,
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _wallet.CreateWallet(wallet1_1);


            WalletCreateDto wallet1_2 = new WalletCreateDto()
            {
                WalletName = "Test1.2",
                Currency = Domain.Enums.Currency.TL,
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _wallet.CreateWallet(wallet1_2);

            WalletCreateDto wallet2_1 = new WalletCreateDto()
            {
                WalletName = "Test2.1",
                Currency = Domain.Enums.Currency.BTC,
                BudgetAccountId = Guid.Parse("35533212-c2cb-4a38-997a-08dbe55b146d")
            };

            result += _wallet.CreateWallet(wallet2_1);







            MoneyFlowCreateDto flow1_1 = new MoneyFlowCreateDto()
            {
                UserId = userId1,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Income,
                Amount = 1000,
                Description = "First flow for test!",
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _money.CreateMoneyFlow(flow1_1);


            MoneyFlowCreateDto flow1_2 = new MoneyFlowCreateDto()
            {
                UserId = userId1,
                Currency = Domain.Enums.Currency.TL,
                Type = Domain.Enums.MoneyFlowType.Expense,
                Amount = 100,
                Description = "Second flow for test!",
                BudgetAccountId = Guid.Parse("5c6f961f-1100-4824-9979-08dbe55b146d")
            };

            result += _money.CreateMoneyFlow(flow1_2);


            MoneyFlowCreateDto flow1_3 = new MoneyFlowCreateDto()
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

            var updateMoney =_money.GetMoneyFlowByIdAsNoTracking(Guid.Parse("17753fbc-a956-42d2-f1b9-08dbe603ed73"));

            updateMoney.Amount = 0;
            var mapMoney = _mapper.Map<MoneyFlowUpdateDto>(updateMoney);



            var updateWallet = _wallet.GetWalletByIdAsNoTracking(Guid.Parse("51c9ad6b-f48c-43aa-99ff-08dbe603ed5b"));

            updateWallet.Total = 50000;
            updateWallet.Currency = Domain.Enums.Currency.ETH;
            var mapWallet = _mapper.Map<WalletUpdateDto>(updateWallet);

            result += _money.UpdateMoneyFlow(mapMoney);
            result += _wallet.UpdateWallet(mapWallet);

            return result;
        }


        [HttpDelete]
        public int Delete()
        {
            int result = 0;

            result += _money.DeleteMoneyFlow(Guid.Parse("17753fbc-a956-42d2-f1b9-08dbe603ed73"));
            result += _money.DeleteMoneyFlow(Guid.Parse("48775b94-79f6-43e5-f1ba-08dbe603ed73"));
            result += _money.DeleteMoneyFlow(Guid.Parse("d1f24588-6550-4466-f1bb-08dbe603ed73"));

            result += _wallet.DeleteWallet(Guid.Parse("51c9ad6b-f48c-43aa-99ff-08dbe603ed5b"));
            result += _wallet.DeleteWallet(Guid.Parse("16cb3de4-2be1-4ade-99fe-08dbe603ed5b"));
            result += _wallet.DeleteWallet(Guid.Parse("886aa76d-3dd7-4c49-99fd-08dbe603ed5b"));

            result += _budget.DeleteBudgetAccount(Guid.Parse("661ca148-0ebd-4dce-4fe6-08dbe55cfcdf"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("24103926-46cc-4286-4fe5-08dbe55cfcdf"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("3b88dcf1-a12e-4d14-4fe4-08dbe55cfcdf"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("7a3c23f3-832c-4b51-144e-08dbe55c63c1"));
            result += _budget.DeleteBudgetAccount(Guid.Parse("888a60c3-e905-4346-40bd-08dbe55c2ec8"));

            return result;
        }
    }
}
