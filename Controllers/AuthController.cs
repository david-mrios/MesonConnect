using MesonConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // ADMIN FIJO
        if (username == "admin" && password == "1234")
        {
            HttpContext.Session.SetString("Usuario", "Administrador");
            HttpContext.Session.SetString("Rol", "Admin");

            return RedirectToAction("Index", "Dashboards");
        }

        // CLIENTES DE LA BASE DE DATOS
        var cliente = _context.Clientes
            .FirstOrDefault(x => x.correo == username);

        if (cliente == null || cliente.contrasena != password)
        {
            TempData["ErrorLogin"] =
                "El correo o la contraseña son incorrectos.";

            return View();
        }

        HttpContext.Session.SetString("Usuario", cliente.nombre);
        HttpContext.Session.SetString("ClienteId", cliente.id.ToString());
        HttpContext.Session.SetString("Rol", "Cliente");

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Register(
        string name,
        string email,
        string phone,
        string address,
        string password)
    {
        var existe = _context.Clientes.FirstOrDefault(x => x.correo == email);

        if (existe != null)
        {
            ViewBag.Error = "El correo ya está registrado";
            return View("Login");
        }

        var cliente = new Cliente
        {
            nombre = name,
            correo = email,
            telefono = phone,
            direccion = address,
            contrasena = password
        };

        _context.Clientes.Add(cliente);
        _context.SaveChanges();

        HttpContext.Session.SetString("ClienteId", cliente.id.ToString());
        HttpContext.Session.SetString("Usuario", cliente.nombre);

        return RedirectToAction("Index", "Home");
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = "";
    }
    [HttpPost]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var cliente = _context.Clientes
            .FirstOrDefault(x => x.correo == request.Email);

        if (cliente == null)
        {
            return Json(new { success = false });
        }

        // Aquí enviarás correo
        return Json(new { success = true });
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}