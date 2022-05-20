using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Data;

namespace src.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProjectContext _context;

    public HomeController(ProjectContext context, ILogger<HomeController> logger)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        IQueryable<Product>? products = _context.Products;

        return View(products.Take(4));
    }

    public IActionResult Products()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
