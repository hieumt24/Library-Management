using LibraryManagement.Application.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Identity.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}