using LibraryManagement.Domain.Common.Models;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.DTOs.Books.Requests
{
    public class BookBorrowingRequestDetails : BaseEntity
    {
        public int BookBorrowingRequestId { get; set; }
        public int Quantity { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public int BookId { get; set; }

        //Navigation property
        public BookBorrowingRequest? BookBorrowingRequest { get; set; }

        public Book Book { get; set; }
    }
}