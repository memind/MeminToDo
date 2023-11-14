using Budget.Domain.Entities;

namespace Budget.Application.Abstractions.Builders.WalletBuilder.Builders
{
    public class GbpWalletBuilder : IWalletBuilder
    {
        private Wallet _wallet = new Wallet();

        public void BuildCurrency() => _wallet.Currency = Domain.Enums.Currency.GBP;

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
