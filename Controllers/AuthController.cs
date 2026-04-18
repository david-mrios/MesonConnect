using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "1234")
        {
            return RedirectToAction("Index", "Admin");
        }

        ViewBag.Error = "Invalid credentials";
        return View();
    }

    [HttpPost]
    public IActionResult Register(string name, string email, string password)
    {   
        ViewBag.Message = "User registered successfully";
        return View("Login");
    }
}