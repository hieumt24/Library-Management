using LibraryManagement.Domain.Common.Models;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Models.DTOs.Categories
{
    public class CategoryDto : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}