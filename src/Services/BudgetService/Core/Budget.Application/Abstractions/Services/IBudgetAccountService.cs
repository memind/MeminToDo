using Budget.Application.DTOs.BudgetAccountDTOs;
using System.Linq.Expressions;

namespace Budget.Application.Abstractions.Services
{
    public interface IBudgetAccountService
    {
        int CreateBudgetAccount(BudgetAccountDto model);
        int UpdateBudgetAccount(BudgetAccountDto model);
        int DeleteBudgetAccount(Guid id);
        BudgetAccountDto GetBudgetAccountById(Guid id);
        List<BudgetAccountDto> GetAllBudgetAccounts();
        List<BudgetAccountDto> GetUsersAllBudgetAccounts(Guid id);

        public void ConsumeBackUpInfo();
        public void ConsumeTestInfo();
    }
}
