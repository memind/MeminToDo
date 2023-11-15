using Budget.Domain.Entities;
using Budget.Domain.Enums;

namespace Budget.Application.DTOs.MoneyFlowDTOs
{
    public class MoneyFlowUpdateDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public Guid BudgetAccountId { get; set; }

        public Guid UserId { get; set; }
        public Currency Currency { get; set; }
        public MoneyFlowType Type { get; set; }
        public string? Message { get; set; }
    }
}
