using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public class Director : IDirector
    {
        public WalletDto Construct(IWalletBuilder builder, WalletDto model) => builder.BuildScheme(model);
    }
}
