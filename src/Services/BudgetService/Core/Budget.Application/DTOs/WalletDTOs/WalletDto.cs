using Budget.Domain.Entities;
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

        public BudgetAccount BudgetAccount { get; set; }
    }
}
