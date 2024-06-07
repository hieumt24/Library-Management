using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Models.DTOs.Account;
using LibraryManagement.Application.Wrappers;
using LibraryManagement.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.WebApi.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Authenticate_Returns_OkResult_With_Response()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();
            var controller = new AccountController(mockAccountService.Object);
            var request = new AuthenticationRequest { Email = "test@example.com", Password = "password" };
            var response = new Response<AuthenticationResponse>(new AuthenticationResponse(), message: "Authenticated successfully.");

            mockAccountService.Setup(service => service.AuthenticateAsync(request))
                              .ReturnsAsync(response);

            // Act
            var result = await controller.Authenticate(request) as OkObjectResult;
            var responseData = result.Value as Response<AuthenticationResponse>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(responseData);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Authenticated successfully.", responseData.Message);
        }

        [Fact]
        public async Task Register_Returns_OkResult_With_Response()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();
            var controller = new AccountController(mockAccountService.Object);
            var request = new RegisterRequest { Email = "test@example.com", Password = "password", FirstName = "John", LastName = "Doe" };
            var response = new Response<string>("userId", message: "User Registered successfully.");

            mockAccountService.Setup(service => service.RegisterAsync(request))
                              .ReturnsAsync(response);

            // Act
            var result = await controller.Register(request) as OkObjectResult;
            var responseData = result.Value as Response<string>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(responseData);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("User Registered successfully.", responseData.Message);
        }
    }
}