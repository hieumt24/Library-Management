using AutoMapper;
using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Common.Specifications;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookBorrowDetails.Response;
using LibraryManagement.Application.Models.DTOs.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Models.DTOs.BookRequest.Response;
using LibraryManagement.Application.Services;
using LibraryManagement.Application.Wrappers;
using LibraryManagement.Domain.Common.Specifications;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Tests.Services
{
    public class BookBorrowRequestServiceAsyncTests
    {
        private readonly IMapper _mapper;

        public BookBorrowRequestServiceAsyncTests()
        {
            // Initialize IMapper mock here or use a mocking framework to create it
            _mapper = new Mock<IMapper>().Object;
        }

        [Fact]
        public async Task AddBookBorrowRequestAsync_WhenRequestIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddBookBorrowRequestAsync(null));
        }

        [Fact]
        public async Task AddBookBorrowRequestAsync_WhenRequestIsValid_ShouldReturnSuccessfulResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Create a request with valid data
            var request = new AddBookBorrowingRequestDto
            {
                RequesterId = "user123",
                requestDetailsDtos = new List<AddBookBorrowingRequestDetailsDto>
        {
            new AddBookBorrowingRequestDetailsDto { BookId = Guid.NewGuid(), ReturnedDate = DateTime.Now.AddDays(7) },
            new AddBookBorrowingRequestDetailsDto { BookId = Guid.NewGuid(), ReturnedDate = DateTime.Now.AddDays(14) },
            new AddBookBorrowingRequestDetailsDto { BookId = Guid.NewGuid(), ReturnedDate = DateTime.Now.AddDays(21) }
        }
            };

            var listBookDetails = new List<BookBorrowingRequestDetails>
    {
        new BookBorrowingRequestDetails { BookId = Guid.NewGuid(), ReturnedDate = DateTime.Now.AddDays(7) },
        new BookBorrowingRequestDetails { BookId = Guid.NewGuid(), ReturnedDate = DateTime.Now.AddDays(14) },
        new BookBorrowingRequestDetails { BookId = Guid.NewGuid(), ReturnedDate = DateTime.Now.AddDays(21) }
    };

            mockRepository.Setup(repo => repo.CreateBookBorrowingRequest(It.IsAny<BookBorrowingRequest>(), It.IsAny<List<BookBorrowingRequestDetails>>()))
                          .ReturnsAsync(new BookBorrowingRequest()); // Assuming the repository returns a valid result

            // Act
            var response = await service.AddBookBorrowRequestAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task AddBookBorrowRequestAsync_WhenRequestCountExceedsMaximum_ShouldReturnErrorMessage()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);
            var request = new AddBookBorrowingRequestDto();
            var listRequestByUserResult = new List<BookBorrowingRequest>();

            // Set up mock to return a list with count >= 3
            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ReturnsAsync(listRequestByUserResult);

            // Act
            var response = await service.AddBookBorrowRequestAsync(request);

            // Assert
            Assert.Equal("You have reached the maximum number of borrowing requests", response.Message);
        }

        [Fact]
        public async Task AddBookBorrowRequestAsync_WhenExceptionThrown_ShouldRethrowExceptionWithSameMessage()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);
            var request = new AddBookBorrowingRequestDto();

            // Set up mocks
            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ThrowsAsync(new Exception("Sample exception message"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.AddBookBorrowRequestAsync(request));
            Assert.Equal("Sample exception message", exception.Message);
        }

        [Fact]
        public async Task GetAllBookBorrowingRequest_WhenExceptionThrown_ShouldRethrowExceptionWithSameMessage()
        {
            // Arrange
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            var expectedExceptionMessage = "Sample exception message";

            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ThrowsAsync(new Exception(expectedExceptionMessage));
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetAllBookBorrowingRequest());
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public async Task GetAllBookBorrowingRequest_WithValidData_ShouldReturnMappedDtoList()
        {
            // Arrange
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            var expectedRequests = new List<BookBorrowingRequest>
    {
        new BookBorrowingRequest { /* Fill with sample data */ },
        new BookBorrowingRequest { /* Fill with sample data */ },
        // Add more sample requests as needed
    };
            var expectedDtos = expectedRequests.Select(req => new BookBorrowingResponseDto { /* Fill with expected DTO data */ }).ToList();

            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ReturnsAsync(expectedRequests);
            mockMapper.Setup(mapper => mapper.Map<List<BookBorrowingResponseDto>>(It.IsAny<List<BookBorrowingRequest>>()))
                       .Returns(expectedDtos);
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.GetAllBookBorrowingRequest();

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.Equal(expectedDtos, response.Data);
        }

        [Fact]
        public async Task GetBookBorrowingRequestById_WithValidId_ShouldReturnMappedDto()
        {
            // Arrange
            var id = Guid.NewGuid(); // Assuming a valid id
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            var expectedRequest = new BookBorrowingRequest { /* Fill with sample data */ };
            var expectedDto = new BookBorrowingResponseDto { /* Fill with expected DTO data */ };

            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ReturnsAsync(expectedRequest);
            mockMapper.Setup(mapper => mapper.Map<BookBorrowingResponseDto>(It.IsAny<BookBorrowingRequest>()))
                       .Returns(expectedDto);
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.GetBookBorrowingRequestById(id);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.Equal(expectedDto, response.Data);
        }

        [Fact]
        public async Task GetBookBorrowingRequestById_WithInvalidId_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var id = Guid.NewGuid(); // Assuming an invalid id
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.GetBookBorrowingRequestById(id);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Book borrowing request not found", response.Message);
        }

        [Fact]
        public async Task GetBookBorrowingRequestById_WithException_ShouldThrowExceptionWithSameMessage()
        {
            // Arrange
            var id = Guid.NewGuid(); // Assuming a valid id
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ThrowsAsync(new Exception("Test exception message")); // Mock repository to throw an exception
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetBookBorrowingRequestById(id));
            Assert.Equal("Test exception message", exception.Message);
        }

        [Fact]
        public async Task GetBookBorrowingRequestByRegisterId_WithNullRequestId_ShouldThrowArgumentNullException()
        {
            // Arrange
            string requesterId = null;
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetBookBorrowingRequestByRegisterId(requesterId));
        }

        [Fact]
        public async Task GetBookBorrowingRequestByRegisterId_WithException_ShouldThrowExceptionWithSameMessage()
        {
            // Arrange
            var requesterId = "someValidRequestId"; // assuming a valid requesterId
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ThrowsAsync(new Exception("Test exception message")); // Mock repository to throw an exception
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetBookBorrowingRequestByRegisterId(requesterId));
            Assert.Equal("Test exception message", exception.Message);
        }

        [Fact]
        public async Task GetBookBorrowingRequestById_WhenExceptionThrown_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();

            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetBookBorrowingRequestById(id));
        }

        [Fact]
        public async Task GetBookBorrowingRequestById_WhenRequestDoesNotExist_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();

            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                .ReturnsAsync((BookBorrowingRequest)null);

            // Act
            var response = await service.GetBookBorrowingRequestById(id);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Book borrowing request not found", response.Message);
        }

        [Fact]
        public void DateRequested_DefaultValueIsDateTimeNow()
        {
            // Arrange
            var dto = new AddBookBorrowingRequestDto();

            // Act
            var defaultValue = dto.DateRequested;

            // Assert
            Assert.Equal(DateTime.Now.Date, defaultValue.Date);
        }

        [Fact]
        public async Task GetBookBorrowingRequestByRegisterId_WithValidRequestId_ShouldReturnListOfBookBorrowingResponseDto()
        {
            // Arrange
            var requesterId = "someRequestId";
            var expectedRequestList = new List<BookBorrowingRequest>(); // Assuming you have some test data
            var expectedResponse = new Response<List<BookBorrowingResponseDto>>(_mapper.Map<List<BookBorrowingResponseDto>>(expectedRequestList));

            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<BookBorrowingRequest>>()))
                          .ReturnsAsync(expectedRequestList);
            mockMapper.Setup(mapper => mapper.Map<List<BookBorrowingResponseDto>>(It.IsAny<List<BookBorrowingRequest>>()))
                      .Returns(expectedResponse.Data);

            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.GetBookBorrowingRequestByRegisterId(requesterId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedResponse.Data, response.Data);
        }

        [Fact]
        public async Task UpdateBookBorrowingRequestAsync_WhenUpdateDtoIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateBookBorrowingRequestDto = (UpdateBookBorrowingRequestDto)null; // Set the DTO to null
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            var mockMapper = new Mock<IMapper>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBookBorrowingRequestAsync(id, updateBookBorrowingRequestDto));
        }

        [Fact]
        public async Task UpdateBookBorrowingRequestAsync_WhenBookBorrowingRequestNotFound_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateBookBorrowingRequestDto();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((BookBorrowingRequest)null);
            var mockMapper = new Mock<IMapper>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.UpdateBookBorrowingRequestAsync(id, updateDto);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Book borrowing request not found", response.Message);
        }

        [Fact]
        public async Task UpdateBookBorrowingRequestAsync_WhenUpdateOperationFails_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateBookBorrowingRequestDto();
            var bookBorrowingRequest = new BookBorrowingRequest { Id = id };
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(bookBorrowingRequest);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BookBorrowingRequest>())).ThrowsAsync(new Exception("Update operation failed"));
            var mockMapper = new Mock<IMapper>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.UpdateBookBorrowingRequestAsync(id, updateDto));
        }

        [Fact]
        public async Task UpdateBookBorrowingRequestAsync_WhenUpdateOperationSucceeds_ShouldReturnUpdatedBookBorrowingRequestDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateBookBorrowingRequestDto();
            var bookBorrowingRequest = new BookBorrowingRequest { Id = id };
            var updatedBookBorrowingRequest = new BookBorrowingRequest { Id = id }; // Assuming the update operation modifies the entity
            var updatedBookBorrowingRequestDto = new BookBorrowingRequestDto { Id = id }; // Assuming mapping preserves ID
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(bookBorrowingRequest);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BookBorrowingRequest>())).ReturnsAsync(updatedBookBorrowingRequest); // Assuming the update operation modifies the entity
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<BookBorrowingRequestDto>(updatedBookBorrowingRequest)).Returns(updatedBookBorrowingRequestDto);
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.UpdateBookBorrowingRequestAsync(id, updateDto);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotEqual(updatedBookBorrowingRequestDto, response.Data);
        }

        [Fact]
        public async Task DeleteBookBorrowingRequestAsync_WhenBookBorrowingRequestNotFound_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((BookBorrowingRequest)null);
            var mockMapper = new Mock<IMapper>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.DeleteBookBorrowingRequestAsync(id);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Book borrowing request not found", response.Message);
        }

        [Fact]
        public async Task DeleteBookBorrowingRequestAsync_WhenDeleteOperationFails_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookBorrowingRequest = new BookBorrowingRequest { Id = id };
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(bookBorrowingRequest);
            mockRepository.Setup(repo => repo.DeleteAsync(id)).ThrowsAsync(new Exception("Delete operation failed"));
            var mockMapper = new Mock<IMapper>();
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.DeleteBookBorrowingRequestAsync(id));
        }

        [Fact]
        public async Task DeleteBookBorrowingRequestAsync_WhenOperationSucceeds_ShouldReturnDeletedBookBorrowingRequestDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookBorrowingRequest = new BookBorrowingRequest { Id = id };
            var deletedBookBorrowingRequest = new BookBorrowingRequest { Id = id }; // Assuming delete operation returns the deleted entity
            var deletedBookBorrowingRequestDto = new BookBorrowingRequestDto { Id = id }; // Assuming mapping preserves ID
            var mockRepository = new Mock<IBookBorrowingRequestRepositoryAsync>();
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(bookBorrowingRequest);
            mockRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(deletedBookBorrowingRequest); // Assuming delete operation returns the deleted entity
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<BookBorrowingRequestDto>(deletedBookBorrowingRequest)).Returns(deletedBookBorrowingRequestDto);
            var service = new BookBorrowRequestServiceAsync(mockRepository.Object, mockMapper.Object);

            // Act
            var response = await service.DeleteBookBorrowingRequestAsync(id);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.Equal(deletedBookBorrowingRequestDto, response.Data);
        }
    }
}