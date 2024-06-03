using LibraryManagement.Application.Models.DTOs.Categories;
using LibraryManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Models.DTOs.Books.Response
{
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        public string? Author { get; set; }

        public double Price { get; set; }

        public string? Description { get; set; }

        public int QuantityInStock { get; set; }

        public string? Language { get; set; }

        public int PublicationYear { get; set; }

        public int BorrowCount { get; set; } = 0;

        public Guid? CategoryId { get; set; }

        //public Category? Category { get; set; }
        public string? CategoryName { get; set; }

        public ICollection<BookImage>? Images { get; set; }
    }
}