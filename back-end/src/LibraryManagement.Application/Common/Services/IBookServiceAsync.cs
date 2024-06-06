using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Common.Services

{
    public interface IBookServiceAsync
    {
        Task<Response<BookDto>> AddBookAsync(AddBookRequestDto request);

        Task<Response<PagedResponse<List<BookResponseDto>>>> GetAllBookAsync(int page, int limit);

        Task<Response<BookResponseDto>> GetBookByIdAsync(Guid id);

        Task<Response<BookDto>> UpdateBookAsync(Guid id, UpdateBookRequestDto request);

        Task<Response<BookDto>> DeleteBookAsync(Guid id);
    }
}