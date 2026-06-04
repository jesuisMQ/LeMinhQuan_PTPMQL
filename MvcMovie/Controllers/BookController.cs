using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models.Baithuchanh13;
using MvcMovie.Models.ViewModels;

namespace MvcMovie.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            return View();
        }

         public async Task<IActionResult> GetBooks(int page = 1, int pageSize = 10)
        {
            var query = _context.Books
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedDate);

            var totalItems = await query.CountAsync();

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<Book>
            {
                Items = books,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return PartialView("_BookTable", result);
        }
    
        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

         [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", book);
            }

            book.CreatedDate = DateTime.Now;

            _context.Books.Add(book);

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true
            });
        }
    

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return PartialView("_Edit", book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", book);
            }

            var existingBook = await _context.Books.FindAsync(book.Id);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.ISBN = book.ISBN;
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publisher = book.Publisher;
            existingBook.PublishYear = book.PublishYear;
            existingBook.Price = book.Price;
            existingBook.Quantity = book.Quantity;
            existingBook.Category = book.Category;
            existingBook.Description = book.Description;
            existingBook.IsAvailable = book.IsAvailable;

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true
            });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return PartialView("_Delete", book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Book book)
        {
            var existingBook = await _context.Books
                .FindAsync(book.Id);

            if (existingBook == null)
            {
                return Json(new
                {
                    success = false
                });
            }

            _context.Books.Remove(existingBook);

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true
            });
        }
    }
}
