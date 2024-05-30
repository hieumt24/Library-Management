using LibraryManagement.Domain.Common.Models;

namespace LibraryManagement.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int QuantityInStock { get; set; }
        public string? Language { get; set; }
        public int PublicationYear { get; set; }

        //Navigation property Category
        public Guid? CategoryId { get; set; }

        public Category? Category { get; set; }

        //Navigation property BookImage
        public ICollection<BookImage>? Images { get; set; }
    }
}