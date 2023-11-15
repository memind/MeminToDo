using Budget.Application.DTOs.BudgetAccountDTOs;
using Budget.Domain.Enums;

namespace Budget.Application.DTOs.WalletDTOs
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string WalletName { get; set; }
        public Currency Currency { get; set; }
        public int Total { get; set; }
        public Guid BudgetAccountId { get; set; }

        public BudgetAccountDto BudgetAccount { get; set; }
    }
}
