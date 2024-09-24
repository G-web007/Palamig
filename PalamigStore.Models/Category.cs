using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PalamigStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a Category")]
        [DisplayName("Category Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Quantity")]
        [DisplayName("Category Quantity")]
        public int Quantity { get; set; }
    }
}
