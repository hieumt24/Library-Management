using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Wrappers;

<<<<<<< HEAD:src/LibraryManagement.Application/Common/Services/IBookServiceAsync.cs
namespace LibraryManagement.Application.Common.Services
=======
namespace LibraryManagement.Application.Interfaces
>>>>>>> d27a830a6df6256e681481fecb324138e493606f:src/LibraryManagement.Application/Interfaces/IBookServiceAsync.cs
{
    public interface IBookServiceAsync
    {
        Task<Response<BookDto>> AddBookAsync(AddBookRequestDto request);

        Task<Response<List<BookResponseDto>>> GetAllBookAsync();

<<<<<<< HEAD:src/LibraryManagement.Application/Common/Services/IBookServiceAsync.cs
        Task<Response<BookResponseDto>> GetBookByIdAsync(Guid id);
=======
        Task<Response<BookResponseDto>> GetBookById(Guid id);
>>>>>>> d27a830a6df6256e681481fecb324138e493606f:src/LibraryManagement.Application/Interfaces/IBookServiceAsync.cs

        Task<Response<BookDto>> UpdateBookAsync(Guid id, UpdateBookRequestDto request);

        Task<Response<BookDto>> DeleteBookAsync(Guid id);
    }
}