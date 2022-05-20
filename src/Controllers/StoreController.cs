using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace src.Controllers;

public class StoreController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProjectContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public StoreController(ProjectContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index(string searchString, int? pageNumber) {
        int pageSize = 8;
        ViewData["CurrentFilter"] = searchString;

        IQueryable<Product> products = _context.Products;
        if (!String.IsNullOrEmpty(searchString)) {
            products = products.Where(p => p.name.Contains(searchString));
        }

        return View(PagedList<Product>.Create(products, pageNumber ?? 1, pageSize));
    }

    public IActionResult Details(int id) {
        if (id == null) {
            return NotFound();
        }
        Product p = _context.Products.FirstOrDefault(p => p.ProductID == id);
        if (p == null) {
            return NotFound();
        }
        return View(p);
    }

    public IActionResult Cart() {
        string userId = _userManager.GetUserId(User);
        var cart = _context.Cart.Where(c => c.ApplicationUserID == userId).Include(c => c.product);
        return View(cart);
    }

    public IActionResult AddToCart(int id, int quantity) {
        Cart c = new Cart {
            quantity = quantity,
            ApplicationUserID = _userManager.GetUserId(User),
            ProductID = id
        };

        _context.Add(c);
        _context.SaveChangesAsync();
        return RedirectToAction(nameof(Cart));
    }

    public IActionResult DeleteCart(int id) {
        Cart c = _context.Cart.Find(id);
        _context.Remove(c);
        _context.SaveChanges();
        return RedirectToAction(nameof(Cart));
    }

    public IActionResult DeleteData() {
        var products = _context.Products;
        foreach (var x in products) {
            _context.Remove(x);
        }
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Checkout() {
        return View();
    }

    public IActionResult ClearCart(string id) {
        var cart = _context.Cart.Where(c => c.ApplicationUserID == _userManager.GetUserId(User));
        foreach (var c in cart) {
            _context.Remove(c);
        }
        _context.SaveChanges();

        return RedirectToAction(nameof(Cart));
    }

    public IActionResult GenerateData() {
        Product p = new Product{
                price = 19.99f,
                name = "Frame 1",
                imageFileName = "1.png",
                modelFileName = "1.gltf"
        };
        Product p2 = new Product{
                price = 24.49f,
                name = "Frame 2",
                imageFileName = "2.png",
                modelFileName = "2.gltf"
        };
        Product p3 = new Product{
                price = 14.49f,
                name = "Frame 3",
                imageFileName = "3.png",
                modelFileName = "3.gltf"
        };
        Product p4 = new Product{
                price = 18.99f,
                name = "Frame 4",
                imageFileName = "4.png",
                modelFileName = "4.gltf"
        };
        Product p5 = new Product{
                price = 5.99f,
                name = "Decor 1",
                imageFileName = "5.png",
                modelFileName = "5.gltf"
        };
        Product p6 = new Product{
                price = 8.99f,
                name = "Decor 2",
                imageFileName = "6.png",
                modelFileName = "6.gltf"
        };
        _context.Add(p);
        _context.Add(p2);
        _context.Add(p3);
        _context.Add(p4);
        _context.Add(p5);
        _context.Add(p6);
        _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
