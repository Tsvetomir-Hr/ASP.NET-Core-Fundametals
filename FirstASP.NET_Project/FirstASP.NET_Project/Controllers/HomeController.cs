using FirstASP.NET_Project.Contracts;
using FirstASP.NET_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstASP.NET_Project.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ITestService _testService;

        public HomeController(
            ILogger<HomeController> logger,
            ITestService testService)
        {
            _logger = logger;
            this._testService = testService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Test()
        {
            var model = new TestModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Test(TestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string product = _testService.GetProduct(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}