using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Models.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountServiceAsync;

        public AccountController(IAccountService accountServiceAsync)
        {
            _accountServiceAsync = accountServiceAsync;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _accountServiceAsync.AuthenticateAsync(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _accountServiceAsync.RegisterAsync(request);

            return Ok(response);
        }
    }
}