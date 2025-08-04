using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class CatelogController : Controller
{
    private readonly ILogger<CatelogController> _logger;
    IProductService _srv;
    IProductRepository repo;

    public CatelogController(ILogger<CatelogController> logger,IProductService srv)
    {
        _logger = logger;
        _srv =srv;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            List<Product> products = _srv.getAllProduct();
            ViewData["allProducts"] = products;
            return View();
        }
    }

    public IActionResult DetailsWithId(int id)
    {
        Product product = _srv.getProductById(id);
        ViewData["productById"] = product;
        return View();
    }

    public IActionResult Details(int id)
    {
        List<Product> products = _srv.getCategoriesProduct(id);
        ViewData["productBycategory"] = products;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
