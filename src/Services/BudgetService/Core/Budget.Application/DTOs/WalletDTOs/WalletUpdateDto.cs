namespace Budget.Application.DTOs.WalletDTOs
{
    public class WalletUpdateDto
    {
        public Guid Id { get; set; }
        public string WalletName { get; set; }
        public int Total { get; set; }
    }
}
