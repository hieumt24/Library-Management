using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BookBorrowingRequestController : ControllerBase
    {
        private readonly IBookBorrowRequestServiceAsync _bookBorrowRequestServiceAsync;

        public BookBorrowingRequestController(IBookBorrowRequestServiceAsync bookBorrowRequestServiceAsync)
        {
            _bookBorrowRequestServiceAsync = bookBorrowRequestServiceAsync;
        }

        //GET: api/book-borrowing-requests
        [HttpGet]
        [Route("book-borrowing-requests")]
        public async Task<IActionResult> GetAll([FromQuery] RequestStatus? status, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var response = await _bookBorrowRequestServiceAsync.GetAllBookBorrowingRequest(status, page, limit);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //GET: api/book-borrowing-requests/{id}
        [HttpGet]
        [Route("book-borrowing-request/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _bookBorrowRequestServiceAsync.GetBookBorrowingRequestById(id);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //GET: api/book-borrowing-requests/{requesterId}
        [HttpGet]
        [Route("book-borrowing-request/register/{requesterId}")]
        public async Task<IActionResult> GetByRegisterId(string? requesterId)
        {
            var response = await _bookBorrowRequestServiceAsync.GetBookBorrowingRequestByRegisterId(requesterId);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //POST: api/book-borrowing-request
        [HttpPost]
        [Route("book-borrowing-request")]
        public async Task<IActionResult> Create([FromBody] AddBookBorrowingRequestDto request)
        {
            var response = await _bookBorrowRequestServiceAsync.AddBookBorrowRequestAsync(request);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //PUT: api/book-borrowing-request/{id}
        [HttpPut]
        [Route("book-borrowing-request/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBookBorrowingRequestDto request)
        {
            var response = await _bookBorrowRequestServiceAsync.UpdateBookBorrowingRequestAsync(id, request);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //DELETE: api/book-borrowing-request/{id}
        [HttpDelete]
        [Route("book-borrowing-request/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _bookBorrowRequestServiceAsync.DeleteBookBorrowingRequestAsync(id);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}