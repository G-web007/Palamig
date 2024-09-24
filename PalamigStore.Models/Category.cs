using System.ComponentModel.DataAnnotations;

namespace PalamigStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
