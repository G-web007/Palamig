using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PalamigStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayName("Brand Name")]
        public string BrandName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1 - 50")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50 +")]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100 +")]
        public double Price100 { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
