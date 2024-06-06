using LibraryManagement.Application.Models.DTOs.BookBorrowDetails.Response;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Common.Services
{
    public interface IBookBorrowRequestDetailsServiceAsync
    {
        Task<Response<List<BookBorrowingDetailsResponseDto>>> GetAllBorrowingRequestDetails();

        Task<Response<BookBorrowingDetailsResponseDto>> GetBorrowingRequestDetailsById(Guid id, Guid bookBorrowingRequestId);

        Task<Response<List<BookBorrowingDetailsResponseDto>>> GetBorrowingRequestDetailsByRequester(Guid bookBorrowingRequestId);
    }
}