using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.DTOs.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Models.DTOs.BookRequest.Response;
using LibraryManagement.Application.Wrappers;
using LibraryManagement.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagement.WebApi.Tests.Controllers
{
    public class BookBorrowingRequestControllerTests
    {
        private readonly Mock<IBookBorrowRequestServiceAsync> mockBookBorrowRequestService;

        public BookBorrowingRequestControllerTests()
        {
            mockBookBorrowRequestService = new Mock<IBookBorrowRequestServiceAsync>();
        }

        [Fact]
        public async Task GetAll_Returns_OkResult_With_Response()
        {
            // Arrange
            var mockBookBorrowRequestService = new Mock<IBookBorrowRequestServiceAsync>();
            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);
            var responseDto = new BookBorrowingResponseDto(); // Assuming this is your response DTO
            var response = new Response<BookBorrowingResponseDto>(responseDto, message: "Book borrowing requests retrieved successfully.");

            mockBookBorrowRequestService.Setup(service => service.GetAllBookBorrowingRequest())
                            .ReturnsAsync(new Response<List<BookBorrowingResponseDto>>(new List<BookBorrowingResponseDto>(), "Book borrowing requests retrieved successfully."));

            // Act
            var result = await controller.GetAll() as OkObjectResult;
            var responseData = result.Value as Response<BookBorrowingResponseDto>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(responseData);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Book borrowing requests retrieved successfully.", responseData.Message);
            Assert.Equal(responseDto, responseData.Data); // Optionally assert response data
        }

        [Fact]
        public async Task GetAll_Returns_BadRequest_When_Message_Present()
        {
            // Arrange
            var mockBookBorrowRequestService = new Mock<IBookBorrowRequestServiceAsync>();
            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);
            var response = new Response<BookBorrowingResponseDto>(null, message: "Error occurred while retrieving book borrowing requests.");

            mockBookBorrowRequestService.Setup(service => service.GetAllBookBorrowingRequest())
                            .ReturnsAsync(new Response<List<BookBorrowingResponseDto>>(new List<BookBorrowingResponseDto>(), "Book borrowing requests retrieved successfully."));

            // Act
            var result = await controller.GetAll() as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Error occurred while retrieving book borrowing requests.", result.Value);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockBookBorrowRequestService = new Mock<IBookBorrowRequestServiceAsync>();
            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);
            var id = Guid.NewGuid();
            var bookBorrowingResponseDto = new BookBorrowingResponseDto { Id = id, /* other properties */ };
            var successResponse = new Response<BookBorrowingResponseDto>(bookBorrowingResponseDto);

            mockBookBorrowRequestService.Setup(service => service.GetBookBorrowingRequestById(id))
                                        .ReturnsAsync(successResponse);

            // Act
            var result = await controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<BookBorrowingResponseDto>>(okResult.Value);
            Assert.Equal(bookBorrowingResponseDto.Id, model.Data.Id);
            // Add more assertions as needed based on the expected behavior
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var mockBookBorrowRequestService = new Mock<IBookBorrowRequestServiceAsync>();
            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);
            var id = Guid.NewGuid();
            var errorMessage = "Request not found.";
            var errorResponse = new Response<BookBorrowingResponseDto>(errorMessage);

            mockBookBorrowRequestService.Setup(service => service.GetBookBorrowingRequestById(id))
                                        .ReturnsAsync(errorResponse);

            // Act
            var result = await controller.GetById(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task Create_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var mockBookBorrowRequestService = new Mock<IBookBorrowRequestServiceAsync>();
            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);
            var addRequestDto = new AddBookBorrowingRequestDto
            {
                RequesterId = "requesterId",
                DateRequested = DateTime.Now,
                requestDetailsDtos = new List<AddBookBorrowingRequestDetailsDto>
                {
                    new AddBookBorrowingRequestDetailsDto { /* details properties */ }
                }
            };
            var successResponse = new Response<BookBorrowingResponseDto>(new BookBorrowingResponseDto { /* response properties */ });

            mockBookBorrowRequestService.Setup(service => service.AddBookBorrowRequestAsync(addRequestDto))
                                        .ReturnsAsync(successResponse);

            // Act
            var result = await controller.Create(addRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<BookBorrowingResponseDto>>(okResult.Value);
            Assert.NotNull(model.Data);
            // Add more assertions as needed based on the expected behavior
        }

        [Fact]
        public async Task Create_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var mockBookBorrowRequestService = new Mock<IBookBorrowRequestServiceAsync>();
            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);
            var addRequestDto = new AddBookBorrowingRequestDto(); // Missing required fields
            var errorMessage = "Invalid request.";
            var errorResponse = new Response<BookBorrowingResponseDto>(errorMessage);

            mockBookBorrowRequestService.Setup(service => service.AddBookBorrowRequestAsync(addRequestDto))
                                        .ReturnsAsync(errorResponse);

            // Act
            var result = await controller.Create(addRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var updateRequestDto = new UpdateBookBorrowingRequestDto
            {
                RequesterId = "requesterId",
                Status = RequestStatus.Approved,
                ApproverId = "approverId"
            };

            var updatedResponse = new Response<BookBorrowingRequestDto>(new BookBorrowingRequestDto(), message: "Updated successfully");

            mockBookBorrowRequestService.Setup(service => service.UpdateBookBorrowingRequestAsync(It.IsAny<Guid>(), updateRequestDto))
                                        .ReturnsAsync(updatedResponse);

            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);

            // Act
            var result = await controller.Update(Guid.NewGuid(), updateRequestDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var deleteResponse = new Response<BookBorrowingRequestDto>(new BookBorrowingRequestDto(), message: "Deleted successfully");

            mockBookBorrowRequestService.Setup(service => service.DeleteBookBorrowingRequestAsync(id))
                                        .ReturnsAsync(deleteResponse);

            var controller = new BookBorrowingRequestController(mockBookBorrowRequestService.Object);

            // Act
            var result = await controller.Delete(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenResponseHasMessage()
        {
            // Arrange
            var id = Guid.NewGuid();
            var message = "Error deleting book borrowing request";
            var mockService = new Mock<IBookBorrowRequestServiceAsync>();
            mockService.Setup(service => service.DeleteBookBorrowingRequestAsync(id))
                       .ReturnsAsync(new Response<BookBorrowingRequestDto> { Message = message });
            var controller = new BookBorrowingRequestController(mockService.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(message, badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenResponseIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mockService = new Mock<IBookBorrowRequestServiceAsync>();
            mockService.Setup(service => service.DeleteBookBorrowingRequestAsync(id))
                       .ReturnsAsync(new Response<BookBorrowingRequestDto>());
            var controller = new BookBorrowingRequestController(mockService.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}