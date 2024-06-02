using AutoMapper;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Wrappers;
using LibraryManagement.Domain.Common.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Specifications.Books;

namespace LibraryManagement.Application.Services
{
    public class BookServiceAsync : IBookServiceAsync
    {
        private readonly IBookRepositoryAsync _bookRepositoryAsync;

        private readonly IMapper _mapper;

        public BookServiceAsync(IBookRepositoryAsync bookRepositoryAsync, IMapper mapper)
        {
            _bookRepositoryAsync = bookRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<BookDto>> AddBookAsync(AddBookRequestDto request)
        {
            try
            {
                //Convert AddBookRequestDto to Book Domain
                var bookDomain = _mapper.Map<Book>(request);

                //Check if book already exists
                var bookSpec = BookSpecifications.GetBookByTitleSpec(bookDomain.Title);
                if (bookSpec != null)
                {
                    var existingBook = await _bookRepositoryAsync.FirstOrDefaultAsync(bookSpec);
                    if (existingBook != null)
                    {
                        return new Response<BookDto>("Book already exists");
                    }
                }

                bookDomain.IsDeleted = false;
                bookDomain.CreatedOn = DateTime.UtcNow;

                await _bookRepositoryAsync.AddAsync(bookDomain);

                //Map Domain to Dto
                return new Response<BookDto>(_mapper.Map<BookDto>(bookDomain));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Response<BookDto>> DeleteBookAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<BookResponseDto>>> GetAllBookAsync()
        {
            try
            {
                var booksSpec = BookSpecifications.GetAllBooksSpec();
                var booksDomain = await _bookRepositoryAsync.ListAsync(booksSpec);
                //var booksDomain = await _bookRepositoryAsync.GetBooksByCategoryAsync();

                //Map Domain to Dto
                var listBookDto = _mapper.Map<List<BookResponseDto>>(booksDomain);

                return new Response<List<BookResponseDto>>(listBookDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Response<BookResponseDto>> GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<BookDto>> UpdateBookAsync(Guid id, UpdateBookRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}