using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Response;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookBorrowingRequestRepositoryAsync : BaseRepositoryAsync<BookBorrowingRequest>, IBookBorrowingRequestRepositoryAsync
    {
        public BookBorrowingRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BookBorrowingRequest> CreateBookBorrowingRequest(BookBorrowingRequest bookBorrowingRequest, List<BookBorrowingRequestDetails> bookBorrowingRequestDetails)
        {
            if (bookBorrowingRequest == null)
            {
                throw new AggregateException(nameof(bookBorrowingRequest));
            }

            //var listRequestByUser = await _dbContext.BookBorrowingRequests.Where(x => x.RequesterId == bookBorrowingRequest.RequesterId && (x.DateRequested.Month.Equals(x.DateRequested.Month))).ToListAsync();
            //if (listRequestByUser.Count >= 3)
            //{
            //    return null;
            //}
            //if (bookBorrowingRequestDetails.Count() >= 5)
            //{
            //    return null;
            //}
            await _dbContext.BookBorrowingRequests.AddAsync(bookBorrowingRequest);
            await _dbContext.SaveChangesAsync();

            foreach (var item in bookBorrowingRequestDetails)
            {
                await _dbContext.BookBorrowingRequestDetails.AddAsync(new BookBorrowingRequestDetails
                {
                    BookBorrowingRequestId = bookBorrowingRequest.Id,
                    BookId = item.BookId,
                    ReturnedDate = item.ReturnedDate,
                    BorrowedDate = DateTime.Now
                });
                await _dbContext.SaveChangesAsync();
            }

            return bookBorrowingRequest;
        }
    }
}