using LibraryManagement.Application.Models.DTOs.Books.Response;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Models.DTOs.Categories.Response
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<BookResponseDto>? Books { get; set; }
    }
}