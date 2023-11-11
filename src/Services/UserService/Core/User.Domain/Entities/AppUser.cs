using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace User.Domain.Entities
{
    public class AppUser : IdentityUser
    {
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

        public string Role { get; set; }


        [Required(ErrorMessage = "Industry is required!")]
        [MaxLength(50, ErrorMessage = "Industry must be shorter than 50 characters!")]
        [MinLength(2, ErrorMessage = "Industry must be longer than 2 characters!")]
        public string Industry { get; set; }


        [Required(ErrorMessage = "Wage is required!")]
        [Range(0,1000000,ErrorMessage = "Wage must be in between 0 and 1,000,000!")]
        public int Wage { get; set; }


        [Required(ErrorMessage = "Position is required!")]
        [MaxLength(50, ErrorMessage = "Position must be shorter than 50 characters!")]
        [MinLength(2, ErrorMessage = "Position must be longer than 2 characters!")]
        public string Position { get; set; }


        [Required(ErrorMessage = "Authority is required!")]
        [MaxLength(50, ErrorMessage = "Authority must be shorter than 50 characters!")]
        [MinLength(2, ErrorMessage = "Authority must be longer than 2 characters!")]
        public string Authority { get; set; }


        [Required(ErrorMessage = "WorkingAt is required!")]
        [MaxLength(50, ErrorMessage = "WorkingAt must be shorter than 50 characters!")]
        [MinLength(2, ErrorMessage = "WorkingAt must be longer than 2 characters!")]
        public string WorkingAt { get; set; }
    }
}
