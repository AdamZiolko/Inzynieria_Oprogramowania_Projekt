using ProjectBookworm.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project_Bookworm.Models;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

public IActionResult Index(string searchString, string genre, string sortOrder, int page = 1)
{
    var books = from b in _context.Books
                select b;

    if (!String.IsNullOrEmpty(searchString))
    {
        books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
    }

    if (!String.IsNullOrEmpty(genre))
    {
        books = books.Where(b => b.Genre == genre);
    }

    switch (sortOrder)
    {
        case "title_asc":
            books = books.OrderBy(b => b.Title);
            break;
        case "title_desc":
            books = books.OrderByDescending(b => b.Title);
            break;
        case "author_asc":
            books = books.OrderBy(b => b.Author);
            break;
        case "author_desc":
            books = books.OrderByDescending(b => b.Author);
            break;
        case "date_asc":
            books = books.OrderBy(b => b.ReleaseDate);
            break;
        case "date_desc":
            books = books.OrderByDescending(b => b.ReleaseDate);
            break;
        default:
            books = books.OrderBy(b => b.Title);
            break;
    }

    int pageSize = 10;
    var pagedBooks = PaginatedList<Book>.Create(books.AsNoTracking(), page, pageSize);

    ViewBag.CurrentPage = page;
    ViewBag.TotalPages = (int)Math.Ceiling(books.Count() / (double)pageSize);
    ViewBag.SearchString = searchString;
    ViewBag.Genre = genre;
    ViewBag.SortOrder = sortOrder;

    return View(pagedBooks);
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