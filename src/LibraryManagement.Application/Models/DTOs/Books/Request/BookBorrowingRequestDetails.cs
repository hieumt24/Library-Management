using LibraryManagement.Domain.Common.Models;
using LibraryManagement.Domain.Entities;

<<<<<<<< HEAD:src/LibraryManagement.Application/Models/BookRequest/BookBorrowingRequestDetails.cs
namespace LibraryManagement.Application.Models.BookRequest
========
namespace LibraryManagement.Application.Models.DTOs.Books.Request
>>>>>>>> d27a830a6df6256e681481fecb324138e493606f:src/LibraryManagement.Application/Models/DTOs/Books/Request/BookBorrowingRequestDetails.cs
{
    public class BookBorrowingRequestDetails : BaseEntity
    {
        public Guid BookBorrowingRequestId { get; set; }
        public int Quantity { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public Guid BookId { get; set; }

        //Navigation property
        public BookBorrowingRequest? BookBorrowingRequest { get; set; }

        public Book Book { get; set; }
    }
}