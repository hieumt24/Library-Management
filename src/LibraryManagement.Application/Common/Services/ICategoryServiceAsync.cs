using LibraryManagement.Application.Models.DTOs.Categories;
using LibraryManagement.Application.Models.DTOs.Categories.Request;
using LibraryManagement.Application.Models.DTOs.Categories.Response;
using LibraryManagement.Application.Wrappers;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Common.Services
{
    public interface ICategoryServiceAsync
    {
        Task<Response<CategoryDto>> AddCategoryAsync(AddCategoryRequestDto request);

        Task<Response<List<CategoryResponseDto>>> GetAllCategoriesAsync();

        Task<Response<CategoryResponseDto>> GetCategoryById(Guid id);

        Task<Response<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request);

        Task<Response<CategoryDto>> DeleteCategoryAsync(Guid id);
    }
}