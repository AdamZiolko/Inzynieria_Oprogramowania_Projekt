using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBookworm.Areas.Identity.Data;
using ProjectBookworm.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace ProjectBookworm.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new UserManagementViewModel
            {
                Users = users
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View(); // Zwraca widok CreateUser
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
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Role == 1)
                    {
                        await _userManager.AddToRoleAsync(user, "Administrator");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    // Zamiast przekierowywania, wyświetl komunikat o sukcesie
                    ModelState.Clear(); // Czyści błędy w ModelState
                    ViewBag.Message = "Użytkownik został pomyślnie utworzony!";
                }
                else
                {
                    // W przypadku błędów dodaj je do ModelState
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}