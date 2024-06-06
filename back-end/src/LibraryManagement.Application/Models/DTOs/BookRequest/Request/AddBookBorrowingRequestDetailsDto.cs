using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Models.DTOs.BookRequest.Request
{
    public class AddBookBorrowingRequestDetailsDto
    {
        [DataType(DataType.Date)]
        public DateTime? ReturnedDate { get; set; }

        public Guid BookId { get; set; }
    }
}