using MesonConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MesonConnect.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(
            ILogger<AdminController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccountSettings(string estado)
        {
            var testimonios = _context.Testimonio
                .Include(t => t.Cliente)
                .AsQueryable();

            if (estado == "pendiente")
            {
                testimonios = testimonios.Where(x => !x.Estado);
            }

            if (estado == "aprobado")
            {
                testimonios = testimonios.Where(x => x.Estado);
            }

            return View(
                testimonios
                .OrderByDescending(x => x.Fecha)
                .ToList()
            );
        }

        public IActionResult AprobarTestimonio(long id)
        {
            var testimonio = _context.Testimonio
                .FirstOrDefault(x => x.IdTestimonio == id);

            if (testimonio != null)
            {
                testimonio.Estado = true;
                _context.SaveChanges();
            }

            return RedirectToAction("AccountSettings");
        }

        public IActionResult EliminarTestimonio(long id)
        {
            var testimonio = _context.Testimonio
                .FirstOrDefault(x => x.IdTestimonio == id);

            if (testimonio != null)
            {
                _context.Testimonio.Remove(testimonio);
                _context.SaveChanges();
            }

            return RedirectToAction("AccountSettings");
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}