using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models.Baithuchanh9;
using MvcMovie.Models.ViewModels;
namespace MvcMovie.Controllers
{
    public class ChitietDHController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChitietDHController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChitietDH
        public async Task<IActionResult> Search(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(CustomerFullVM vm)
        {
            if (string.IsNullOrEmpty(vm.CustomerID))
            {
                ModelState.AddModelError("", "Nhập CustomerID");
                return View(vm);
            }

            // lấy customer
            var customer = _context.Customer
                .FirstOrDefault(c => c.CustomerID == vm.CustomerID);

            if (customer == null)
            {
                ModelState.AddModelError("", "Không tìm thấy khách hàng");
                return View(vm);
            }

            // lấy đơn hàng
            var orders = _context.Orders
                .Where(o => o.CustomerID == vm.CustomerID)
                .ToList();

            // lấy chi tiết đơn hàng
            var details = _context.Details
                .Where(d => orders.Select(o => o.OrderID).Contains(d.OrderID))
                .ToList();

            vm.Customer = customer;
            // thiếu dòng này
            ViewBag.Orders = orders;
            ViewBag.Details = details;
            return View(vm);
        }
        // GET: ChitietDH/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietDH = await _context.Details
                .Include(c => c.Orders)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chitietDH == null)
            {
                return NotFound();
            }

            return View(chitietDH);
        }

        // GET: ChitietDH/Create
        public IActionResult Create()
        {
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID");
            return View();
        }

        // POST: ChitietDH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OrderID,ProductID,Quantity,Price,SumAll")] ChitietDH chitietDH)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietDH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", chitietDH.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", chitietDH.ProductID);
            return View(chitietDH);
        }

        // GET: ChitietDH/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietDH = await _context.Details.FindAsync(id);
            if (chitietDH == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", chitietDH.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", chitietDH.ProductID);
            return View(chitietDH);
        }

        // POST: ChitietDH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OrderID,ProductID,Quantity,Price,SumAll")] ChitietDH chitietDH)
        {
            if (id != chitietDH.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietDH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietDHExists(chitietDH.ID))
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
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", chitietDH.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", chitietDH.ProductID);
            return View(chitietDH);
        }

        // GET: ChitietDH/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietDH = await _context.Details
                .Include(c => c.Orders)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chitietDH == null)
            {
                return NotFound();
            }

            return View(chitietDH);
        }

        // POST: ChitietDH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chitietDH = await _context.Details.FindAsync(id);
            if (chitietDH != null)
            {
                _context.Details.Remove(chitietDH);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietDHExists(int id)
        {
            return _context.Details.Any(e => e.ID == id);
        }
    }
}
