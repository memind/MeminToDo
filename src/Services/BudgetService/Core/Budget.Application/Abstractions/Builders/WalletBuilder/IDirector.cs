using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public interface IDirector
    {
        public WalletCreateDto Construct(IWalletBuilder builder, WalletCreateDto model);
    }
}
