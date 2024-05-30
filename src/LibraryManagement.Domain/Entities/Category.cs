using LibraryManagement.Domain.Common.Models;

namespace LibraryManagement.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        //Navigation property
        public ICollection<Book>? Books { get; set; }
    }
}