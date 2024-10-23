using ProjectBookworm.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string searchString, string genre, int page = 1, int pageSize = 10)
    {
        var books = from b in _context.Books
                    select b;

        if (!string.IsNullOrEmpty(searchString))
        {
            books = books
                .Where(
                    s =>
                        s.Title.ToLower().Contains(searchString.ToLower()) ||
                        s.Author.ToLower().Contains(searchString.ToLower())
                    );
        }

        if (!string.IsNullOrEmpty(genre))
        {
            books = books.Where(s => s.Genre.ToLower().Contains(genre.ToLower()));
        }

        var paginatedBooks = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)books.Count() / pageSize);
        ViewBag.SearchString = searchString;
        ViewBag.Genre = genre;

        return View(paginatedBooks);
    }

    public IActionResult Details(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return NotFound();
        }

        var book = _context.Books
            .Include(b => b.BookContent)
            .FirstOrDefault(b => b.Title.ToLower() == title.ToLower());

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }
}