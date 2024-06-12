namespace Dashboard.Aggregator.Services.Abstractions
{
    public interface IBudgetService
    {
        Task<int> GetTotalBudgetAccountCount();
        Task<int> GetTotalWalletCount();
        Task<int> GetTotalMoneyFlowCount();
        Task<int> GetUsersBudgetAccountCount(string id);
        Task<int> GetUsersWalletCount(string id);
        Task<int> GetUsersMoneyFlowCount(string id);

    }
}
