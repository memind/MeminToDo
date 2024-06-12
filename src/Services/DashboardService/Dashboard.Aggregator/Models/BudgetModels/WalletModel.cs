using Dashboard.Aggregator.Models.Enums;

namespace Dashboard.Aggregator.Models.BudgetModels
{
    public class WalletModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string WalletName { get; set; }
        public Currency Currency { get; set; }
        public int Total { get; set; }
        public Guid BudgetAccountId { get; set; }
    }
}
