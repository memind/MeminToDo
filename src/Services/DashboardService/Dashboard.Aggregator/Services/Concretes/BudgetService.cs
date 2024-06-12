using Dashboard.Aggregator.Extensions;
using Dashboard.Aggregator.Models.BudgetModels;
using Dashboard.Aggregator.Models.EntertainmentModels;
using Dashboard.Aggregator.Services.Abstractions;

namespace Dashboard.Aggregator.Services.Concretes
{
    public class BudgetService : IBudgetService
    {
        private readonly HttpClient _client;

        public BudgetService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetTotalBudgetAccountCount()
        {
            var response = await _client.GetAsync($"/api/budget");
            var accounts = await response.ReadContentAs<List<BudgetAccountModel>>();

            return accounts.Count;
        }

        public async Task<int> GetTotalMoneyFlowCount()
        {
            var response = await _client.GetAsync($"/api/moneyflow");
            var moneyFlows = await response.ReadContentAs<List<MoneyFlowModel>>();

            return moneyFlows.Count;
        }

        public async Task<int> GetTotalWalletCount()
        {
            var response = await _client.GetAsync($"/api/wallet");
            var wallets = await response.ReadContentAs<List<WalletModel>>();

            return wallets.Count;
        }

        public async Task<int> GetUsersBudgetAccountCount(string id)
        {
            var response = await _client.GetAsync($"/api/Budget/useraccounts/{id}");
            var budgetAccounts = await response.ReadContentAs<List<BudgetAccountModel>>();

            return budgetAccounts.Count;
        }

        public async Task<int> GetUsersMoneyFlowCount(string id)
        {
            var response = await _client.GetAsync($"/api/moneyflow/userflows/{id}");
            var moneyFlows = await response.ReadContentAs<List<MoneyFlowModel>>();

            return moneyFlows.Count;
        }

        public async Task<int> GetUsersWalletCount(string id)
        {
            var response = await _client.GetAsync($"/api/wallet/userwallets/{id}");
            var wallets = await response.ReadContentAs<List<WalletModel>>();

            return wallets.Count;
        }
    }
}
