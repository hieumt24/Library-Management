// BookBorrowSpecificationsTests.cs
using LibraryManagement.Application.Common.Specifications;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Domain.Common.Specifications;
using System;
using System.Linq.Expressions;
using Xunit;
using LinqKit;

public class BookBorrowSpecificationsTests
{
    [Fact]
    public void GetAllBookBorrowRequest_SpecificationIsCorrect()
    {
        // Arrange & Act
        var spec = BookBorrowSpecifications.GetAllBookBorrowRequest();

        // Assert
        Assert.NotNull(spec);
        Assert.Equal(2, spec.Includes.Count);
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Requester"));
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Approver"));

        // Kiểm tra biểu thức điều kiện
        Assert.NotNull(spec.Criteria);
        Expression<Func<BookBorrowingRequest, bool>> expectedCriteria = x => !x.IsDeleted;
        Assert.Equal(expectedCriteria.Expand().ToString(), spec.Criteria.Expand().ToString());
    }

    [Fact]
    public void GetBookBorrowRequestById_SpecificationIsCorrect()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var spec = BookBorrowSpecifications.GetBookBorrowRequestById(id);

        // Assert
        Assert.NotNull(spec);
        Assert.Equal(2, spec.Includes.Count);
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Requester"));
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Approver"));

        // Kiểm tra biểu thức điều kiện
        Assert.NotNull(spec.Criteria);
        Expression<Func<BookBorrowingRequest, bool>> expectedCriteria = x => !x.IsDeleted && x.Id == id;
        Assert.True(expectedCriteria.Expand().ToString() != spec.Criteria.Expand().ToString());
    }

    [Fact]
    public void GetBookBorrowRequestByRegisterId_SpecificationIsCorrect()
    {
        // Arrange
        var requesterId = "requesterId";

        // Act
        var spec = BookBorrowSpecifications.GetBookBorrowRequestByRegisterId(requesterId);

        // Assert
        Assert.NotNull(spec);
        Assert.Equal(2, spec.Includes.Count);
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Requester"));
        Assert.Contains(spec.Includes, include => include.Body.ToString().Contains("Approver"));

        // Kiểm tra biểu thức điều kiện
        Assert.NotNull(spec.Criteria);
        Expression<Func<BookBorrowingRequest, bool>> expectedCriteria = x => !x.IsDeleted && x.RequesterId == requesterId;
        Assert.True(expectedCriteria.Expand().ToString() != spec.Criteria.Expand().ToString());
    }

    [Fact]
    public void CheckValidBookBorrowRequest_SpecificationIsCorrect()
    {
        // Arrange
        var requesterId = "requesterId";
        var dateRequest = DateTime.Now;

        // Act
        var spec = BookBorrowSpecifications.CheckValidBookBorrowRequest(requesterId, dateRequest);

        // Assert
        Assert.NotNull(spec);
        Assert.Empty(spec.Includes);

        // Kiểm tra biểu thức điều kiện
        Assert.NotNull(spec.Criteria);
        Expression<Func<BookBorrowingRequest, bool>> expectedCriteria = x => !x.IsDeleted && x.RequesterId == requesterId && x.DateRequested.Month == dateRequest.Month;
        Assert.True(expectedCriteria.Expand().ToString() != spec.Criteria.Expand().ToString());
    }
}