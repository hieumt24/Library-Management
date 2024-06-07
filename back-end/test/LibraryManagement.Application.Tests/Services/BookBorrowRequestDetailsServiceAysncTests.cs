// BookBorrowRequestDetailsServiceAysncTests.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Models.DTOs.BookBorrowDetails.Response;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Common.Specifications;
using Moq;
using Xunit;
using LibraryManagement.Application.Models.BookRequest;

public class BookBorrowRequestDetailsServiceAysncTests
{
    [Fact]
    public async Task GetAllBorrowingRequestDetails_ShouldReturnCorrectResponse()
    {
        // Arrange
        var mockRepository = new Mock<IBookBorrowingRequestDetailsRepositoryAsync>();
        var mockMapper = new Mock<IMapper>();

        var bookBorrowingDetails = new List<BookBorrowingRequestDetails>(); // Assuming you have some test data
        var bookBorrowingDetailsDto = new List<BookBorrowingDetailsResponseDto>(); // Assuming you have corresponding DTOs

        mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequestDetails>>()))
            .ReturnsAsync(bookBorrowingDetails);
        mockMapper.Setup(mapper => mapper.Map<List<BookBorrowingDetailsResponseDto>>(It.IsAny<List<BookBorrowingRequestDetails>>()))
            .Returns(bookBorrowingDetailsDto);

        var service = new BookBorrowRequestDetailsServiceAysnc(mockRepository.Object, mockMapper.Object);

        // Act
        var response = await service.GetAllBorrowingRequestDetails();

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Succeeded);
        Assert.Equal(bookBorrowingDetailsDto, response.Data);
    }

    [Fact]
    public async Task GetBorrowingRequestDetailsById_ShouldReturnCorrectResponse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        var mockRepository = new Mock<IBookBorrowingRequestDetailsRepositoryAsync>();
        var mockMapper = new Mock<IMapper>();

        var bookBorrowingDetails = new BookBorrowingRequestDetails(); // Assuming you have some test data
        var bookBorrowingDetailsDto = new BookBorrowingDetailsResponseDto(); // Assuming you have corresponding DTO

        mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<BookBorrowingRequestDetails>>()))
            .ReturnsAsync(bookBorrowingDetails);
        mockMapper.Setup(mapper => mapper.Map<BookBorrowingDetailsResponseDto>(It.IsAny<BookBorrowingRequestDetails>()))
            .Returns(bookBorrowingDetailsDto);

        var service = new BookBorrowRequestDetailsServiceAysnc(mockRepository.Object, mockMapper.Object);

        // Act
        var response = await service.GetBorrowingRequestDetailsById(id, requestId);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Succeeded);
        Assert.Equal(bookBorrowingDetailsDto, response.Data);
    }

    [Fact]
    public async Task GetBorrowingRequestDetailsByRequester_ShouldReturnCorrectResponse()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        var mockRepository = new Mock<IBookBorrowingRequestDetailsRepositoryAsync>();
        var mockMapper = new Mock<IMapper>();

        var bookBorrowingDetails = new List<BookBorrowingRequestDetails>(); // Assuming you have some test data
        var bookBorrowingDetailsDto = new List<BookBorrowingDetailsResponseDto>(); // Assuming you have corresponding DTOs

        mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequestDetails>>()))
            .ReturnsAsync(bookBorrowingDetails);
        mockMapper.Setup(mapper => mapper.Map<List<BookBorrowingDetailsResponseDto>>(It.IsAny<List<BookBorrowingRequestDetails>>()))
            .Returns(bookBorrowingDetailsDto);

        var service = new BookBorrowRequestDetailsServiceAysnc(mockRepository.Object, mockMapper.Object);

        // Act
        var response = await service.GetBorrowingRequestDetailsByRequester(requestId);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Succeeded);
        Assert.Equal(bookBorrowingDetailsDto, response.Data);
    }

    [Fact]
    public async Task GetBorrowingRequestDetailsById_ShouldReturnNotFoundResponseWhenDetailsIsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        var mockRepository = new Mock<IBookBorrowingRequestDetailsRepositoryAsync>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<BookBorrowingRequestDetails>>()))
            .ReturnsAsync((BookBorrowingRequestDetails)null);

        var service = new BookBorrowRequestDetailsServiceAysnc(mockRepository.Object, mockMapper.Object);

        // Act
        var response = await service.GetBorrowingRequestDetailsById(id, requestId);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Succeeded);
        Assert.Equal("Book borrowing details not found", response.Message);
    }

    [Fact]
    public async Task GetBorrowingRequestDetailsByRequester_ShouldReturnNotFoundResponseWhenDetailsIsNull()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        var mockRepository = new Mock<IBookBorrowingRequestDetailsRepositoryAsync>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequestDetails>>()))
            .ReturnsAsync((List<BookBorrowingRequestDetails>)null);

        var service = new BookBorrowRequestDetailsServiceAysnc(mockRepository.Object, mockMapper.Object);

        // Act
        var response = await service.GetBorrowingRequestDetailsByRequester(requestId);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Succeeded);
        Assert.Equal("Book borrowing details not found", response.Message);
    }

    [Fact]
    public async Task GetBorrowingRequestDetailsById_ShouldThrowExceptionWhenRepositoryThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        var mockRepository = new Mock<IBookBorrowingRequestDetailsRepositoryAsync>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<BookBorrowingRequestDetails>>()))
            .ThrowsAsync(new Exception("Test exception"));

        var service = new BookBorrowRequestDetailsServiceAysnc(mockRepository.Object, mockMapper.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => service.GetBorrowingRequestDetailsById(id, requestId));
    }

    [Fact]
    public async Task GetBorrowingRequestDetailsByRequester_ShouldThrowExceptionWhenRepositoryThrowsException()
    {
        // Arrange
        var requestId = Guid.NewGuid();

        var mockRepository = new Mock<IBookBorrowingRequestDetailsRepositoryAsync>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequestDetails>>()))
            .ThrowsAsync(new Exception("Test exception"));

        var service = new BookBorrowRequestDetailsServiceAysnc(mockRepository.Object, mockMapper.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => service.GetBorrowingRequestDetailsByRequester(requestId));
    }
}