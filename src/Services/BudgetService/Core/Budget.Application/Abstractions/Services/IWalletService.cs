using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Services
{
    public interface IWalletService
    {
        int CreateWallet(WalletDto model);
        int UpdateWallet(WalletDto model);
        int DeleteWallet(Guid id);
        WalletDto GetWalletById(Guid id);
        WalletDto GetWalletByIdAsNoTracking(Guid id);
        List<WalletDto> GetAllWallets();
        List<WalletDto> GetUsersAllWallets(Guid userId);

        public void ConsumeBackUpInfo();
        public void ConsumeTestInfo();
    }
}
