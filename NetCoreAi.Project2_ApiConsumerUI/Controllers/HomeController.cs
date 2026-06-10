using Microsoft.AspNetCore.Mvc;
using NetCoreAi.Project2_ApiConsumerUI.Models;
using System.Diagnostics;

namespace NetCoreAi.Project2_ApiConsumerUI.Controllers;

public class HomeController : Controller
{
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
}
