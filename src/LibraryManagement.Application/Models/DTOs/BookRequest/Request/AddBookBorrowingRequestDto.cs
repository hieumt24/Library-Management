using LibraryManagement.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Models.DTOs.BookRequest.Request
{
    public class AddBookBorrowingRequestDto
    {
        [Required]
        public string RequesterId { get; set; }

        [Required]
        public DateTime DateRequested { get; set; } = DateTime.Now;

        public RequestStatus Status { get; set; } = RequestStatus.Waiting;
        public string? ApproverId { get; set; }
    }
}