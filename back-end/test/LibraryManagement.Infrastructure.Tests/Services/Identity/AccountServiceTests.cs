using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Models.DTOs.Account;
using LibraryManagement.Application.Models.Identity;
using LibraryManagement.Domain.Common.Settings;
using LibraryManagement.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Tests.Services.Identity
{
    public class AccountServiceTests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private JWTSettings _jwtSettings;

        public AccountServiceTests(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Fact]
        public async Task AuthenticateAsync_Throws_ApiException_WhenNoAccountRegisteredWithEmail()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(userManagerMock.Object, null, null, null, null, null, null);
            var jwtSettingsMock = new Mock<IOptions<JWTSettings>>();

            var service = new AccountService(userManagerMock.Object, signInManagerMock.Object, jwtSettingsMock.Object);

            var request = new AuthenticationRequest { Email = "test@example.com", Password = "password" };

            userManagerMock.Setup(m => m.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => service.AuthenticateAsync(request));
        }

        [Fact]
        public async Task AuthenticateAsync_Throws_ApiException_WhenInvalidAuthentication()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(userManagerMock.Object, null, null, null, null, null, null);
            var jwtSettingsMock = new Mock<IOptions<JWTSettings>>();

            var service = new AccountService(userManagerMock.Object, signInManagerMock.Object, jwtSettingsMock.Object);

            var request = new AuthenticationRequest { Email = "test@example.com", Password = "password" };
            var user = new ApplicationUser();

            userManagerMock.Setup(m => m.FindByEmailAsync(request.Email)).ReturnsAsync(user);
            signInManagerMock.Setup(m => m.PasswordSignInAsync(user.UserName, request.Password, false, false)).ReturnsAsync(SignInResult.Failed);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => service.AuthenticateAsync(request));
        }
    }
}