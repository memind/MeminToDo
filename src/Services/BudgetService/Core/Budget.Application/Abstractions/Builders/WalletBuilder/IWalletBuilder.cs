using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public interface IWalletBuilder
    {
        public void BuildCurrency();
        public void BuildTotal();
        public WalletDto BuildScheme(WalletDto model);
        public WalletDto GetWallet();
    }
}
