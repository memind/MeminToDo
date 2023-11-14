using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.DTOs.BudgetAccountDTOs
{
    public class BudgetAccountUpdateDto
    {
        public Guid Id { get; set; }
        public List<MoneyFlowDto> MoneyFlows { get; set; } = new List<MoneyFlowDto>();
        public List<WalletDto> Wallets { get; set; } = new List<WalletDto>();
    }
}
