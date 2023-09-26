using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainerHubTask.Model
{
    public class Image
    {

        [Key]
        public int ImageId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FilePath { get; set; }

        
        public int UserId { get; set; }


        public User User { get; set; }


        public int? ProductId { get; set; }


        public Product Product { get; set; }
    }
}