// RequestStatusTests.cs
using LibraryManagement.Application.Enums;
using Xunit;

public class RequestStatusTests
{
    [Fact]
    public void RequestStatus_Approved_ShouldHaveCorrectValue()
    {
        // Arrange & Act
        var approvedStatus = RequestStatus.Approved;

        // Assert
        Assert.Equal(RequestStatus.Approved, approvedStatus);
    }

    [Fact]
    public void RequestStatus_Rejected_ShouldHaveCorrectValue()
    {
        // Arrange & Act
        var rejectedStatus = RequestStatus.Rejected;

        // Assert
        Assert.Equal(RequestStatus.Rejected, rejectedStatus);
    }

    [Fact]
    public void RequestStatus_Waiting_ShouldHaveCorrectValue()
    {
        // Arrange & Act
        var waitingStatus = RequestStatus.Waiting;

        // Assert
        Assert.Equal(RequestStatus.Waiting, waitingStatus);
    }
}