using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    { 
        // GET: /HelloWorld/
        public string Index()
        {
            return "Hello Lê Minh Quân - 2221050656";
        } 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
