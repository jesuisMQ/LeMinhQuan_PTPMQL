using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Controllers
{
    public class QuanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quan
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quan.ToListAsync());
        }

        // GET: Quan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quan = await _context.Quan
                .FirstOrDefaultAsync(m => m.Email == id);
            if (quan == null)
            {
                return NotFound();
            }

            return View(quan);
        }

        // GET: Quan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email")] Quan quan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quan);
        }

        // GET: Quan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quan = await _context.Quan.FindAsync(id);
            if (quan == null)
            {
                return NotFound();
            }
            return View(quan);
        }

        // POST: Quan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email")] Quan quan)
        {
            if (id != quan.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuanExists(quan.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quan);
        }

        // GET: Quan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quan = await _context.Quan
                .FirstOrDefaultAsync(m => m.Email == id);
            if (quan == null)
            {
                return NotFound();
            }

            return View(quan);
        }

        // POST: Quan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var quan = await _context.Quan.FindAsync(id);
            if (quan != null)
            {
                _context.Quan.Remove(quan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuanExists(string id)
        {
            return _context.Quan.Any(e => e.Email == id);
        }
    }
}
