using LibraryManagement.Domain.Common.Models;

namespace LibraryManagement.Domain.Entities
{
    public class BookImage : BaseEntity
    {
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        //Navigation property
        public Guid BookId { get; set; }

        public Book? Book { get; set; }
    }
}