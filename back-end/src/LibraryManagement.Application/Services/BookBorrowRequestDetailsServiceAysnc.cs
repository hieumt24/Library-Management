using AutoMapper;
using LibraryManagement.Application.Common.Repositories;
using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Common.Specifications;
using LibraryManagement.Application.Models.DTOs.BookBorrowDetails.Response;
using LibraryManagement.Application.Wrappers;

namespace LibraryManagement.Application.Services
{
    public class BookBorrowRequestDetailsServiceAysnc : IBookBorrowRequestDetailsServiceAsync
    {
        private readonly IBookBorrowingRequestDetailsRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public BookBorrowRequestDetailsServiceAysnc(IBookBorrowingRequestDetailsRepositoryAsync repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<BookBorrowingDetailsResponseDto>>> GetAllBorrowingRequestDetails()
        {
            var detailRequestSpec = BookBorrowingRequestDetailsSpec.GetAllBookBorrowingRequestDetails();
            var listBookBorrowingDetails = await _repository.ListAsync(detailRequestSpec);

            //Map to Dto
            var bookBorrowingDetailsDto = _mapper.Map<List<BookBorrowingDetailsResponseDto>>(listBookBorrowingDetails);
            return new Response<List<BookBorrowingDetailsResponseDto>>(bookBorrowingDetailsDto);
        }

        public async Task<Response<BookBorrowingDetailsResponseDto>> GetBorrowingRequestDetailsById(Guid id, Guid bookBorrowingRequestId)
        {
            try
            {
                var detailRequestSpec = BookBorrowingRequestDetailsSpec.GetBookBorrowingRequestDetailsById(id, bookBorrowingRequestId);
                var bookBorrowingDetails = await _repository.FirstOrDefaultAsync(detailRequestSpec);
                if (bookBorrowingDetails == null)
                {
                    return new Response<BookBorrowingDetailsResponseDto>("Book borrowing details not found");
                }
                //Map to Dto
                var bookBorrowingDetailsDto = _mapper.Map<BookBorrowingDetailsResponseDto>(bookBorrowingDetails);
                return new Response<BookBorrowingDetailsResponseDto>(bookBorrowingDetailsDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<BookBorrowingDetailsResponseDto>>> GetBorrowingRequestDetailsByRequester(Guid bookBorrowingRequestId)
        {
            try
            {
                var detailRequestSpec = BookBorrowingRequestDetailsSpec.GetBookBorrowingRequestDetailsByRequester(bookBorrowingRequestId);
                var bookBorrowingDetails = await _repository.ListAsync(detailRequestSpec);
                if (bookBorrowingDetails == null)
                {
                    return new Response<List<BookBorrowingDetailsResponseDto>>("Book borrowing details not found");
                }
                //Map to Dto
                var bookBorrowingDetailsDto = _mapper.Map<List<BookBorrowingDetailsResponseDto>>(bookBorrowingDetails);
                return new Response<List<BookBorrowingDetailsResponseDto>>(bookBorrowingDetailsDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}