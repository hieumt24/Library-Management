using LibraryManagement.Application.Models.DTOs.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Common.Services
{
    public interface IBookBorrowRequestServiceAsync
    {
        Task<Response<BookBorrowingRequestDto>> AddBookBorrowRequestAsync(AddBookBorrowingRequestDto request);
    }
}