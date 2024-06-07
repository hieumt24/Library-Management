// LibraryRolesTests.cs
using LibraryManagement.Application.Enums;
using Xunit;

public class LibraryRolesTests
{
    [Fact]
    public void UserRole_ShouldBeCorrect()
    {
        // Assert
        Assert.Equal("User", LibraryRoles.User);
    }

    [Fact]
    public void SuperUserRole_ShouldBeCorrect()
    {
        // Assert
        Assert.Equal("SuperUser", LibraryRoles.SuperUser);
    }
}