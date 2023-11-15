using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder.Builders
{
    public class GbpWalletBuilder : IWalletBuilder
    {
        private WalletDto _wallet;

        public void BuildCurrency() => _wallet.Currency = Domain.Enums.Currency.GBP;

        public void BuildTotal() => _wallet.Total = 0;

        public WalletDto GetWallet() => _wallet;

        public WalletDto BuildScheme(WalletDto model)
        {
            _wallet = model;

            BuildCurrency();
            BuildTotal();

            return GetWallet();
        }
    }
}
