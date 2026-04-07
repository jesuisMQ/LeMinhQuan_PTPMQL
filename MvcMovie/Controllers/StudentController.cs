using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovie.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
namespace MvcMovie.Controllers
{
    
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
       // GET: Movies
        public async Task<IActionResult> Index()
        {
            var model=await _context.Students.ToListAsync();
            return View(model);
        }
         public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("FullName,StudentCode,Email")] Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
        public async Task<IActionResult>Edit(String id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
                
            }
           
            var std =await _context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( string id, [Bind("FullName,StudentCode,Email")] Student std)
        {
            if(id!=std.StudentCode)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try{
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
            return View(std);
        }
        public async Task<IActionResult>Delete(String id)
        {
            if(id==null|| _context.Students == null)
            {
                return NotFound();
            }
            var std=await _context.Students.FirstOrDefaultAsync(q=>q.StudentCode==id);
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
            if (_context.Students==null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Students' is null.");
            }
            var std =await _context.Students.FindAsync(id);
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
            ViewBag.message="Hello "+st.FullName+ " "+st.StudentCode;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        private bool PersonExists(string id)
        {
            return(_context.Students?.Any(q=>q.StudentCode==id)).GetValueOrDefault();
        }

    }
}