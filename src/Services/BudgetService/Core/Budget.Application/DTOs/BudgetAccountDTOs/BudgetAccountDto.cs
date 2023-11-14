using Budget.Domain.Entities;

namespace Budget.Application.DTOs.BudgetAccountDTOs
{
    public class BudgetAccountDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public List<MoneyFlow> MoneyFlows { get; set; } = new List<MoneyFlow>();
        public List<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
