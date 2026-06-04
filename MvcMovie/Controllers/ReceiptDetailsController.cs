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
    public class ReceiptDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReceiptDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceiptDetails.Include(r => r.Device).Include(r => r.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceiptDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetails = await _context.ReceiptDetails
                .Include(r => r.Device)
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (receiptDetails == null)
            {
                return NotFound();
            }

            return View(receiptDetails);
        }

        // GET: ReceiptDetails/Create
        public IActionResult Create()
        {
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID");
            return View();
        }

        // POST: ReceiptDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Device_ID,SupplierID,Quantity,SumAll")] ReceiptDetails receiptDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID", receiptDetails.Device_ID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", receiptDetails.SupplierID);
            return View(receiptDetails);
        }

        // GET: ReceiptDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetails = await _context.ReceiptDetails.FindAsync(id);
            if (receiptDetails == null)
            {
                return NotFound();
            }
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID", receiptDetails.Device_ID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", receiptDetails.SupplierID);
            return View(receiptDetails);
        }

        // POST: ReceiptDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Device_ID,SupplierID,Quantity,SumAll")] ReceiptDetails receiptDetails)
        {
            if (id != receiptDetails.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptDetailsExists(receiptDetails.ID))
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
            ViewData["Device_ID"] = new SelectList(_context.Devices, "Device_ID", "Device_ID", receiptDetails.Device_ID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", receiptDetails.SupplierID);
            return View(receiptDetails);
        }

        // GET: ReceiptDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetails = await _context.ReceiptDetails
                .Include(r => r.Device)
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (receiptDetails == null)
            {
                return NotFound();
            }

            return View(receiptDetails);
        }

        // POST: ReceiptDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiptDetails = await _context.ReceiptDetails.FindAsync(id);
            if (receiptDetails != null)
            {
                _context.ReceiptDetails.Remove(receiptDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptDetailsExists(int id)
        {
            return _context.ReceiptDetails.Any(e => e.ID == id);
        }
    }
}
