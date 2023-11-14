using Budget.Domain.Entities;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public interface IWalletBuilder
    {
        public void BuildCurrency();
        public void BuildTotal();
        public void BuildWalletName(string name);
        public Wallet BuildScheme(string name);
        public Wallet GetWallet();
    }
}
