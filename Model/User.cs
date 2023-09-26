using System.ComponentModel.DataAnnotations;

namespace BrainerHubTask.Model
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<Product> Products { get; set; }

        
        public ICollection<Image> Images { get; set; }

    }
}
