using LibraryManagement.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    public class BookImage : BaseEntity
    {
        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ImageUrl { get; set; }

        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string? Description { get; set; }

        //Navigation property
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }

        public Book? Book { get; set; }
    }
}