using LibraryManagement.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application.Tests
{
    public class ServiceExtensionsTests
    {
        [Fact]
        public void Configure_ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            ServiceExtensions.Configure(services);

            // Assert
            var serviceProvider = services.BuildServiceProvider();

            // Verify that the services are registered
            Assert.NotNull(serviceProvider.GetRequiredService<ICategoryServiceAsync>());
            Assert.NotNull(serviceProvider.GetRequiredService<IBookServiceAsync>());
            Assert.NotNull(serviceProvider.GetRequiredService<IBookBorrowRequestServiceAsync>());
            Assert.NotNull(serviceProvider.GetRequiredService<IBookBorrowRequestDetailsServiceAsync>());

            // Verify AutoMapper registration
            Assert.NotNull(serviceProvider.GetRequiredService<AutoMapper.IMapper>());
        }
    }
}