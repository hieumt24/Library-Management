using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookServiceAsync _bookServiceAsync;

        public BooksController(IBookServiceAsync bookServiceAsync)
        {
            _bookServiceAsync = bookServiceAsync;
        }

        // POST: api/book
        [HttpPost]
        [Route("book")]
        public async Task<IActionResult> Create([FromBody] AddBookRequestDto request)
        {
            var response = await _bookServiceAsync.AddBookAsync(request);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        // GET: api/books
        [HttpGet]
        [Route("books")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bookServiceAsync.GetAllBookAsync();
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}