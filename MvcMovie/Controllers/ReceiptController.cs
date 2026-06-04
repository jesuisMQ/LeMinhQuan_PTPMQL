using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;
using MvcMovie.Models.Baithuchanh12;
using MvcMovie.Models.ViewModels;

namespace MvcMovie.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Receipt
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Receipts.Include(r => r.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Receipt/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.ReceiptID == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // GET: Receipt/Create
        public IActionResult Create(string? SupplierId)
        {
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID",SupplierId);
            ViewData["Device"] = _context.Devices.ToList();

            return View();
        }

        // POST: Receipt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceiptVM receipt)
        {
            var lastReceipt= _context.Receipts.OrderByDescending(o=>o.ReceiptID).FirstOrDefault();
            string newId;
            if (lastReceipt == null)
            {
                newId="Receipt001";
            }
            else
            {
                int num = int.Parse(lastReceipt.ReceiptID.Replace("Receipt", ""));
                num++;
                newId = "Receipt" + num.ToString("D3");
            }
            if (ModelState.IsValid)
            {
                var rcp = new Receipt
                {
                    ReceiptID = newId,
                    SupplierID = receipt.SupplierID,
                    TimeStamp = DateTime.Now,
                    Total = receipt.Total
                };
                _context.Receipts.Add(rcp);
                await _context.SaveChangesAsync();
                


                foreach (var item in receipt.Goods?? new List<ReceiptDetails>())
                {

                    var ct = new ReceiptDetails
                    {
                        ReceiptID = rcp.ReceiptID,
                        Device_ID = item.Device_ID,
                        Quantity = item.Quantity,
                        Cost = item.Cost,
                        SumAll = item.Quantity * item.Cost
                    };

                    var device = _context.Devices.FirstOrDefault(d =>d.Device_ID == item.Device_ID);

                    if(device != null)
                    {
                        // giá trị tồn cũ
                        decimal oldTotal =
                            device.Remain * device.Cost;

                        // giá trị nhập mới
                        decimal newTotal =
                            item.Quantity * item.Cost;

                        // tổng SL mới
                        int totalQty =
                            device.Remain + item.Quantity;

                        // giá TB mới
                        decimal avgCost =
                            (oldTotal + newTotal)
                            / totalQty;

                        // update device
                        device.Cost = avgCost;
                        device.Remain = totalQty;
                    }

                    await _context.ReceiptDetails.AddAsync(ct);
                }

                // 4. save
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", receipt.SupplierID);
            ViewData["Device"] = new SelectList(_context.Devices, "Device_ID", "DeviceName");
            return View(receipt);

        }
        public JsonResult GetGood(string deviceid)
        {
            var product = _context.Devices
                .FirstOrDefault(p => p.Device_ID == deviceid);
            if (product == null)
            {
                return Json(null);
            }

            return Json(new
            {
                productID = product.Device_ID,
                price = product?.Cost ?? 0
            });
        }

        // GET: Receipt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", receipt.SupplierID);
            return View(receipt);
        }

        // POST: Receipt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReceiptID,TimeStamp,SupplierID,Total")] Receipt receipt)
        {
            if (id != receipt.ReceiptID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.ReceiptID))
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
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", receipt.SupplierID);
            return View(receipt);
        }

        // GET: Receipt/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.ReceiptID == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(string id)
        {
            return _context.Receipts.Any(e => e.ReceiptID == id);
        }
    }
}
