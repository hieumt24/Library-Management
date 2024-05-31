using LibraryManagement.Application.Models.DTOs.Account;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Common.Services
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);

        Task<Response<string>> ConfirmEmailAsync(Guid userId, string code);

        Task ForgotPassword(ForgotPasswordRequest model, string origin);

        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    }
}