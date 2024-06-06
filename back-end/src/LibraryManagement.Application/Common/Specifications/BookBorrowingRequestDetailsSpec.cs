using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Domain.Common.Specifications;

namespace LibraryManagement.Application.Common.Specifications
{
    public class BookBorrowingRequestDetailsSpec
    {
        public static BaseSpecification<BookBorrowingRequestDetails> GetAllBookBorrowingRequestDetails()
        {
            var spec = new BaseSpecification<BookBorrowingRequestDetails>(x => !x.IsDeleted);
            spec.AddInclude(x => x.Book);
            return spec;
        }

        public static BaseSpecification<BookBorrowingRequestDetails> GetBookBorrowingRequestDetailsById(Guid id, Guid bookBorrowingRequestId)
        {
            var spec = new BaseSpecification<BookBorrowingRequestDetails>(x => !x.IsDeleted && x.Id == id && x.BookBorrowingRequestId == bookBorrowingRequestId);
            spec.AddInclude(x => x.Book);
            return spec;
        }

        public static BaseSpecification<BookBorrowingRequestDetails> GetBookBorrowingRequestDetailsByRequester(Guid bookBorrowingRequestId)
        {
            var spec = new BaseSpecification<BookBorrowingRequestDetails>(x => !x.IsDeleted && x.BookBorrowingRequestId == bookBorrowingRequestId);
            spec.AddInclude(x => x.Book);
            return spec;
        }
    }
}