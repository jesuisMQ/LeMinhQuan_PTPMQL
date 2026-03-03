using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovie.Models.Entities;
namespace MvcMovie.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {

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