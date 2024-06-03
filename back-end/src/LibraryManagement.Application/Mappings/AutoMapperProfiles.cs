using AutoMapper;
using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Models.DTOs.Categories;
using LibraryManagement.Application.Models.DTOs.Categories.Request;
using LibraryManagement.Application.Models.DTOs.Categories.Response;
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
            //CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ReverseMap();
            CreateMap<Category, AddCategoryRequestDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequestDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, AddBookRequestDto>().ReverseMap();
            CreateMap<Book, UpdateBookRequestDto>().ReverseMap();
            //CreateMap<Book, BookResponseDto>().ReverseMap();
            CreateMap<Book, BookResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
        }
    }
}