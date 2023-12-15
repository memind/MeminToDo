namespace Common.Logging.Logs.BudgetLogs
{
    public static class BudgetLogs
    {
        public static string AnErrorOccured(string errorMessage) => $"An error occured: {errorMessage}";
        

        public static string CreateBudgetAccount(Guid id) => $"Created Budget Account: {id}";
        

        public static string DeleteBudgetAccount(Guid id) => $"Deleted Budget Account Successfully: {id}";
        

        public static string GetAllBudgetAccounts() => $"Getting All Budget Accounts Successfully.";
        

        public static string GetBudgetAccountById(Guid id) =>  $"Getting Budget Account With Tracking: {id}";

        
        public static string GetUsersAllBudgetAccounts(Guid id) => $"Getting All Budget Accounts Of User ({id}) Successfully.";


        public static string UpdateBudgetAccount(Guid id) => $"Updated Budget Account Successfully: {id}";


        public static string CreateMoneyFlow(Guid id) => $"Created Money Flow: {id}";


        public static string DeleteMoneyFlow(Guid id) => $"Deleted Money Flow Successfully: {id}";


        public static string GetAllMoneyFlows() => $"Getting All Money Flows Successfully.";


        public static string GetMoneyFlowById(Guid id) => $"Getting Money Flow With Tracking: {id}";


        public static string GetMoneyFlowByIdAsNoTracking(Guid id) => $"Getting Money Flow Without Tracking: {id}";


        public static string GetUsersAllMoneyFlows(Guid id) => $"Getting All Money Flows Of User ({id}) Successfully.";


        public static string UpdateMoneyFlow(Guid id) => $"Updated Money Flow Successfully: {id}";


        public static string CreateWallet(Guid id) => $"Created Wallet: {id}";


        public static string DeleteWallet(Guid id) => $"Deleted Wallet Successfully: {id}";


        public static string GetAllWallets() => $"Getting All Wallets Successfully.";


        public static string GetWalletById(Guid id) => $"Getting Wallet With Tracking: {id}";


        public static string GetWalletByIdAsNoTracking(Guid id) => $"Getting Wallet Without Tracking: {id}";


        public static string GetUsersAllWallets(Guid id) => $"Getting All Wallets Of User ({id}) Successfully.";


        public static string UpdateWallet(Guid id) => $"Updated Wallet Successfully: {id}";

    }
}
