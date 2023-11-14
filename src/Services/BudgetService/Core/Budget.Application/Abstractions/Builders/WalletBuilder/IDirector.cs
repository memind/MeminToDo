using Budget.Domain.Entities;

namespace Budget.Application.Abstractions.Builders.WalletBuilder
{
    public interface IDirector
    {
        public Wallet Construct(IWalletBuilder builder, string name);
    }
}
