namespace LibraryManagement.Application.Models.DTOs.BookBorrowDetails.Response
{
    public class BookBorrowingDetailsResponseDto
    {
        public Guid Id { get; set; }
        public string BookBorrowingRequestId { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public Guid BookId { get; set; }
        public string? Title { get; set; }
    }
}