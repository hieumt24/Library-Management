using AutoMapper;
using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Models.DTOs.Books;
using LibraryManagement.Application.Models.DTOs.Books.Request;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Wrappers;
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
                bookDomain.CreatedOn = DateTime.Now;

                await _bookRepositoryAsync.AddAsync(bookDomain);

                //Map Domain to Dto
                return new Response<BookDto>(_mapper.Map<BookDto>(bookDomain));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<BookDto>> DeleteBookAsync(Guid id)
        {
            try
            {
                var bookDomain = await _bookRepositoryAsync.GetByIdAsync(id);
                if (bookDomain == null)
                {
                    return new Response<BookDto>("Book not found");
                }
                var deletedBookDomain = await _bookRepositoryAsync.DeleteAsync(bookDomain.Id);
                //Map Domain to Dto
                var deletedBookDto = _mapper.Map<BookDto>(deletedBookDomain);
                return new Response<BookDto>(deletedBookDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<PagedResponse<List<BookResponseDto>>>> GetAllBookAsync(int page, int limit)
        {
            try
            {
                var booksSpec = BookSpecifications.GetAllBooksSpec();
                var totalRecord = await _bookRepositoryAsync.CountAsync(booksSpec);
                booksSpec.ApplyPaging((page - 1) * limit, limit);

                var totalBooks = await _bookRepositoryAsync.CountAsync(booksSpec);
                var booksDomain = await _bookRepositoryAsync.ListAsync(booksSpec);

                var listBookDto = _mapper.Map<List<BookResponseDto>>(booksDomain);
                var pagedResponse = new PagedResponse<List<BookResponseDto>>(listBookDto, page, limit, totalRecord);

                return new Response<PagedResponse<List<BookResponseDto>>>(pagedResponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<BookResponseDto>> GetBookByIdAsync(Guid id)
        {
            try
            {
                var bookSpec = BookSpecifications.GetBookByIdWithCategorySpec(id);
                var bookDomain = await _bookRepositoryAsync.FirstOrDefaultAsync(bookSpec);
                if (bookDomain == null)
                {
                    return new Response<BookResponseDto>("Book not found");
                }
                var bookDto = _mapper.Map<BookResponseDto>(bookDomain);
                return new Response<BookResponseDto>(bookDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<BookDto>> UpdateBookAsync(Guid id, UpdateBookRequestDto request)
        {
            try
            {
                var bookDomain = _mapper.Map<Book>(request);

                var existingBook = await _bookRepositoryAsync.GetByIdAsync(id);
                if (existingBook == null)
                {
                    return new Response<BookDto>("Book not found");
                }
                existingBook.Title = bookDomain.Title;
                existingBook.Author = bookDomain.Author;
                existingBook.Price = bookDomain.Price;
                existingBook.Description = bookDomain.Description;
                existingBook.QuantityInStock = bookDomain.QuantityInStock;
                existingBook.Language = bookDomain.Language;
                existingBook.PublicationYear = bookDomain.PublicationYear;
                existingBook.CategoryId = bookDomain.CategoryId;
                existingBook.IsDeleted = bookDomain.IsDeleted;

                existingBook.LastModifiedOn = DateTime.Now;

                await _bookRepositoryAsync.UpdateAsync(existingBook);

                var updatedBookDto = _mapper.Map<BookDto>(existingBook);
                return new Response<BookDto>(updatedBookDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}