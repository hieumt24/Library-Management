using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.DTOs.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Models.DTOs.BookRequest.Response;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Common.Services
{
    public interface IBookBorrowRequestServiceAsync
    {
        Task<Response<BookBorrowingResponseDto>> AddBookBorrowRequestAsync(AddBookBorrowingRequestDto request);

        Task<Response<PagedResponse<List<BookBorrowingResponseDto>>>> GetAllBookBorrowingRequest(RequestStatus? status, int page, int limit);

        Task<Response<BookBorrowingRequestDto>> UpdateBookBorrowingRequestAsync(Guid id, UpdateBookBorrowingRequestDto updateBookBorrowingRequestDto);

        Task<Response<BookBorrowingRequestDto>> DeleteBookBorrowingRequestAsync(Guid id);

        Task<Response<BookBorrowingResponseDto>> GetBookBorrowingRequestById(Guid id);

        Task<Response<List<BookBorrowingResponseDto>>> GetBookBorrowingRequestByRegisterId(string? requesterId);
    }
}