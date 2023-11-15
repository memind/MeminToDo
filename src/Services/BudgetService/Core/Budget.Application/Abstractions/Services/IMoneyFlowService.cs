using Budget.Application.DTOs.MoneyFlowDTOs;

namespace Budget.Application.Abstractions.Services
{
    public interface IMoneyFlowService
    {
        int CreateMoneyFlow(MoneyFlowCreateDto model);
        int UpdateMoneyFlow(MoneyFlowUpdateDto model);
        int DeleteMoneyFlow(Guid id);
        MoneyFlowDto GetMoneyFlowById(Guid id);
        MoneyFlowDto GetMoneyFlowByIdAsNoTracking(Guid id);
        List<MoneyFlowDto> GetAllMoneyFlows();
        List<MoneyFlowDto> GetUsersAllMoneyFlows(Guid userId);
    }
}
