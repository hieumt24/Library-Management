using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Identity.Models;
using LibraryManagement.Domain.Common.Models;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.DTOs.Books.Requests
{
    public class BookBorrowingRequest : BaseEntity
    {
        public string RequesterId { get; set; }  // Changed to string
        public DateTime DateRequested { get; set; }
        public RequestStatus Status { get; set; }
        public string? ApproverId { get; set; }  // Changed to string

        //Navigation properties
        public User Requester { get; set; }

        public User? Approver { get; set; }
        public ICollection<BookBorrowingRequestDetails> RequestDetails { get; set; }
    }
}