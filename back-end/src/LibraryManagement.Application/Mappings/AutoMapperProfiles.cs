using AutoMapper;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookBorrowDetails;
using LibraryManagement.Application.Models.DTOs.BookBorrowDetails.Response;
using LibraryManagement.Application.Models.DTOs.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Models.DTOs.BookRequest.Response;
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

        public IMapper Mapper => _mapper;

        public AutoMapperProfiles()
        {
            //map Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ReverseMap();
            CreateMap<Category, AddCategoryRequestDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequestDto>().ReverseMap();

            //map Book
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, AddBookRequestDto>().ReverseMap();
            CreateMap<Book, UpdateBookRequestDto>().ReverseMap();
            CreateMap<Book, BookResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

            //map BookBorrowingRequest
            CreateMap<BookBorrowingRequest, BookBorrowingRequestDto>().ReverseMap();
            CreateMap<BookBorrowingRequest, AddBookBorrowingRequestDto>().ReverseMap();
            CreateMap<BookBorrowingRequest, UpdateBookBorrowingRequestDto>().ReverseMap();
            CreateMap<BookBorrowingRequest, BookBorrowingResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Requester.UserName))
                .ForMember(dest => dest.ApproverName, opt => opt.MapFrom(src => src.Approver.UserName))
                .ReverseMap();

            //map BookBorrowingRequestDetails
            CreateMap<AddBookBorrowingRequestDetailsDto, BookBorrowingRequestDetails>();
            CreateMap<BookBorrowingRequestDetails, BookBorrowingRequestDetailsDto>();
            CreateMap<BookBorrowingRequestDetails, BookBorrowingDetailsResponseDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
                .ReverseMap();
        }
    }
}