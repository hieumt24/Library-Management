using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Models.DTOs.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Services
{
    public class BookBorrowRequestService : IBookBorrowRequestServiceAsync
    {
        private readonly IBookBorrowingRequestRepositoryAsync _bookBorrowingRequestRepositoryAsync;

        public BookBorrowRequestService(IBookBorrowingRequestRepositoryAsync bookBorrowingRequestRepositoryAsync)
        {
            _bookBorrowingRequestRepositoryAsync = bookBorrowingRequestRepositoryAsync;
        }

        public Task<Response<BookBorrowingRequestDto>> AddBookBorrowRequestAsync(AddBookBorrowingRequestDto request)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}