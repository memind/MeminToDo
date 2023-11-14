using Budget.Domain.Entities;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public class Director : IDirector
    {
        public Wallet Construct(IWalletBuilder builder, string name) => builder.BuildScheme(name);
    }
}
