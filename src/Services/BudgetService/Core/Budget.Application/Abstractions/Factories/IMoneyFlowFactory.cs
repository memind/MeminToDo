using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Domain.Enums;

namespace Budget.Application.Abstractions.Factories
{
    public interface IMoneyFlowFactory
    {
        MoneyFlowDto CreateMoneyFlowMessage(MoneyFlowDto moneyFlowType);
    }
}
