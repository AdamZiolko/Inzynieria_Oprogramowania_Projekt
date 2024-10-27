using ProjectBookworm.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Bookworm.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public double Value { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}