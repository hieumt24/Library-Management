using LibraryManagement.Application.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BookBorrowingRequestDetailsController : ControllerBase
    {
        private readonly IBookBorrowRequestDetailsServiceAsync _service;

        public BookBorrowingRequestDetailsController(IBookBorrowRequestDetailsServiceAsync service)
        {
            _service = service;
        }

        //GET: api/v1/book-borrowing-request-details
        [HttpGet]
        [Route("book-borrowing-request-details")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllBorrowingRequestDetails();
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //GET: api/v1/book-borrowing-request-details/{id}&&{bookborrowingRequestId}
        [HttpGet]
        [Route("book-borrowing-request-details/{id}&&{bookborrowingRequestId}")]
        public async Task<IActionResult> GetById(Guid id, Guid bookborrowingRequestId)
        {
            var response = await _service.GetBorrowingRequestDetailsById(id, bookborrowingRequestId);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //GET: api/v1/book-borrowing-request-details/{bookborrowingRequestId}
        [HttpGet]
        [Route("book-borrowing-request-details/{bookborrowingRequestId}")]
        public async Task<IActionResult> GetByRequester(Guid bookborrowingRequestId)
        {
            var response = await _service.GetBorrowingRequestDetailsByRequester(bookborrowingRequestId);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}