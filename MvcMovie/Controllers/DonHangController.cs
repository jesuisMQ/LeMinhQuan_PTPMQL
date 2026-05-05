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
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DonHang
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(d => d.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DonHang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Orders
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: DonHang/Create
        public IActionResult Create(string? khachHangId, string id)
        {
            ViewData["CustomerID"] = new SelectList(
                _context.Customer,
                "CustomerID",
                "CustomerID",
                khachHangId // 👈 cái này tự chọn sẵn
            );
            if (id != null)
            {
                ViewData["CustomerID"] = new SelectList(
                _context.Customer,
                "CustomerID",
                "CustomerID",
                id // 👈 cái này tự chọn sẵn
            );
            }
            ViewData["ProductID"] = new SelectList(
                _context.Products,
                "ProductID",
                "ProductName"
            );
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonHangVM vm)
        {
            var lastOrder = _context.Orders
                            .OrderByDescending(o => o.OrderID)
                            .FirstOrDefault();
            string newId;

            if (lastOrder == null)
            {
                newId = "001";
            }
            else
            {
                int number = int.Parse(lastOrder.OrderID.Replace("DH", ""));
                number++;

                newId = "DH" + number.ToString("D3"); // format 3 chữ số
            }
            // 1. validate
            foreach (var item in vm.Products)
            {
                var product = _context.Products
                    .FirstOrDefault(x => x.ProductID == item.ProductID);

                if (product == null)
                    return NotFound();

                if (product.Remain < item.Quantity)
                {
                    ModelState.AddModelError("", $"Sản phẩm {item.ProductID}, {product.ProductName} không đủ hàng");

                    ViewBag.CustomerID = new SelectList(_context.Customer, "CustomerID", "CustomerID", vm.CustomerID);
                    ViewBag.ProductID = new SelectList(_context.Products, "ProductID", "ProductName");

                    return View(vm);
                }
            }

            // 2. tạo đơn hàng
            var donHang = new DonHang
            {
                OrderID = newId,
                CustomerID = vm.CustomerID,
                Timestamp = DateTime.Now,
                Total = vm.SumAll
            };

            _context.Orders.Add(donHang);
            await _context.SaveChangesAsync();

            // 3. tạo chi tiết + trừ kho
            foreach (var item in vm.Products)
            {
                var product = _context.Products
                    .FirstOrDefault(x => x.ProductID == item.ProductID);

                product.Remain -= item.Quantity;

                var ct = new ChitietDH
                {
                    OrderID = donHang.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    SumAll = item.Quantity * item.Price
                };

                await _context.Details.AddAsync(ct);
            }

            // 4. save
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        public JsonResult GetProduct(string productId)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                return Json(null);
            }

            return Json(new
            {
                productID = product.ProductID,
                price = product?.Price ?? 0
            });
        }

        // GET: DonHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Orders.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID", donHang.CustomerID);
            return View(donHang);
        }


        // POST: DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderID,CustomerID,Timestamp")] DonHang donHang)
        {
            if (id != donHang.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.OrderID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID", donHang.CustomerID);
            return View(donHang);
        }

        // GET: DonHang/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.Orders
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var donHang = await _context.Orders.FindAsync(id);
            var CT =  _context.Details.Where(x => x.OrderID == id);
            if (donHang != null&& CT != null)
            {
                _context.Details.RemoveRange(CT);
                _context.Orders.Remove(donHang);
                
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(string id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
