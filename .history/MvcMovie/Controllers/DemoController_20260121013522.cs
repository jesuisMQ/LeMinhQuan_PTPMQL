using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MvcMovie.Controllers
{
    public class DemoController : Controller
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello Lê Minh Quân - MSV: 2221050656";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
