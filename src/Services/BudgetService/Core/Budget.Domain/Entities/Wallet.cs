using Budget.Domain.Entities.Common;
using Budget.Domain.Enums;

namespace Budget.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public string WalletName { get; set; }
        public Currency Currency { get; set; }
        public int Total { get; set; }

        public BudgetAccount BudgetAccount { get; set; }
    }
}
