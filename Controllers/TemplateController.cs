using IO_B.EF;
using IO_B.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IO_B.Controllers
{
    public class TemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TemplateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var entityData = await _context
                .YourEntities
                .ToListAsync();

            return View(entityData);
        }

        [HttpPost]
        public async Task<IActionResult> Create(YourEntity newEntity)
        {
            if (ModelState.IsValid)
            {
                _context.YourEntities.Add(newEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newEntity);
        }
    }
}