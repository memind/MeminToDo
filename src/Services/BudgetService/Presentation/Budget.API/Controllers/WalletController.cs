using Budget.Application.Abstractions.Services;
using Budget.Application.DTOs.WalletDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _wallet;

        public WalletController(IWalletService wallet) => _wallet = wallet;

        [HttpGet]
        [Authorize(Policy = "BudgetRead")]
        public List<WalletDto> GetAllWallets() => _wallet.GetAllWallets();

        [HttpGet("/user/{userId}")]
        [Authorize(Policy = "BudgetRead")]
        public List<WalletDto> GetUsersAllWallets(Guid userId) => _wallet.GetUsersAllWallets(userId);

        [HttpGet("/noTracking/{walletId}")]
        [Authorize(Policy = "BudgetRead")]
        public WalletDto GetWalletByIdAsNoTracking(Guid walletId) => _wallet.GetWalletByIdAsNoTracking(walletId);

        [HttpGet("/{walletId}")]
        [Authorize(Policy = "BudgetRead")]
        public WalletDto GetWalletById(Guid walletId) => _wallet.GetWalletById(walletId);

        [HttpPost]
        [Authorize(Policy = "BudgetWrite")]
        public int CreateWallet(WalletDto model) => _wallet.CreateWallet(model);

        [HttpPut]
        [Authorize(Policy = "BudgetWrite")]
        public int UpdateWallet(WalletDto model) => _wallet.UpdateWallet(model);

        [HttpDelete]
        [Authorize(Policy = "BudgetWrite")]
        public int DeleteWallet(Guid walletId) => _wallet.DeleteWallet(walletId);
    }
}
