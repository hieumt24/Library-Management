using AutoMapper;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Models.DTOs.Categories;
using LibraryManagement.Application.Models.DTOs.Categories.Request;
using LibraryManagement.Application.Models.DTOs.Categories.Response;
using LibraryManagement.Application.Wrappers;
using LibraryManagement.Domain.Common.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Specifications.Categories;

namespace LibraryManagement.Application.Services
{
    public class CategoryServiceAsync : ICategoryServiceAsync
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServiceAsync(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryDto>> AddCategoryAsync(AddCategoryRequestDto request)
        {
            try
            {
                //Covert request to Domain Category
                var categoryDomain = _mapper.Map<Category>(request);

                //Check name is already exist or not
                var categorySpec = CategorySpecifications.GetCategoryByNameSpec(categoryDomain.Name);
                if (categorySpec != null)
                {
                    var existingCategory = await _categoryRepository.FirstOrDefaultAsync(categorySpec);
                    if (existingCategory != null)
                    {
                        return new Response<CategoryDto>("Category Name Already Exist");
                    }
                }

                categoryDomain.IsDeleted = false;
                categoryDomain.CreatedOn = DateTime.UtcNow;

                await _categoryRepository.AddAsync(categoryDomain);

                //Map Domain to Dto
                return new Response<CategoryDto>(_mapper.Map<CategoryDto>(categoryDomain));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<CategoryDto>> DeleteCategoryAsync(Guid id)
        {
            var categoryDomain = await _categoryRepository.GetByIdAsync(id);
            if (categoryDomain == null)
            {
                return new Response<CategoryDto>("Category not found");
            }

            var deletedCategoryDomain = await _categoryRepository.DeleteAsync(categoryDomain.Id);
            return new Response<CategoryDto>(_mapper.Map<CategoryDto>(deletedCategoryDomain));
        }

        public async Task<Response<List<CategoryResponseDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var categoriesSpec = CategorySpecifications.GetAllCategoriesSpec();
                var categoriesDomain = await _categoryRepository.ListAsync(categoriesSpec);

                var listCategoryDto = _mapper.Map<List<CategoryResponseDto>>(categoriesDomain);

                return new Response<List<CategoryResponseDto>>(listCategoryDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<CategoryResponseDto>> GetCategoryById(Guid id)
        {
            try
            {
                var categoryDomain = await _categoryRepository.GetByIdAsync(id);
                if (categoryDomain == null)
                {
                    return new Response<CategoryResponseDto>("Category Id Not Found");
                }

                return new Response<CategoryResponseDto>(_mapper.Map<CategoryResponseDto>(categoryDomain));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request)
        {
            try
            {
                //Map request to Domain
                var categoryDomain = _mapper.Map<Category>(request);

                var exsitingCategory = await _categoryRepository.GetByIdAsync(id);
                if (exsitingCategory == null)
                {
                    return new Response<CategoryDto>("Category not found");
                }

                exsitingCategory.Name = categoryDomain.Name;
                exsitingCategory.Description = categoryDomain.Description;
                exsitingCategory.LastModifiedOn = DateTime.UtcNow;
                exsitingCategory.IsDeleted = categoryDomain.IsDeleted;

                await _categoryRepository.UpdateAsync(exsitingCategory);

                //Map Domain to Dto
                return new Response<CategoryDto>(_mapper.Map<CategoryDto>(exsitingCategory));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}