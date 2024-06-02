using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBookServiceAsync
    {
        Task<Response<BookDto>> AddBookAsync(AddBookRequestDto request);

        Task<Response<List<BookResponseDto>>> GetAllBookAsync();

        Task<Response<BookResponseDto>> GetBookById(Guid id);

        Task<Response<BookDto>> UpdateBookAsync(Guid id, UpdateBookRequestDto request);

        Task<Response<BookDto>> DeleteBookAsync(Guid id);
    }
}