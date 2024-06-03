using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.Identity;
using LibraryManagement.Domain.Common.Models;

<<<<<<<< HEAD:src/LibraryManagement.Application/Models/BookRequest/BookBorrowingRequest.cs
namespace LibraryManagement.Application.Models.BookRequest
========
namespace LibraryManagement.Application.Models.DTOs.Books.Request
>>>>>>>> d27a830a6df6256e681481fecb324138e493606f:src/LibraryManagement.Application/Models/DTOs/Books/Request/BookBorrowingRequest.cs
{
    public class BookBorrowingRequest : BaseEntity
    {
        public string RequesterId { get; set; }  // Changed to string
        public DateTime DateRequested { get; set; }
        public RequestStatus Status { get; set; }
        public string? ApproverId { get; set; }  // Changed to string

        //Navigation properties
        public ApplicationUser Requester { get; set; }

        public ApplicationUser? Approver { get; set; }
        public ICollection<BookBorrowingRequestDetails> RequestDetails { get; set; }
    }
}