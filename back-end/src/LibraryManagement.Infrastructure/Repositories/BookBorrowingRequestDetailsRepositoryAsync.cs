using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookBorrowingRequestDetailsRepositoryAsync : BaseRepositoryAsync<BookBorrowingRequestDetails>, IBookBorrowingRequestDetailsRepositoryAsync
    {
        public BookBorrowingRequestDetailsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}