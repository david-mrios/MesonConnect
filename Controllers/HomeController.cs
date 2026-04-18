using MesonConnect.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MesonConnect.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult BookTable(string Name, string Email, string Phone, DateTime Date, string Time, int People, string Message)
        {
            // Aquí luego puedes guardar en BD

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Contact(string Name, string Email, string Subject, string Message)
        {
            // Aquí puedes guardar en BD o enviar correo

            ViewBag.Message = "Mensaje enviado correctamente";
            return View("Index");
        }
    }
}
