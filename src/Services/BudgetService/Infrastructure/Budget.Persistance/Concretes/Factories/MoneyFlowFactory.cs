using Budget.Application.Abstractions.Factories;
using Budget.Application.DTOs.MoneyFlowDTOs;
using Budget.Domain.Entities;
using Budget.Domain.Enums;
using Budget.Persistance.Consts;

namespace Budget.Persistance.Concretes.Factories
{
    public class MoneyFlowFactory : IMoneyFlowFactory
    {
        public MoneyFlowDto CreateMoneyFlowMessage(MoneyFlowDto moneyFlow)
        {
            if (moneyFlow.Type == MoneyFlowType.Income)
                moneyFlow.Message = MoneyFlowMessages.IncomeMessage();
            

            if (moneyFlow.Type == MoneyFlowType.Expense)
                moneyFlow.Message = MoneyFlowMessages.ExpenseMessage();
            
            return moneyFlow;
        }
    }
}
