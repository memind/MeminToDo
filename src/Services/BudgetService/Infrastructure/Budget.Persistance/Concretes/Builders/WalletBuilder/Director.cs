using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public class Director : IDirector
    {
        public WalletCreateDto Construct(IWalletBuilder builder, WalletCreateDto model) => builder.BuildScheme(model);
    }
}
