using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MesonConnect.Models;

namespace MesonConnect.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var platillos = await (
                from p in _context.Platillo
                join c in _context.Set<CategoriaPlatillo>()
                    on p.CategoriaPlatilloId equals c.Id
                select new Platillo
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Categoria = c.Descripcion
                }
            ).ToListAsync();

            return View(platillos);
        }
    }
}