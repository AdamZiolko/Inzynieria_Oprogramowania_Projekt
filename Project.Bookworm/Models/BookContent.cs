using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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

        public string GetFormattedContent()
        {
            if (string.IsNullOrEmpty(Content))
            {
                return Content;
            }

            string formattedContent = Regex.Replace(Content, @"\\h\s*(.+?)\s*\\h", "<h2 class=\"my-3\">$1</h2>");
            return formattedContent;
        }

        public BookContent(int id) { 
            Content = "";
            BookId = id;
        }
    }
}
