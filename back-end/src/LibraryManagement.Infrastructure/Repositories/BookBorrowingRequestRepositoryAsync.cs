using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookBorrowingRequestRepositoryAsync : BaseRepositoryAsync<BookBorrowingRequest>, IBookBorrowingRequestRepositoryAsync
    {
        public BookBorrowingRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}