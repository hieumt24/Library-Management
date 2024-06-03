using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Application.Models.Identity;
using LibraryManagement.Domain.Common.Models;

namespace LibraryManagement.Application.Models.DTOs.BookRequest
{
    public class BookBorrowingRequestDto : BaseEntity
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