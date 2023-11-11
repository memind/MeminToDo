namespace HomePages.Models
{
    public class UserLoginVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
