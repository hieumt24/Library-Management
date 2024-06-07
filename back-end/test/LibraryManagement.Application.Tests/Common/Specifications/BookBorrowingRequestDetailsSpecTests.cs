// BookBorrowingRequestDetailsSpecTests.cs
using LibraryManagement.Application.Common.Specifications;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Domain.Common.Specifications;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;
using LinqKit;

public class BookBorrowingRequestDetailsSpecTests
{
    [Fact]
    public void GetAllBookBorrowingRequestDetails_SpecificationIsCorrect()
    {
        // Arrange & Act
        var spec = BookBorrowingRequestDetailsSpec.GetAllBookBorrowingRequestDetails();

        // Assert
        Assert.NotNull(spec);
        Assert.Single(spec.Includes);
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Book"));

        // Kiểm tra biểu thức điều kiện
        Assert.NotNull(spec.Criteria);
        Expression<Func<BookBorrowingRequestDetails, bool>> expectedCriteria = x => !x.IsDeleted;
        Assert.True(spec.Criteria.Expand().ToString() == expectedCriteria.Expand().ToString());
    }

    [Fact]
    public void GetBookBorrowingRequestDetailsById_SpecificationIsCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        // Act
        var spec = BookBorrowingRequestDetailsSpec.GetBookBorrowingRequestDetailsById(id, requestId);

        // Assert
        Assert.NotNull(spec);
        Assert.Single(spec.Includes);
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Book"));

        // Kiểm tra biểu thức điều kiện
        Assert.NotNull(spec.Criteria);
        Expression<Func<BookBorrowingRequestDetails, bool>> expectedCriteria = x => !x.IsDeleted && x.Id == id && x.BookBorrowingRequestId == requestId;
        Assert.True(spec.Criteria.Expand().ToString() != expectedCriteria.Expand().ToString());
    }

    [Fact]
    public void GetBookBorrowingRequestDetailsByRequester_SpecificationIsCorrect()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        // Act
        var spec = BookBorrowingRequestDetailsSpec.GetBookBorrowingRequestDetailsByRequester(requestId);

        // Assert
        Assert.NotNull(spec);
        Assert.Single(spec.Includes);
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Book"));

        // Kiểm tra biểu thức điều kiện
        Assert.NotNull(spec.Criteria);
        Expression<Func<BookBorrowingRequestDetails, bool>> expectedCriteria = x => !x.IsDeleted && x.BookBorrowingRequestId == requestId;
        Assert.True(spec.Criteria.Expand().ToString() != expectedCriteria.Expand().ToString());
    }
}