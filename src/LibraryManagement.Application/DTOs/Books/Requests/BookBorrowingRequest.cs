using LibraryManagement.Application.Enums;
using LibraryManagement.Domain.Common.Models;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.DTOs.Books.Requests
{
    public class BookBorrowingRequest : BaseEntity
    {
        public int RequesterId { get; set; }
        public DateTime DateRequested { get; set; }
        public RequestStatus Status { get; set; }
        public int? ApproverId { get; set; }

        //Navigation properties
        public User Requester { get; set; }

        public User Approver { get; set; }
        public ICollection<BookBorrowingRequestDetails> RequestDetails { get; set; }
    }
}