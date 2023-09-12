using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace User.Domain.Entities
{
    public class AppUser
    {
        public AppUser()
        {
            Roles = new List<string>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(30, ErrorMessage = "First name must be shorter than 30 characters!")]
        [MinLength(2, ErrorMessage = "First name must be longer than 2 characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(50, ErrorMessage = "Last name must be shorter than 50 characters!")]
        [MinLength(2, ErrorMessage = "Last name must be longer than 2 characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birth date is required!")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [MaxLength(30, ErrorMessage = "Username must be shorter than 30 characters!")]
        [MinLength(2, ErrorMessage = "Username must be longer than 2 characters!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password must be longer than 8 characters!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Enter a valid email address!")]
        public string Email { get; set; }

        public List<string>? Roles { get; set; }
    }
}
