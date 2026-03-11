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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("FullName,StudentCode")] Student std)
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
            var std =await _context.Students.FindAsync(id);
            return View(std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student std)
        {
            _context.Students.Update(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Delete(String id)
        {
            var std=await _context.Students.FirstOrDefaultAsync(q=>q.StudentCode==id);

            return View(std);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(String id)
        {    var std =await _context.Students.FindAsync(id);
            _context.Students.Remove(std);
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
        

    }
}