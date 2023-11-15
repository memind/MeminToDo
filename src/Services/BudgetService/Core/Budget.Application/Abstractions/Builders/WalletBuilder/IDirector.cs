using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public interface IDirector
    {
        public WalletDto Construct(IWalletBuilder builder, WalletDto model);
    }
}
