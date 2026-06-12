using MesonConnect.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MesonConnect.Controllers

{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.CanConnect = _context.Database.CanConnect()
                    ? "Conexión Exitosa"
                    : "No se pudo conectar a la base de datos";

                var platillos = await _context.Platillo
                    .Include(p => p.CategoriaPlatillo)
                    .Select(p => new Platillo
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Categoria = p.CategoriaPlatillo!.Descripcion
                    })
                    .ToListAsync();

                // TESTIMONIOS
                ViewBag.Testimonios = await _context.Testimonio
          .Include(t => t.Cliente)
          .Where(t => !t.Estado)
          .OrderByDescending(t => t.Fecha)
          .ToListAsync();

                // PROMOCIONES
                ViewBag.Promociones = await _context.Promocion
                    .Where(p => p.Estado == true)
                    .ToListAsync();

                // PEDIDOS DEL CLIENTE LOGUEADO
                var clienteId = HttpContext.Session.GetString("ClienteId");

                if (!string.IsNullOrEmpty(clienteId))
                {
                    ViewBag.Pedidos = await _context.Pedidos
     .Where(p =>
         p.Cliente_id == Convert.ToInt64(clienteId) &&
         p.Estado != "Cancelado")
     .OrderByDescending(p => p.fecha_pedido)
     .ToListAsync();
                }
                else
                {
                    ViewBag.Pedidos = new List<Pedido>();
                }

                return View(platillos);
            }
            catch (Exception ex)
            {
                ViewBag.CanConnect = "Error: " + ex.Message;

                ViewBag.Testimonios = new List<Testimonio>();
                ViewBag.Promociones = new List<Promocion>();
                ViewBag.Pedidos = new List<Pedido>();

                return View(new List<Platillo>());
            }
        }

        [HttpPost]
        public IActionResult GuardarTestimonio(string mensaje, int calificacion)
        {
            var clienteId = HttpContext.Session.GetString("ClienteId");

            if (string.IsNullOrEmpty(clienteId))
            {
                return Json(new { success = false });
            }

            var testimonio = new Testimonio
            {
                IdCliente = Convert.ToInt64(clienteId),
                Mensaje = mensaje,
                Calificacion = calificacion,
                Estado = false, // pendiente de aprobación
                Fecha = DateTime.Now
            };

            _context.Testimonio.Add(testimonio);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        public IActionResult MiCuenta()
        {
            var clienteId = HttpContext.Session.GetString("ClienteId");

            if (string.IsNullOrEmpty(clienteId))
            {
                return View(null);
            }

            var cliente = _context.Clientes
                .FirstOrDefault(x => x.id.ToString() == clienteId);

            if (cliente == null)
            {
                return View(null);
            }

            return View(cliente);
        }

        public IActionResult ObtenerPedidosCliente()
        {
            var clienteId = HttpContext.Session.GetString("ClienteId");

            if (string.IsNullOrEmpty(clienteId))
            {
                return Json(new List<object>());
            }

            var pedidos = _context.Pedidos
       .Where(x =>
           x.Cliente_id == Convert.ToInt64(clienteId) &&
           x.Estado != "Cancelado")
       .OrderByDescending(x => x.fecha_pedido)
       .Select(x => new
       {
           x.id,
           Fecha = x.fecha_pedido.ToString("dd/MM/yyyy"),
           x.total,
           x.Estado
       })
       .ToList();

            return Json(pedidos);
        }


        [HttpPost]
        public IActionResult CrearPedido([FromBody] List<PedidoItem> items)
        {
            var clienteId = HttpContext.Session.GetString("ClienteId");

            if (string.IsNullOrEmpty(clienteId))
            {
                return Json(new
                {
                    noLogin = true
                });
            }

            if (items == null || !items.Any())
            {
                return Json(new
                {
                    success = false,
                    message = "Carrito vacío"
                });
            }

            decimal total = items.Sum(x => x.price * x.cantidad);

            var pedido = new Pedido
            {
                fecha_pedido = DateTime.Now,
                total = total,
                Cliente_id = Convert.ToInt64(clienteId),
                Estado = "Pendiente"
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            foreach (var item in items)
            {
                _context.DetallePedidos.Add(new DetallePedido
                {
                    PedidoId = pedido.id,
                    PlatilloId = item.platilloId,
                    Cantidad = item.cantidad,
                    Precio = item.price
                });
            }

            _context.SaveChanges();

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public IActionResult CancelarPedido([FromBody] PedidoRequest req)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.id == req.id);

            if (pedido == null)
                return Json(new { success = false });

            pedido.Estado = "Cancelado";

            _context.SaveChanges();

            return Json(new { success = true });
        }

        public class PedidoRequest
        {
            public long id { get; set; }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId =
                    Activity.Current?.Id ??
                    HttpContext.TraceIdentifier
            });
        }

        [HttpPost]
        public IActionResult BookTable(
            string Name,
            string Email,
            string Phone,
            DateTime Date,
            string Time,
            int People,
            string Message)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Contact(
            string Name,
            string Email,
            string Subject,
            string Message)
        {
            ViewBag.Message =
                "Mensaje enviado correctamente";

            return RedirectToAction("Index");
        }


    }
}