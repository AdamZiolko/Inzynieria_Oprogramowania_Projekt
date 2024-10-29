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
                    Role = model.Role == 1 ? 1 : 0
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

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Niepoprawny adres email." });
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(new { success = false, message = "Użytkownik nie został znaleziony." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }

            var errors = result.Errors.Select(e => e.Description);
            return Json(new { success = false, message = "Błąd podczas usuwania użytkownika: " + string.Join("; ", errors) });
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, UserManagementViewModel model)
        {
            if (string.IsNullOrEmpty(id) || model == null)
            {
                return Json(new { success = false, message = "Niepoprawne dane." });
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Użytkownik nie został znaleziony." });
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;

            user.Role = model.Role;

            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!passwordResult.Succeeded)
                {
                    var passwordErrors = passwordResult.Errors.Select(e => e.Description);
                    return Json(new { success = false, message = "Błąd podczas aktualizacji hasła: " + string.Join("; ", passwordErrors) });
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }

            var errors = result.Errors.Select(e => e.Description);
            return Json(new { success = false, message = "Błąd podczas aktualizacji użytkownika: " + string.Join("; ", errors) });
        }



        public class UserData
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
        }



    }
}