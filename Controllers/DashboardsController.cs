using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MesonConnect.Models;

namespace AspnetCoreMvcFull.Controllers;

public class DashboardsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
