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
    public class DeviceTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeviceType
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeviceTypes.ToListAsync());
        }

        // GET: DeviceType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes
                .FirstOrDefaultAsync(m => m.Type_ID == id);
            if (deviceType == null)
            {
                return NotFound();
            }

            return View(deviceType);
        }
        [HttpPost]
        public async Task<JsonResult> CreateAjax ([FromBody] DeviceType devicetype)
        {
            if (_context.DeviceTypes.Any(q => q.Type_ID == devicetype.Type_ID))
            {
                return Json(new
                {
                    success = false,
                    message = "Mã NCC đã tồn tại"
                });
            }
            _context.DeviceTypes.Add(devicetype);

            await _context.SaveChangesAsync();
            return Json(new
            {
                success = true,
                typeID = devicetype.Type_ID,
                typeName = devicetype.TypeName
            });
        }

        // GET: DeviceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeviceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type_ID,TypeName")] DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deviceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deviceType);
        }

        // GET: DeviceType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound();
            }
            return View(deviceType);
        }

        // POST: DeviceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Type_ID,TypeName")] DeviceType deviceType)
        {
            if (id != deviceType.Type_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deviceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceTypeExists(deviceType.Type_ID))
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
            return View(deviceType);
        }

        // GET: DeviceType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceType = await _context.DeviceTypes
                .FirstOrDefaultAsync(m => m.Type_ID == id);
            if (deviceType == null)
            {
                return NotFound();
            }

            return View(deviceType);
        }

        // POST: DeviceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType != null)
            {
                _context.DeviceTypes.Remove(deviceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceTypeExists(string id)
        {
            return _context.DeviceTypes.Any(e => e.Type_ID == id);
        }
    }
}
