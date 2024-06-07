using AutoMapper;
using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Models.DTOs.Categories.Request;
using LibraryManagement.Application.Models.DTOs.Categories;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using Moq;
using LibraryManagement.Domain.Common.Specifications;
using LibraryManagement.Domain.Specifications.Categories;
using LibraryManagement.Application.Models.DTOs.Categories.Response;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Tests.Services
{
    public class CategoryServiceAsyncTests
    {
        private readonly IMapper _mapper;

        public CategoryServiceAsyncTests()
        {
            // Initialize IMapper mock here or use a mocking framework to create it
            _mapper = new Mock<IMapper>().Object;
        }

        [Fact]
        public async Task AddCategoryAsync_WhenCategoryDoesNotExist_ShouldReturnSuccessfulResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var request = new AddCategoryRequestDto
            {
                Name = "Example Category",
                Description = "Example Description"
                // Add other properties as needed
            };

            // Mock the repository to return null when the category does not exist
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Category>>()))
                          .ReturnsAsync((Category)null);

            // Mock the repository to return the added category
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>()))
                          .ReturnsAsync(new Category { /* Populate with valid data */ });

            // Mock the mapper to return a valid CategoryDto
            mockMapper.Setup(mapper => mapper.Map<CategoryDto>(It.IsAny<Category>()))
                      .Returns(new CategoryDto { /* Populate with valid data */ });

            // Act
            var response = await service.AddCategoryAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            // Add additional assertions if needed
        }

        [Fact]
        public async Task AddCategoryAsync_WhenCategoryNameAlreadyExists_ShouldReturnErrorResponse()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var request = new AddCategoryRequestDto
            {
                Name = "Example Category",
                Description = "Example Description"
                // Add other properties as needed
            };

            // Mock the repository to return an existing category with the same name
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(CategorySpecifications.GetCategoryByNameSpec(request.Name)))
                          .ReturnsAsync(new Category { /* Populate with valid data */ });

            // Act
            var response = await service.AddCategoryAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Null(response.Data);
            // Add additional assertions if needed
        }

        [Fact]
        public async Task AddCategoryAsync_WhenCategoryNameAlreadyExists_ShouldReturnErrorResponse1()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var request = new AddCategoryRequestDto
            {
                Name = "Example Category",
                Description = "Example Description"
                // Add other properties as needed
            };

            var categorySpec = CategorySpecifications.GetCategoryByNameSpec(request.Name);

            // Mock the repository to return an existing category with the same name
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(categorySpec))
                          .ReturnsAsync(new Category { /* Populate with valid data */ });

            // Act
            var response = await service.AddCategoryAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Null(response.Data);
            // Add additional assertions if needed
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenCategoryExists_ShouldReturnDeletedCategory()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var categoryId = Guid.NewGuid();

            var existingCategory = new Category
            {
                Id = categoryId,
                Name = "Existing Category",
                Description = "Example Description"
                // Add other properties as needed
            };

            mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
                          .ReturnsAsync(existingCategory);

            mockRepository.Setup(repo => repo.DeleteAsync(categoryId))
                          .ReturnsAsync(existingCategory); // Assuming it returns the deleted category

            mockMapper.Setup(mapper => mapper.Map<CategoryDto>(existingCategory))
                      .Returns(new CategoryDto { /* Populate with valid data */ });

            // Act
            var response = await service.DeleteCategoryAsync(categoryId);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenCategoryDoesNotExist_ShouldReturnCategoryNotFound()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var categoryId = Guid.NewGuid();

            mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
                          .ReturnsAsync((Category)null); // Simulate that category does not exist

            // Act
            var response = await service.DeleteCategoryAsync(categoryId);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Category not found", response.Message);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnPagedListOfCategories()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var page = 1;
            var limit = 10;

            var categories = new List<Category>
    {
        new Category { Id = Guid.NewGuid(), Name = "Category 1" },
        new Category { Id = Guid.NewGuid(), Name = "Category 2" },
        // Add more categories as needed
    };

            var totalRecords = categories.Count;

            var categoriesDto = categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            mockRepository.Setup(repo => repo.CountAsync(It.IsAny<BaseSpecification<Category>>()))
                          .ReturnsAsync(totalRecords);
            mockRepository.Setup(repo => repo.ListAsync(It.IsAny<BaseSpecification<Category>>()))
                          .ReturnsAsync(categories);

            mockMapper.Setup(mapper => mapper.Map<List<CategoryResponseDto>>(It.IsAny<List<Category>>()))
                      .Returns(categoriesDto);

            // Act
            var response = await service.GetAllCategoriesAsync(page, limit);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(page, response.Data.PageNumber);
            Assert.Equal(limit, response.Data.PageSize);
            Assert.Equal(totalRecords, response.Data.TotalRecords);
            Assert.Equal(categoriesDto, response.Data.Data);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var page = 1;
            var limit = 10;

            mockRepository.Setup(repo => repo.CountAsync(It.IsAny<BaseSpecification<Category>>()))
                          .ThrowsAsync(new Exception("Repository exception"));

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetAllCategoriesAsync(page, limit));
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var categoryId = Guid.NewGuid();

            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Category>>()))
                          .ReturnsAsync((Category)null);

            // Act
            var response = await service.GetCategoryById(categoryId);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Category Id Not Found", response.Message);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnCategoryResponseDto_WhenCategoryExists()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var categoryId = Guid.NewGuid();

            // Create a mock category domain object
            var categoryDomain = new Category { Id = categoryId, Name = "TestCategory" };

            // Set up the mock repository to return the category domain object
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Category>>()))
                          .ReturnsAsync(categoryDomain);

            // Set up the mapper to map the category domain object to a category response DTO
            var expectedCategoryResponseDto = new CategoryResponseDto { Id = categoryId, Name = "TestCategory" };
            mockMapper.Setup(mapper => mapper.Map<CategoryResponseDto>(categoryDomain))
                      .Returns(expectedCategoryResponseDto);

            // Act
            var response = await service.GetCategoryById(categoryId);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(expectedCategoryResponseDto, response.Data);
        }

        [Fact]
        public async Task GetCategoryById_ShouldThrowException_WhenExceptionThrown()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);
            var categoryId = Guid.NewGuid();

            // Set up the repository mock to throw an exception
            mockRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Category>>()))
                          .ThrowsAsync(new Exception("Repository exception"));

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetCategoryById(categoryId));
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryExists_ShouldReturnUpdatedCategory()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);

            var id = Guid.NewGuid();
            var request = new UpdateCategoryRequestDto
            {
                Name = "Updated Name",
                Description = "Updated Description"
            };

            var existingCategory = new Category
            {
                Id = id,
                Name = "Original Name",
                Description = "Original Description"
            };

            var updatedCategory = new Category
            {
                Id = id,
                Name = request.Name,
                Description = request.Description
            };

            var updatedCategoryDto = new CategoryDto
            {
                Id = id,
                Name = request.Name,
                Description = request.Description
            };

            // Mock IMapper Setup
            mockMapper.Setup(mapper => mapper.Map<Category>(It.IsAny<UpdateCategoryRequestDto>()))
                      .Returns(updatedCategory);
            mockRepository.Setup(repo => repo.GetByIdAsync(id))
                          .ReturnsAsync(existingCategory);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Category>()))
                          .ReturnsAsync(updatedCategory);
            mockMapper.Setup(mapper => mapper.Map<CategoryDto>(It.IsAny<Category>()))
                      .Returns(updatedCategoryDto);

            // Act
            var response = await service.UpdateCategoryAsync(id, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(updatedCategoryDto, response.Data);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);

            var categoryId = Guid.NewGuid();
            var request = new UpdateCategoryRequestDto
            {
                Name = "Updated Name",
                Description = "Updated Description"
            };

            // Set up the repository mock to throw an exception
            mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
                          .ThrowsAsync(new Exception("Repository exception"));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.UpdateCategoryAsync(categoryId, request));
            Assert.Equal("Repository exception", exception.Message);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldReturnNotFoundResponse_WhenCategoryNotFound()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICategoryRepositoryAsync>();
            var service = new CategoryServiceAsync(mockRepository.Object, mockMapper.Object);

            var categoryId = Guid.NewGuid();
            var request = new UpdateCategoryRequestDto
            {
                Name = "Updated Name",
                Description = "Updated Description"
            };

            // Set up the repository mock to return null for existing category
            mockRepository.Setup(repo => repo.GetByIdAsync(categoryId))
                          .ReturnsAsync((Category)null);

            // Act
            var response = await service.UpdateCategoryAsync(categoryId, request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Succeeded);
            Assert.Equal("Category not found", response.Message);
            Assert.Null(response.Data);
        }
    }
}