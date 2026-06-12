using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MesonConnect.Models;

namespace AspnetCoreMvcFull.Controllers;

public class IconsController : Controller
{
  public IActionResult Boxicons() => View();
}
