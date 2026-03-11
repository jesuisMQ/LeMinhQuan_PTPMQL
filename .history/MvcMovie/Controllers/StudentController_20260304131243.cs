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
namespace MvcMovie.Controllers
{
    
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        
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