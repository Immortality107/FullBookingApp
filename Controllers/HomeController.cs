
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookingApp.Models;

namespace MyBookingServiceProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("Home/About")]
        public IActionResult About()
        {
            return View();
        }
        [Route("Home/Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("Home/LocalChooseSession")]
        public IActionResult LocalChooseSession()
        {
            return View("EgyptiansSessionTypeView");
        }
        [Route("Home/InternationalChooseSession")]
        public IActionResult InternationalChooseSession()
        {
            return View("InternationalSessionTypeView");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
