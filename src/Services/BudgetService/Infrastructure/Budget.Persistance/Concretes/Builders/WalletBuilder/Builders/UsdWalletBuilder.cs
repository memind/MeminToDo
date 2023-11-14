using Budget.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Budget.Application.Abstractions.Builders.WalletBuilder.Builders
{
    public class UsdWalletBuilder : IWalletBuilder
    {
        private Wallet _wallet = new Wallet();

        public void BuildCurrency() => _wallet.Currency = Domain.Enums.Currency.USD;

        public void BuildTotal() => _wallet.Total = 0;
        
        public void BuildWalletName(string name) => _wallet.WalletName = name;

        public Wallet GetWallet() => _wallet;

        public Wallet BuildScheme(string name)
        {
            BuildCurrency();
            BuildTotal();
            BuildWalletName(name);

            return GetWallet();
        }
    }
}
