using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Models.DTOs.Categories.Request
{
    public class UpdateCategoryRequestDto
    {
        [Required(ErrorMessage = "Name Category is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string? Description { get; set; }
    }
}