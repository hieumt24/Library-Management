using AutoMapper;
using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Common.Specifications;
using LibraryManagement.Application.Enums;
using LibraryManagement.Application.Models.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest;
using LibraryManagement.Application.Models.DTOs.BookRequest.Request;
using LibraryManagement.Application.Models.DTOs.BookRequest.Response;
using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Services
{
    public class BookBorrowRequestServiceAsync : IBookBorrowRequestServiceAsync
    {
        private readonly IBookBorrowingRequestRepositoryAsync _bookBorrowingRequestRepositoryAsync;
        private readonly IMapper _mapper;

        public BookBorrowRequestServiceAsync(IBookBorrowingRequestRepositoryAsync bookBorrowingRequestRepositoryAsync, IMapper mapper)
        {
            _bookBorrowingRequestRepositoryAsync = bookBorrowingRequestRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<BookBorrowingResponseDto>> AddBookBorrowRequestAsync(AddBookBorrowingRequestDto request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                //Mapping request to BookBorrowingRequest
                var bookBorrowingRequest = _mapper.Map<BookBorrowingRequest>(request);
                var checkValidRequest = BookBorrowSpecifications.CheckValidBookBorrowRequest(request.RequesterId, request.DateRequested);
                var listRequestByUserResult = await _bookBorrowingRequestRepositoryAsync.ListAsync(checkValidRequest);
                if (listRequestByUserResult.Count >= 3)
                {
                    return new Response<BookBorrowingResponseDto>("You have reached the maximum number of borrowing requests");
                }

                var listBookDetails = new List<BookBorrowingRequestDetails>();

                foreach (var detailsDto in request.requestDetailsDtos)
                {
                    listBookDetails.Add(new BookBorrowingRequestDetails
                    {
                        BookId = detailsDto.BookId,
                        ReturnedDate = detailsDto.ReturnedDate,
                        BookBorrowingRequestId = bookBorrowingRequest.Id,
                    });
                }

                if (listBookDetails.Count >= 5)
                {
                    return new Response<BookBorrowingResponseDto>("You can only borrow a maximum of 3 times per month");
                }
                bookBorrowingRequest.CreatedOn = DateTime.Now;
                bookBorrowingRequest.Status = Enums.RequestStatus.Waiting;
                bookBorrowingRequest.DateRequested = DateTime.Now;
                bookBorrowingRequest.IsDeleted = false;

                var resultBookBorrowingRequest = await _bookBorrowingRequestRepositoryAsync.CreateBookBorrowingRequest(bookBorrowingRequest, listBookDetails);
                if (resultBookBorrowingRequest == null)
                {
                    return new Response<BookBorrowingResponseDto>("You can only borrow a maximum of 5 books per request");
                }
                return new Response<BookBorrowingResponseDto>(_mapper.Map<BookBorrowingResponseDto>(resultBookBorrowingRequest));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<PagedResponse<List<BookBorrowingResponseDto>>>> GetAllBookBorrowingRequest(RequestStatus? status, int page, int limit)
        {
            try
            {
                var bookBorrowRequestSpec = BookBorrowSpecifications.GetAllBookBorrowRequest(status);
                var totalRecord = await _bookBorrowingRequestRepositoryAsync.CountAsync(bookBorrowRequestSpec);
                bookBorrowRequestSpec.ApplyPaging((page - 1) * limit, limit);

                var bookBorrowRequest = await _bookBorrowingRequestRepositoryAsync.ListAsync(bookBorrowRequestSpec);

                var bookBorrowRequestDto = _mapper.Map<List<BookBorrowingResponseDto>>(bookBorrowRequest);
                var pagedResponse = new PagedResponse<List<BookBorrowingResponseDto>>(bookBorrowRequestDto, page, limit, totalRecord);
                return new Response<PagedResponse<List<BookBorrowingResponseDto>>>(pagedResponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<BookBorrowingResponseDto>> GetBookBorrowingRequestById(Guid id)
        {
            try
            {
                var bookBorrowRequestSpec = BookBorrowSpecifications.GetBookBorrowRequestById(id);
                var bookBorrowingRequest = await _bookBorrowingRequestRepositoryAsync.FirstOrDefaultAsync(bookBorrowRequestSpec);
                if (bookBorrowingRequest == null)
                {
                    return new Response<BookBorrowingResponseDto>("Book borrowing request not found");
                }
                var bookBorrowingRequestDto = _mapper.Map<BookBorrowingResponseDto>(bookBorrowingRequest);
                return new Response<BookBorrowingResponseDto>(bookBorrowingRequestDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<BookBorrowingResponseDto>>> GetBookBorrowingRequestByRegisterId(string? requesterId)
        {
            try
            {
                //Check requesterId is null
                if (requesterId == null)
                {
                    throw new ArgumentNullException(nameof(requesterId));
                }
                var bookBorrowRequestSpec = BookBorrowSpecifications.GetBookBorrowRequestByRegisterId(requesterId);
                var bookBorrowingRequest = await _bookBorrowingRequestRepositoryAsync.ListAsync(bookBorrowRequestSpec);

                //Map to Dto
                return new Response<List<BookBorrowingResponseDto>>(_mapper.Map<List<BookBorrowingResponseDto>>(bookBorrowingRequest));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<BookBorrowingRequestDto>> UpdateBookBorrowingRequestAsync(Guid id, UpdateBookBorrowingRequestDto updateBookBorrowingRequestDto)
        {
            try
            {
                if (updateBookBorrowingRequestDto == null)
                {
                    throw new ArgumentNullException(nameof(updateBookBorrowingRequestDto));
                }
                //Mapping request to BookBorrowingRequest
                var bookBorrowingRequest = _mapper.Map<BookBorrowingRequest>(updateBookBorrowingRequestDto);
                var existingBookBorrowingRequest = await _bookBorrowingRequestRepositoryAsync.GetByIdAsync(id);
                if (existingBookBorrowingRequest == null)
                {
                    return new Response<BookBorrowingRequestDto>("Book borrowing request not found");
                }
                existingBookBorrowingRequest.Status = updateBookBorrowingRequestDto.Status;
                existingBookBorrowingRequest.ApproverId = updateBookBorrowingRequestDto.ApproverId;
                existingBookBorrowingRequest.RequesterId = updateBookBorrowingRequestDto.RequesterId;

                existingBookBorrowingRequest.LastModifiedOn = DateTime.Now;

                await _bookBorrowingRequestRepositoryAsync.UpdateAsync(existingBookBorrowingRequest);

                var updatedBookBorrowingRequestDto = _mapper.Map<BookBorrowingRequestDto>(existingBookBorrowingRequest);
                return new Response<BookBorrowingRequestDto>(updatedBookBorrowingRequestDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<BookBorrowingRequestDto>> DeleteBookBorrowingRequestAsync(Guid id)
        {
            try
            {
                var bookBorrowingRequest = await _bookBorrowingRequestRepositoryAsync.GetByIdAsync(id);
                if (bookBorrowingRequest == null)
                {
                    return new Response<BookBorrowingRequestDto>("Book borrowing request not found");
                }
                bookBorrowingRequest.LastModifiedOn = DateTime.Now;
                var deletedBookBorrowingRequest = await _bookBorrowingRequestRepositoryAsync.DeleteAsync(bookBorrowingRequest.Id);

                //Map to BookBorrowingRequestDto
                var deletedBookBorrowingRequestDto = _mapper.Map<BookBorrowingRequestDto>(deletedBookBorrowingRequest);
                return new Response<BookBorrowingRequestDto>(deletedBookBorrowingRequestDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}