using System.Diagnostics;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using moment3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Data;

namespace moment3.Controllers;

public class HomeController : Controller
{
    // So we can search books on the "Hem" index page!
     private readonly BooksDbContext _context;

        public HomeController(BooksDbContext context)
        {
            _context = context;
        }

    // GET: Also for "search" from search input field!
    public IActionResult Index(string search)
    {
        // Make book table searchable
         IQueryable<Book> books = _context.Books;
            if (!string.IsNullOrEmpty(search))
            {
                // We search book(s) by first JOINing author table so we can search by book title AND author of that book
                // since book table only refer to Author by AuthorId and not actual author name from Authors
                // We also use EF.Functions.Like to search case-insensitive but it will not work 100 % with ÅÄÖ characters
                books = books.Include(b => b.Author)
             .Where(b => EF.Functions.Like(b.Name, $"%{search}%") || EF.Functions.Like(b.Author.AuthorName, $"%{search}%"));

                // Return as a list accessed through "Model" variable name
            return View(books.ToList());
            }

        // Return empty model of books when no search string was provided!
        return View(new List<Book>());

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
