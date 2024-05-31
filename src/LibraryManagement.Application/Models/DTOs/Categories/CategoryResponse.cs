using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Models.DTOs.Categories
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}