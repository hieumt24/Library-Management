using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models.DTOs.BookRequest.Response
{
    public class BookBorrowingRequestDetailsResponseDto
    {
        public Guid BookBorrowingRequestId { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public Guid BookId { get; set; }
    }
}