using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectBookworm.Areas.Identity.Data; 

namespace Project_Bookworm.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } 

        [Required]
        public int BookId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } 
    }
}