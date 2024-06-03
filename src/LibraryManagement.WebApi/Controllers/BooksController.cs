using LibraryManagement.Application.Common.Services;
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

        // GET: api/book/{id}
        [HttpGet]
        [Route("book/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _bookServiceAsync.GetBookByIdAsync(id);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
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

        //PUT: api/book/{id}
        [HttpPut]
        [Route("book/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookRequestDto request)
        {
            var response = await _bookServiceAsync.UpdateBookAsync(id, request);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        // DELETE: api/book/{id}
        [HttpDelete]
        [Route("book/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _bookServiceAsync.DeleteBookAsync(id);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}