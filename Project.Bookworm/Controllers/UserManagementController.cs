using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBookworm.Models;
using ProjectBookworm.Areas.Identity.Data;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectBookworm.Controllers
{
    public class usermanagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public usermanagementController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserManagementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    Role = model.Role == 1 ? 1 : 0 // Ustaw rolę
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Niepoprawne dane formularza: " + string.Join("; ", errors) });
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var model = new UserManagementViewModel
            {
                Users = users
            };
            return View(model);
        }
    }
}
