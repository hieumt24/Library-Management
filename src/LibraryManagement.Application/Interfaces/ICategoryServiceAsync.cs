using LibraryManagement.Application.Models.DTOs.Categories;
using LibraryManagement.Application.Wrappers;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface ICategoryServiceAsync
    {
        Task<Response<CategoryDto>> AddCategoryAsync(AddCategoryRequestDto request);

        Task<Response<List<CategoryResponse>>> GetAllCategoriesAsync();

        Task<Response<CategoryResponse>> GetCategoryById(Guid id);

        Task<Response<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request);

        Task<Response<CategoryDto>> DeleteCategoryAsync(Guid id);
    }
}