using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1 - 50")]
        [Range(1, 50)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50 +")]
        [Range(50, 99)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100 +")]
        [Range(100, 1000)]
        public double Price100 { get; set; }

    }
}
