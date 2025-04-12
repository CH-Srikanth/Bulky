using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
   public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [MinLength(5),MaxLength(30)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Product Description is required.")]
        [MinLength(10), MaxLength(400)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product ISBN is required.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Product Author is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Product ListPrice is required.")]
        [Display(Name ="List Price for 1-50")]
        [Range(1,1000)]
        public double ListPrice { get; set; }

        [Required(ErrorMessage = "Product ListPrice is required.")]
        [Display(Name = "List Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required(ErrorMessage = "Product ListPrice is required.")]
        [Display(Name = "List Price 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        // Foreign Key
       
        public int CategoryId { get; set; }
        // Navigation Property
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }


    }
}
