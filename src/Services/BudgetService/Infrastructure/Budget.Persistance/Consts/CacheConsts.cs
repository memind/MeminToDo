using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Persistance.Consts
{
    public static class CacheConsts
    {
        public static string GetAllWallets() => "GetAllWallets";
        public static string GetUsersAllWallets(Guid userId) => $"GetAllWalletsOfUser:{userId}";
        public static string GetWalletById(Guid id) => $"GetWalletById:{id}";
        public static string GetWalletByIdAsNoTracking(Guid id) => $"GetWalletByIdAsNoTracking:{id}";


        public static string GetAllMoneyFlows() => "GetAllMoneyFlows";
        public static string GetUsersAllMoneyFlows(Guid userId) => $"GetAllMoneyFlowsOfUser:{userId}";
        public static string GetMoneyFlowById(Guid id) => $"GetMoneyFlowById:{id}";
        public static string GetMoneyFlowByIdAsNoTracking(Guid id) => $"GetMoneyFlowByIdAsNoTracking:{id}";


        public static string GetAllBudgetAccounts() => "GetAllBudgetAccounts";
        public static string GetUsersAllBudgetAccounts(Guid userId) => $"GetAllBudgetAccountsOfUser:{userId}";
        public static string GetBudgetAccountById(Guid id) => $"GetBudgetAccountById:{id}";

    }
}
