using LibraryManagement.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Models.DTOs.BookRequest.Request
{
    public class UpdateBookBorrowingRequestDto
    {
        [Required]
        public string RequesterId { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Waiting;
        public string? ApproverId { get; set; }
    }
}