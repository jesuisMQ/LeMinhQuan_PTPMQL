using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace MvcMovie.Controllers
{
    public class DemoController : Controller
    { 
        public IActionResult Index()
        {   
            ViewBag.message="Xin chào Lê Minh Quân";
            return View();
        }
          public IActionResult Form()   // ⬅ GET
        {   
            return View();
        } 
        [HttpPost]
        public IActionResult Form(string name)
        {   
            ViewBag.message="Hello "+name;
            return View();
        }

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
