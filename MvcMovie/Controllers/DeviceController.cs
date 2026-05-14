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
    public class DeviceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Device
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Devices.Include(d => d.Supplier).Include(d=>d.DeviceTypes);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Device/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.Device_ID == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        public async Task<JsonResult> CreateAjax ([FromBody] Device device)
        {
           var type=_context.Devices.Include(d=>d.DeviceTypes).FirstOrDefault(q=>q.Device_ID==device.Device_ID)?.DeviceTypes?.TypeName;
            return Json(new
            {
                success = true,
                device_type=type,
                
            });
        }
        // GET: Device/Create
        public IActionResult Create()
        {
            ViewData["SupplierName"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierName");
            ViewData["Type_Name"] = new SelectList(_context.DeviceTypes, "Type_ID", "TypeName");
            return View();
        }

        // POST: Device/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Device_ID,DeviceName,Remain,Cost,SupplierID,Type_ID")] Device device)
        {
            if (_context.Devices.Any(d => d.Device_ID == device.Device_ID))
            {
                ModelState.AddModelError(
                    "Device_ID",
                    "Mã thiết bị đã tồn tại");
            }
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", device.SupplierID);
            ViewData["Type_Name"] = new SelectList(_context.DeviceTypes, "Type_ID", "TypeName",device.Type_ID);

            return View(device);
        }

        // GET: Device/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", device.SupplierID);
            return View(device);
        }

        // POST: Device/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Device_ID,DeviceName,Remain,Cost,SupplierID")] Device device)
        {
            if (id != device.Device_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Device_ID))
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
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", device.SupplierID);
            return View(device);
        }

        // GET: Device/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.Device_ID == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Device/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(string id)
        {
            return _context.Devices.Any(e => e.Device_ID == id);
        }
    }
}
