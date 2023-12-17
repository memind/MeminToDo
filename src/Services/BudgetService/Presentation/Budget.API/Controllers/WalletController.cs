using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.WalletDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _wallet;

        public WalletController(IWalletService wallet) => _wallet = wallet;

        [HttpGet]
        public List<WalletDto> GetAllWallets() => _wallet.GetAllWallets();

        [HttpGet("/user/{userId}")]
        public List<WalletDto> GetUsersAllWallets(Guid userId) => _wallet.GetUsersAllWallets(userId);

        [HttpGet("/noTracking/{walletId}")]
        public WalletDto GetWalletByIdAsNoTracking(Guid walletId) => _wallet.GetWalletByIdAsNoTracking(walletId);

        [HttpGet("/{walletId}")]
        public WalletDto GetWalletById(Guid walletId) => _wallet.GetWalletById(walletId);

        [HttpPost]
        public int CreateWallet(WalletDto model) => _wallet.CreateWallet(model);

        [HttpPut]
        public int UpdateWallet(WalletDto model) => _wallet.UpdateWallet(model);

        [HttpDelete]
        public int DeleteWallet(Guid walletId) => _wallet.DeleteWallet(walletId);
    }
}
