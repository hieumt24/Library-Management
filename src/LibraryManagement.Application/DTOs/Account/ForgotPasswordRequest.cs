using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.Account
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}