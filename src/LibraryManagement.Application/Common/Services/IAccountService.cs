using LibraryManagement.Application.Models.DTOs.Account;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Common.Services
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);

        Task<Response<string>> RegisterAsync(RegisterRequest request);

        Task<Response<string>> ConfirmEmailAsync(string userId, string code);

        Task ForgotPassword(ForgotPasswordRequest model);

        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    }
}