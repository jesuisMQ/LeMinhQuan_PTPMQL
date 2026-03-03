using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace MvcMovie.Controllers
{
    public class DemoController : Controller
    { 
        public string Index()
        {   ViewBag.message="Xin chào Lê Minh Quân";
            return "Xin chào Lê Minh Quân";
        }

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
