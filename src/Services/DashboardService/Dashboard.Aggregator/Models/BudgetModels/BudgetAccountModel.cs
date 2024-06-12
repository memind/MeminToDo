namespace Dashboard.Aggregator.Models.BudgetModels
{
    public class BudgetAccountModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public List<MoneyFlowModel> MoneyFlows { get; set; } = new List<MoneyFlowModel>();
        public List<WalletModel> Wallets { get; set; } = new List<WalletModel>();
    }
}
