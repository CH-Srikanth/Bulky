using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category // Renamed from category to Category
    {
        public int Id { get; set; }

        [Required( ErrorMessage = "Category Name is required.") ]
        [MinLength(3, ErrorMessage = "Category Name must be at least 3 characters long.")]
        public string Name { get; set; }

       
        [Required(ErrorMessage = "Display Order is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Display Order must be greater than 0.")]
        public int DisplayOrder { get; set; }
    }
}
