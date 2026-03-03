using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models.Entities;

namespace MvcMovie.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.message = "Xin chào Lê Minh Quân";
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(string name)
        {
            ViewBag.message = "Hello " + name;
            return View();
        }

        public IActionResult Form_Models()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form_Models(Student st)
        {
            ViewBag.message = "Hello " + st.FullName + " " + st.StudentCode;
            return View();
        }
    }
}
