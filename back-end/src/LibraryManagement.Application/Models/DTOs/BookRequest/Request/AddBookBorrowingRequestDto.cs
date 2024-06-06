using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Models.DTOs.BookRequest.Request
{
    public class AddBookBorrowingRequestDto
    {
        [Required]
        public string RequesterId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateRequested { get; set; } = DateTime.Now;

        public ICollection<AddBookBorrowingRequestDetailsDto> requestDetailsDtos { get; set; }
    }
}