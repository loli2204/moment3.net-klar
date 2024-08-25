using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Books.Data;
using Books.Models;

namespace moment3.Controllers
{
    public class AuthorsController : Controller
    {
        // Fält för att hålla en instans av databaskontexten
        private readonly BooksDbContext _context;

        // Konstruktor som initierar databaskontexten genom dependency injection
        public AuthorsController(BooksDbContext context)
        {
            _context = context;
        }

        // GET: Authors
        // Hämtar och returnerar en lista över alla författare i databasen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        // GET: Authors/Details/5
        // Visar detaljer för en specifik författare baserat på id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        // GET: Authors/Create
        // Returnerar en vy för att skapa en ny författare
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // Skapar en ny författare i databasen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorName")] Author author)
        {
            if (ModelState.IsValid)
            {
                // Kontrollera om författarnamnet redan finns i databasen
                bool authorNameAlreadyUsed = await _context.Authors.AnyAsync(m => m.AuthorName == author.AuthorName);

                if (authorNameAlreadyUsed)
                {
                    return RedirectToAction(nameof(Create));
                }

                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        // Returnerar en vy för att redigera en befintlig författare baserat på id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // Uppdaterar en befintlig författare i databasen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorName")] Author author)
        {
            if (id != author.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        // Returnerar en vy för att ta bort en specifik författare baserat på id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        // Tar bort en specifik författare från databasen
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                // Kontrollera om författaren har några böcker kopplade till sig
                var authorHasBookAlready = await _context.Books.AnyAsync(m => m.AuthorId == id);

                if (authorHasBookAlready)
                {
                    return RedirectToAction(nameof(Index));
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Kontrollmetod för att se om en författare existerar i databasen
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
