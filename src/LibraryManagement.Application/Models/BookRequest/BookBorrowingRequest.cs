﻿using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.Identity;
using LibraryManagement.Domain.Common.Models;

namespace LibraryManagement.Application.Models.BookRequest
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