using MesonConnect.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MesonConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // 1. Añadimos el campo privado para la base de datos
        private readonly ApplicationDbContext _context;

        // 2. Modificamos el constructor para recibir AMBOS: logger y context
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; // Guardamos la conexión
        }

        public IActionResult Index()
        {
            // 3. Prueba rápida: Si CanConnect() es true, SQL está respondiendo.
            try
            {
                ViewBag.CanConnect = _context.Database.CanConnect()
                    ? "Conexión Exitosa"
                    : "No se pudo conectar (¿Creaste la BD?)";
            }
            catch (Exception ex)
            {
                ViewBag.CanConnect = "Error: " + ex.Message;
            }

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
