using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models.Baithuchanh12;

namespace MvcMovie.Controllers
{
    public class IssueNoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueNoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IssueNote
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IssueNotes.Include(i => i.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IssueNote/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueNote = await _context.IssueNotes
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.IN_ID == id);
            if (issueNote == null)
            {
                return NotFound();
            }

            return View(issueNote);
        }

        // GET: IssueNote/Create
        public IActionResult Create()
        {
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID");
            return View();
        }

        // POST: IssueNote/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IN_ID,TimeStamp,SupplierID,Total")] IssueNote issueNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", issueNote.SupplierID);
            return View(issueNote);
        }

        // GET: IssueNote/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueNote = await _context.IssueNotes.FindAsync(id);
            if (issueNote == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", issueNote.SupplierID);
            return View(issueNote);
        }

        // POST: IssueNote/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IN_ID,TimeStamp,SupplierID,Total")] IssueNote issueNote)
        {
            if (id != issueNote.IN_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueNoteExists(issueNote.IN_ID))
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
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", issueNote.SupplierID);
            return View(issueNote);
        }

        // GET: IssueNote/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueNote = await _context.IssueNotes
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.IN_ID == id);
            if (issueNote == null)
            {
                return NotFound();
            }

            return View(issueNote);
        }

        // POST: IssueNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var issueNote = await _context.IssueNotes.FindAsync(id);
            if (issueNote != null)
            {
                _context.IssueNotes.Remove(issueNote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueNoteExists(string id)
        {
            return _context.IssueNotes.Any(e => e.IN_ID == id);
        }
    }
}
