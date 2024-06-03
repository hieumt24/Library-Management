using LibraryManagement.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    public class Book : BaseEntity
    {
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string? Title { get; set; }

        [StringLength(100, ErrorMessage = "Author can't be longer than 100 characters.")]
        public string? Author { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public double Price { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be a non-negative value.")]
        public int QuantityInStock { get; set; }

        [StringLength(50, ErrorMessage = "Language can't be longer than 50 characters.")]
        public string? Language { get; set; }

        [Range(0, 2024, ErrorMessage = "Publication year must be a valid year.")]
        public int PublicationYear { get; set; }

        //Track borrow count
        public int BorrowCount { get; set; } = 0;

        //Navigation property Category
        public Guid? CategoryId { get; set; }

        public Category? Category { get; set; }

        //Navigation property BookImage
        public ICollection<BookImage>? Images { get; set; }
    }
}