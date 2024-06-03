using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Models.DTOs.Categories.Request;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServiceAsync _categoryServiceAsync;

        public CategoryController(ICategoryServiceAsync categoryServiceAsync)
        {
            _categoryServiceAsync = categoryServiceAsync;
        }

        // GET: api/categories
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryServiceAsync.GetAllCategoriesAsync();
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        // GET: api/category/{id}
        [HttpGet]
        [Route("category/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _categoryServiceAsync.GetCategoryById(id);
            if (response.Message != null)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        // POST: api/category
        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto request)
        {
            var response = await _categoryServiceAsync.AddCategoryAsync(request);
            if (response.Message != null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //DELETE: api/category/{id}
        [HttpDelete]
        [Route("category/{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _categoryServiceAsync.DeleteCategoryAsync(id);
            if (response.Message != null)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        // PUT: api/category/{id}
        [HttpPut]
        [Route("category/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto request)
        {
            var response = await _categoryServiceAsync.UpdateCategoryAsync(id, request);
            if (response.Message != null)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}