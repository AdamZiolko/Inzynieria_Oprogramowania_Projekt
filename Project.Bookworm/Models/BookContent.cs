using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_Bookworm.Models
{
    public class BookContent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
