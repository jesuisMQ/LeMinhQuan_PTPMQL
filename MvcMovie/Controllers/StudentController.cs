using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovie.Models.Entities;
using MvcMovie.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using MvcMovie.Models.Process;
using OfficeOpenXml;
namespace MvcMovie.Controllers
{

    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult DownLoad()
        {
            var fileName ="YourFileName"+".xlsx";
            ExcelPackage.License.SetNonCommercialPersonal("StudentProject");
            using (ExcelPackage excelPackage=new ExcelPackage())
            {
                ExcelWorksheet ws= excelPackage.Workbook.Worksheets.Add("Sheet 1");
                ws.Cells["A1"].Value="StudentID";
                ws.Cells["B1"].Value="FullName";
                ws.Cells["C1"].Value="Email";
                ws.Cells["D1"].Value="Faculty";
                var stdList =_context.Students.ToList();
                ws.Cells["A2"].LoadFromCollection(stdList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            }
        }
        // GET: Movies
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
            {
                ModelState.AddModelError("", "Please choose a file");
                return View();
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (fileExtension != ".xls" && fileExtension != ".xlsx" )
            {
                ModelState.AddModelError("", "Please choose Excel file to upload");
                return View();
            }

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Excels");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExtension;
            var filePath = Path.Combine(folder, fileName);

            // 💾 SAVE FILE
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 📖 READ EXCEL (PHẢI ĐỂ NGOÀI USING)
            var data = ExcelProcess.ImportFromExcel(filePath);

            // 💾 SAVE DB
            foreach (var item in data)
            {
                var std = item.student;

                _context.Add(std);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            var result = await _context.Students
                            .Select(s => new StudentVM
                            {
                                StudentCode = s.StudentCode,
                                FullName = s.FullName,
                                FacultyName = s.Faculty!.FacultyName
                            })
                            .ToListAsync();
            return View(result);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
            .Where(s => s.StudentCode == id)   // ✔ lọc trước
            .Select(s => new StudentVM
            {
                StudentCode = s.StudentCode,
                FullName = s.FullName,
                FacultyName = s.Faculty != null ? s.Faculty.FacultyName : "N/A"
            })
            .FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("FullName,StudentCode,Email,FacultyID")] Student std)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(std);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("StudentCode", "Mã sinh viên bị trùng");
                }
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();

            }

            var std = await _context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FullName,StudentCode,Email,FacultyID")] Student std)
        {
            if (id != std.StudentCode)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Students.Update(std);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(std.StudentCode))
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
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }
        public async Task<IActionResult> Delete(String id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }
            var std = await _context.Students.FirstOrDefaultAsync(q => q.StudentCode == id);
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(String id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Students' is null.");
            }
            var std = await _context.Students.FindAsync(id);
            if (std != null)
            {
                _context.Students.Remove(std);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Form_Models()   // ⬅ GET
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form_Models(Student st)
        {
            ViewBag.message = "Hello " + st.FullName + " " + st.StudentCode;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        private bool PersonExists(string id)
        {
            return (_context.Students?.Any(q => q.StudentCode == id)).GetValueOrDefault();
        }

    }
}