using Dashboard.Aggregator.Models.Enums;

namespace Dashboard.Aggregator.Models.BudgetModels
{
    public class MoneyFlowModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public Currency Currency { get; set; }
        public MoneyFlowType Type { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string? Message { get; set; }

        public Guid BudgetAccountId { get; set; }
    }
}
