using ProjectBookworm.Areas.Identity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectBookworm.Models
{
    public class UserManagementViewModel
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public int Role { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
