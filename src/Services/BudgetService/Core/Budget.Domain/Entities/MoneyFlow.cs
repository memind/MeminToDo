using Budget.Domain.Entities.Common;
using Budget.Domain.Enums;

namespace Budget.Domain.Entities
{
    public class MoneyFlow : BaseEntity
    {
        public Guid UserId { get; set; }
        public Currency Currency { get; set; }
        public MoneyFlowType Type { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string? Message { get; set; }

        public BudgetAccount BudgetAccount { get; set; }
    }
}
