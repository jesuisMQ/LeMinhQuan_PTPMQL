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
    public class IssueNoteDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueNoteDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IssueNoteDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IssueNoteDetails.Include(i => i.Device).Include(i => i.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IssueNoteDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueNoteDetails = await _context.IssueNoteDetails
                .Include(i => i.Device)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issueNoteDetails == null)
            {
                return NotFound();
            }

            return View(issueNoteDetails);
        }

        // GET: IssueNoteDetails/Create
        public IActionResult Create()
        {
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID");
            return View();
        }

        // POST: IssueNoteDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Device_ID,SupplierID,Cost,Quantity,SumAll")] IssueNoteDetails issueNoteDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueNoteDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID", issueNoteDetails.Device_ID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", issueNoteDetails.SupplierID);
            return View(issueNoteDetails);
        }

        // GET: IssueNoteDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueNoteDetails = await _context.IssueNoteDetails.FindAsync(id);
            if (issueNoteDetails == null)
            {
                return NotFound();
            }
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID", issueNoteDetails.Device_ID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", issueNoteDetails.SupplierID);
            return View(issueNoteDetails);
        }

        // POST: IssueNoteDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Device_ID,SupplierID,Cost,Quantity,SumAll")] IssueNoteDetails issueNoteDetails)
        {
            if (id != issueNoteDetails.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueNoteDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueNoteDetailsExists(issueNoteDetails.ID))
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
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID", issueNoteDetails.Device_ID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", issueNoteDetails.SupplierID);
            return View(issueNoteDetails);
        }

        // GET: IssueNoteDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueNoteDetails = await _context.IssueNoteDetails
                .Include(i => i.Device)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issueNoteDetails == null)
            {
                return NotFound();
            }

            return View(issueNoteDetails);
        }

        // POST: IssueNoteDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueNoteDetails = await _context.IssueNoteDetails.FindAsync(id);
            if (issueNoteDetails != null)
            {
                _context.IssueNoteDetails.Remove(issueNoteDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueNoteDetailsExists(int id)
        {
            return _context.IssueNoteDetails.Any(e => e.ID == id);
        }
    }
}
