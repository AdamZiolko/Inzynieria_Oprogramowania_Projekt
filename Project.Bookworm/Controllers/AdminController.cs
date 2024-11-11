using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBookworm.Areas.Identity.Data;
using ProjectBookworm.Models;
using Microsoft.EntityFrameworkCore;
using Project_Bookworm.Models;
using ProjectBookworm.Data;
namespace ProjectBookworm.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
        ) {
            _userManager = userManager;
            _context = context;
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
            return View(); 
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

                    ModelState.Clear(); 
                    ViewBag.Message = "Użytkownik został pomyślnie utworzony!";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                _context.BookContents.Add(new BookContent(book.Id));
                _context.SaveChanges();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);    
            }
            
            return View("_BookManagementPartial", book);  
        }
    }
}