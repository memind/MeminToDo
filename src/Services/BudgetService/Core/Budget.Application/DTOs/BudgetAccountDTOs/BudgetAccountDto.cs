using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.DTOs.BudgetAccountDTOs
{
    public class BudgetAccountDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public List<MoneyFlowDto> MoneyFlows { get; set; } = new List<MoneyFlowDto>();
        public List<WalletDto> Wallets { get; set; } = new List<WalletDto>();
    }
}
