using LibraryManagement.Application.Enums;

namespace LibraryManagement.Application.Models.DTOs.BookRequest.Response
{
    public class BookBorrowingResponseDto
    {
        public Guid Id { get; set; }
        public string RequesterId { get; set; }
        public string UserName { get; set; }
        public DateTime DateRequested { get; set; }
        public RequestStatus Status { get; set; }
        public string? ApproverId { get; set; }
        public string? ApproverName { get; set; }
    }
}