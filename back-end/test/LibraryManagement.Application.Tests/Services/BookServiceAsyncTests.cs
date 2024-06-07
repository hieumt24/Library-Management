using AutoMapper;
using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using Moq;
using LibraryManagement.Domain.Common.Specifications;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Models.BookRequest;
using System.Linq.Expressions;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Tests.Services
{
    public class BookServiceAsyncTests
    {
        [Fact]
        public async Task DeleteBookAsync_WhenBookExists_ShouldReturnSuccessfulResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var bookId = Guid.NewGuid();

            // Mock the repository to return a valid book when getById is called
            mockRepository.Setup(repo => repo.GetByIdAsync(bookId))
                          .ReturnsAsync(new Book { Id = bookId });

            // Mock the repository to return a valid book when deleted
            mockRepository.Setup(repo => repo.DeleteAsync(bookId))
                          .ReturnsAsync(new Book { Id = bookId });

            // Mock the mapper to return a valid BookDto
            mockMapper.Setup(mapper => mapper.Map<BookDto>(It.IsAny<Book>()))
                      .Returns(new BookDto());

            // Act
            var response = await service.DeleteBookAsync(bookId);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task DeleteBookAsync_WhenBookDoesNotExist_ShouldReturnErrorResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var bookId = Guid.NewGuid();

            // Mock the repository to return null when getById is called
            mockRepository.Setup(repo => repo.GetByIdAsync(bookId))
                          .ReturnsAsync((Book)null);

            // Act
            var response = await service.DeleteBookAsync(bookId);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Book not found", response.Message);
        }

        [Fact]
        public async Task DeleteBookAsync_WhenExceptionThrown_ShouldThrowException()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var bookId = Guid.NewGuid();

            // Mock the repository to throw an exception when getById is called
            mockRepository.Setup(repo => repo.GetByIdAsync(bookId))
                          .ThrowsAsync(new Exception("Test Exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.DeleteBookAsync(bookId));
        }

        [Fact]
        public async Task GetAllBookAsync_ShouldReturnPagedResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var page = 1;
            var limit = 10;

            // Mock data
            var booksDomain = new List<Book>(); // Mock list of books
            var totalRecords = 20; // Mock total records

            // Mock repository method calls
            mockRepository.Setup(repo => repo.CountAsync(It.IsAny<ISpecification<Book>>()))
                          .ReturnsAsync(totalRecords);
            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<ISpecification<Book>>()))
                          .ReturnsAsync(booksDomain);

            // Mock mapper
            mockMapper.Setup(mapper => mapper.Map<List<BookResponseDto>>(It.IsAny<List<Book>>()))
                       .Returns(new List<BookResponseDto>()); // Mock mapped list of BookResponseDto

            // Act
            var response = await service.GetAllBookAsync(page, limit);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(page, response.Data.PageNumber); // Updated to use PageNumber property
            Assert.Equal(limit, response.Data.PageSize); // Updated to use PageSize property
            Assert.Equal(totalRecords, response.Data.TotalRecords);
            Assert.Empty(response.Data.Data); // Ensure data is empty for mock setup
        }

        [Fact]
        public async Task GetAllBookAsync_WhenExceptionThrown_ShouldThrowException()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var page = 1;
            var limit = 10;

            // Mock repository method calls to throw an exception
            mockRepository.Setup(repo => repo.CountAsync(It.IsAny<ISpecification<Book>>()))
                          .ThrowsAsync(new Exception("Test Exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetAllBookAsync(page, limit));
        }

        [Fact]
        public async Task GetBookByIdAsync_WhenBookExists_ShouldReturnBookResponseDto()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var id = Guid.NewGuid();

            // Mock data
            var bookDomain = new Book(); // Mock book domain object
            var bookResponseDto = new BookResponseDto(); // Mock book response DTO

            // Mock repository method call
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Book>>()))
                          .ReturnsAsync(bookDomain);

            // Mock mapper
            mockMapper.Setup(mapper => mapper.Map<BookResponseDto>(It.IsAny<Book>()))
                      .Returns(bookResponseDto);

            // Act
            var response = await service.GetBookByIdAsync(id);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(bookResponseDto, response.Data);
            Assert.True(response.Succeeded);
            Assert.Null(response.Message);
        }

        [Fact]
        public async Task GetBookByIdAsync_WhenBookDoesNotExist_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var id = Guid.NewGuid();

            // Mock repository method call
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Book>>()))
                          .ReturnsAsync((Book)null);

            // Act
            var response = await service.GetBookByIdAsync(id);

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.False(response.Succeeded);
            Assert.Equal("Book not found", response.Message);
        }

        [Fact]
        public async Task GetBookByIdAsync_WhenExceptionThrown_ShouldThrowException()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var id = Guid.NewGuid();

            // Mock repository method call to throw an exception
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Book>>()))
                          .ThrowsAsync(new Exception("An error occurred"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetBookByIdAsync(id));
        }

        [Fact]
        public async Task UpdateBookAsync_WhenBookNotFound_ShouldReturnBookNotFoundResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var id = Guid.NewGuid();
            var request = new UpdateBookRequestDto { /* Populate with valid data */ };

            mockRepository.Setup(repo => repo.GetByIdAsync(id))
                          .ReturnsAsync((Book)null); // Simulate book not found

            // Act
            var response = await service.UpdateBookAsync(id, request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Book not found", response.Message);
            Assert.Null(response.Data);
            // Add additional assertions if needed
        }

        [Fact]
        public async Task UpdateBookAsync_WhenBookExists_ShouldReturnUpdatedBook()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);

            var id = Guid.NewGuid();
            var request = new UpdateBookRequestDto
            {
                Title = "Original Title",
                Author = "Updated Author",
                Price = 2,
                Description = "Updated Description",
                QuantityInStock = 50,
                Language = "Updated Language",
                PublicationYear = 2023,
                CategoryId = Guid.NewGuid(),
            };

            var existingBook = new Book
            {
                Id = id,
                Title = "Original Title",
                Author = "Original Author",
                Price = 15,
                Description = "Original Description",
                QuantityInStock = 20,
                Language = "Original Language",
                PublicationYear = 2020,
                CategoryId = Guid.NewGuid(),
                IsDeleted = false
            };

            var updatedBook = new Book
            {
                Id = id,
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Description = request.Description,
                QuantityInStock = request.QuantityInStock,
                Language = request.Language,
                PublicationYear = request.PublicationYear,
                CategoryId = request.CategoryId,
                IsDeleted = false
            };

            var updatedBookDto = new BookDto
            {
                Id = id,
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Description = request.Description,
                QuantityInStock = request.QuantityInStock,
                Language = request.Language,
                PublicationYear = request.PublicationYear,
                CategoryId = request.CategoryId,
            };

            // Mock IMapper Setup
            mockMapper.Setup(mapper => mapper.Map<Book>(It.IsAny<UpdateBookRequestDto>()))
                      .Returns(updatedBook);
            mockRepository.Setup(repo => repo.GetByIdAsync(id))
                          .ReturnsAsync(existingBook);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Book>()))
                          .ReturnsAsync(updatedBook);
            mockMapper.Setup(mapper => mapper.Map<BookDto>(It.IsAny<Book>()))
                      .Returns(updatedBookDto);

            // Act
            var response = await service.UpdateBookAsync(id, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(updatedBookDto, response.Data);
        }

        [Fact]
        public async Task AddBookAsync_WhenBookDoesNotExist_ShouldReturnSuccessfulResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var request = new AddBookRequestDto
            {
                Title = "Example Title",
                Author = "Example Author",
                Price = 22,
                Description = "Example Description",
                QuantityInStock = 100,
                Language = "English",
                PublicationYear = 2022,
                CategoryId = Guid.NewGuid() // Assuming you have a valid category ID
            };

            // Mock the repository to return null when the book does not exist
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Book>>()))
                          .ReturnsAsync((Book)null);

            // Mock the repository to return the added book
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>()))
                          .ReturnsAsync(new Book { /* Populate with valid data */ });

            // Mock the mapper to return a valid BookDto
            mockMapper.Setup(mapper => mapper.Map<BookDto>(It.IsAny<Book>()))
                      .Returns(new BookDto { /* Populate with valid data */ });

            // Act
            var response = await service.AddBookAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            // Add additional assertions if needed
        }

        [Fact]
        public async Task AddBookAsync_WhenBookExists_ShouldReturnErrorResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookRepositoryAsync>();
            var service = new BookServiceAsync(mockRepository.Object, mockMapper.Object);
            var request = new AddBookRequestDto
            {
                Title = "Example Title",
                Author = "Example Author",
                Price = 22,
                Description = "Example Description",
                QuantityInStock = 100,
                Language = "English",
                PublicationYear = 2022,
                CategoryId = Guid.NewGuid() // Assuming you have a valid category ID
            };

            // Mock the repository to return an existing book
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Book>>()))
              .ReturnsAsync(new Book
              {
                  Id = Guid.NewGuid(), // Assuming you have a valid book ID
                  Title = "Existing Title",
                  Author = "Existing Author",
                  Price = 22,
                  Description = "Existing Description",
                  QuantityInStock = 50,
                  Language = "French",
                  PublicationYear = 2019,
                  CategoryId = Guid.NewGuid(), // Assuming you have a valid category ID
                  IsDeleted = false
              });

            // Act
            var response = await service.AddBookAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Null(response.Data);
            // Add additional assertions if needed
        }
    }
}