using Budget.Domain.Entities;
using Budget.Domain.Enums;

namespace Budget.Application.DTOs.WalletDTOs
{
    public class WalletCreateDto
    {
        public string WalletName { get; set; }
        public Currency Currency { get; set; }
    }
}
