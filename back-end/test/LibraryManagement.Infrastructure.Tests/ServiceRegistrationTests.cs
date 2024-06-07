using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Models.Identity;
using LibraryManagement.Domain.Common.Settings;
using LibraryManagement.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Enums;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Infrastructure.Tests
{
    public class ServiceRegistrationTests
    {
        [Fact]
        public void ConfigureServices_RegistersRequiredServices()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build(); // Add any necessary configuration here

            // Act
            ServiceRegistration.ConfigureServices(services, configuration);
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            Assert.NotNull(serviceProvider.GetService<ApplicationDbContext>());
            Assert.NotNull(serviceProvider.GetService<ICategoryRepositoryAsync>());
            Assert.NotNull(serviceProvider.GetService<IBookRepositoryAsync>());
            Assert.NotNull(serviceProvider.GetService<IAccountService>());
            Assert.NotNull(serviceProvider.GetService<IBookBorrowingRequestRepositoryAsync>());
            Assert.NotNull(serviceProvider.GetService<IBookBorrowingRequestDetailsRepositoryAsync>());
        }

        [Fact]
        public void ConfigureServices_ConfiguresJWTAuthentication()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            ServiceRegistration.ConfigureServices(services, configuration);

            var serviceProvider = services.BuildServiceProvider();

            var jwtSettings = configuration.GetSection("JWTSettings").Get<JWTSettings>();
            var jwtBearerOptions = serviceProvider.GetService<IOptionsMonitor<JwtBearerOptions>>();

            // Assert
            Assert.NotNull(jwtBearerOptions);
            Assert.Equal(jwtSettings.Issuer, jwtBearerOptions.CurrentValue.TokenValidationParameters.ValidIssuer);
            Assert.Equal(jwtSettings.Audience, jwtBearerOptions.CurrentValue.TokenValidationParameters.ValidAudience);
            Assert.IsType<SymmetricSecurityKey>(jwtBearerOptions.CurrentValue.TokenValidationParameters.IssuerSigningKey);

            var symmetricSecurityKey = jwtBearerOptions.CurrentValue.TokenValidationParameters.IssuerSigningKey as SymmetricSecurityKey;
            Assert.Equal(jwtSettings.Key, Encoding.UTF8.GetString(symmetricSecurityKey.Key));
        }

        [Fact]
        public void ConfigureServices_ConfiguresAuthorizationPolicies()
        {
            // Arrange
            var services = new ServiceCollection();
            ServiceRegistration.ConfigureServices(services, new ConfigurationBuilder().Build());
            var serviceProvider = services.BuildServiceProvider();

            var authorizationOptions = serviceProvider.GetService<Microsoft.AspNetCore.Authorization.AuthorizationOptions>();

            // Assert
            Assert.NotNull(authorizationOptions);

            var policy = authorizationOptions.GetPolicy("RequireAdminRole");
            Assert.NotNull(policy);
        }
    }
}