using Project_Bookworm.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.Bookworm.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [StringLength(250)]
        public string Genre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public BookContent BookContent { get; set; }
    }

}