using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Response;

namespace LibraryManagement.Application.Common.Repositories
{
    public interface IBookBorrowingRequestRepositoryAsync : IBaseRepositoryAsync<BookBorrowingRequest>
    {
        Task<BookBorrowingRequest> CreateBookBorrowingRequest(BookBorrowingRequest bookBorrowingRequest, List<BookBorrowingRequestDetails> bookBorrowingRequestDetails);
    }
}