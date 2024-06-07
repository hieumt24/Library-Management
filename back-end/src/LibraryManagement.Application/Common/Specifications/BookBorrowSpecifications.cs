using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Domain.Common.Specifications;

namespace LibraryManagement.Application.Common.Specifications
{
    public class BookBorrowSpecifications
    {
        public static BaseSpecification<BookBorrowingRequest> GetAllBookBorrowRequest(RequestStatus? status)
        {
            var spec = new BaseSpecification<BookBorrowingRequest>(x => !x.IsDeleted);
            if (status != null)
            {
                spec = new BaseSpecification<BookBorrowingRequest>(x => !x.IsDeleted && x.Status == status.Value);
            }
            spec.AddInclude(x => x.Requester);
            spec.AddInclude(x => x.Approver);
            return spec;
        }

        public static BaseSpecification<BookBorrowingRequest> GetBookBorrowRequestById(Guid id)
        {
            var spec = new BaseSpecification<BookBorrowingRequest>(x => !x.IsDeleted && x.Id == id);
            spec.AddInclude(x => x.Requester);
            spec.AddInclude(x => x.Approver);

            return spec;
        }

        public static BaseSpecification<BookBorrowingRequest> GetBookBorrowRequestByRegisterId(string? requesterId)
        {
            var spec = new BaseSpecification<BookBorrowingRequest>(x => !x.IsDeleted && x.RequesterId == requesterId);
            spec.AddInclude(x => x.Requester);
            spec.AddInclude(x => x.Approver);
            return spec;
        }

        public static BaseSpecification<BookBorrowingRequest> CheckValidBookBorrowRequest(string requesterId, DateTime dateRequest)
        {
            var spec = new BaseSpecification<BookBorrowingRequest>(x => !x.IsDeleted && x.RequesterId == requesterId && (x.DateRequested.Month == dateRequest.Month));
            return spec;
        }
    }
}