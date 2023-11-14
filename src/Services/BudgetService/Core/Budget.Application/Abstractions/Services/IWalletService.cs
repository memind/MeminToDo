using Budget.Application.DTOs.WalletDTOs;

namespace Budget.Application.Abstractions.Services
{
    public interface IWalletService
    {
        int CreateWallet(WalletCreateDto model);
        int UpdateWallet(WalletUpdateDto model);
        int DeleteWallet(Guid id);
        WalletDto GetWalletById(Guid id);
        List<WalletDto> GetAllWallets();
    }
}
