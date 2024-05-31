using LibraryManagement.Application.Models.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Models.Identity
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters.")]
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}