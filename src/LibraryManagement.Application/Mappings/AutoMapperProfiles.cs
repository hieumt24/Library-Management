using AutoMapper;
using LibraryManagement.Application.Models.DTOs.Categories;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        private readonly IMapper _mapper;

        public AutoMapperProfiles(IMapper mapper)
        {
            _mapper = mapper;
        }

        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, AddCategoryRequestDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequestDto>().ReverseMap();
        }
    }
}