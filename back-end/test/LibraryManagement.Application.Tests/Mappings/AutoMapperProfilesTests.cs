// AutoMapperProfilesTests.cs
using AutoMapper;
using LibraryManagement.Application.Mappings;
using Moq;
using Xunit;

public class AutoMapperProfilesTests
{
    private readonly IMapper _mapper;

    public AutoMapperProfilesTests()
    {
        // Khởi tạo cấu hình AutoMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfiles());
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public void CategoryMapping_ShouldBeValid()
    {
        // Act
        var source = new LibraryManagement.Domain.Entities.Category { Id = Guid.NewGuid(), Name = "Test Category" };
        var destination = _mapper.Map<LibraryManagement.Domain.Entities.Category, LibraryManagement.Application.Models.DTOs.Categories.CategoryDto>(source);

        // Assert
        Assert.NotNull(destination);
        Assert.Equal(source.Id, destination.Id);
        Assert.Equal(source.Name, destination.Name);
    }

    [Fact]
    public void Constructor_ShouldSetMapper()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();

        // Act
        var autoMapperProfiles = new AutoMapperProfiles(mockMapper.Object);

        // Assert
        Assert.NotNull(autoMapperProfiles);
        Assert.Equal(mockMapper.Object, autoMapperProfiles.Mapper);
    }
}