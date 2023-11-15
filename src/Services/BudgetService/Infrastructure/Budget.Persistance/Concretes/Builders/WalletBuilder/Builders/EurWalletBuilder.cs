using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder.Builders
{
    public class EurWalletBuilder : IWalletBuilder
    {
        private WalletCreateDto _wallet;

        public void BuildCurrency() => _wallet.Currency = Domain.Enums.Currency.EUR;

        public void BuildTotal() => _wallet.Total = 0;

        public WalletCreateDto GetWallet() => _wallet;

        public WalletCreateDto BuildScheme(WalletCreateDto model)
        {
            _wallet = model;

            BuildCurrency();
            BuildTotal();

            return GetWallet();
        }
    }
}
