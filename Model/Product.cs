using System.ComponentModel.DataAnnotations;

namespace BrainerHubTask.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(255)]
        
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        // Navigation property to represent the images associated with the product
        
        public ICollection<Image>? Images { get; set; }

        // Foreign key to represent the creator (user) of the product
        public int UserId { get; set; }

        // Navigation property to access the user who created the product
        public User User { get; set; }

    }
}
