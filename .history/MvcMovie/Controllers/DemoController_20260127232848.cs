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
        [HttpPost]
        public IActionResult Form(string name)
        {   

            ViewBag.message=name;
            return View();
        }

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
