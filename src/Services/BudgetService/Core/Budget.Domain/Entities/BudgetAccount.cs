using Budget.Domain.Entities.Common;

namespace Budget.Domain.Entities
{
    public class BudgetAccount : BaseEntity
    {
        public Guid UserId { get; set; }
        public List<MoneyFlow> MoneyFlows { get; set; } = new List<MoneyFlow>();
        public List<Wallet> Wallets { get; set; } = new List<Wallet>(); 
    }
}
