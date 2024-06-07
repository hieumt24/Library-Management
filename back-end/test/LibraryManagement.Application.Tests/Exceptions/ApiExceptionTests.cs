// ApiExceptionTests.cs
using LibraryManagement.Application.Exceptions;
using System;
using Xunit;

public class ApiExceptionTests
{
    [Fact]
    public void ApiException_DefaultConstructor_ShouldCreateInstance()
    {
        // Arrange & Act
        var exception = new ApiException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void ApiException_MessageConstructor_ShouldCreateInstanceWithMessage()
    {
        // Arrange
        var message = "Test Message";

        // Act
        var exception = new ApiException(message);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void ApiException_FormatConstructor_ShouldCreateInstanceWithFormattedMessage()
    {
        // Arrange
        var format = "Test {0}";
        var arg1 = "Message";
        var expectedMessage = string.Format(format, arg1);

        // Act
        var exception = new ApiException(format, arg1);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(expectedMessage, exception.Message);
    }
}