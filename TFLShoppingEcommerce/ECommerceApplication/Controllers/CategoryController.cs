using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    ICategoryService _categorysrv;
    IProductService _productsrv;

    public CategoryController(ILogger<CategoryController> logger, ICategoryService categorysrv, IProductService productService)
    {
        _logger = logger;
        _categorysrv = categorysrv;
        _productsrv = productService;
    }

    public IActionResult Flowers()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Categories categories = _categorysrv.getCategoryByName("flowers");
            List<Product> products = _productsrv.getProductByCategoryId(categories.CategoryId);
            ViewData["flowerProducts"] = products;
            return View();
        }
    }
    
    public IActionResult ElectronicDevices()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Categories categories = _categorysrv.getCategoryByName("Electronic Devices");
            List<Product> products = _productsrv.getProductByCategoryId(categories.CategoryId);
            ViewData["ElectronicDevicesProducts"] = products;
            return View();
        }
    }

    public IActionResult Fashion()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Categories categories = _categorysrv.getCategoryByName("Fashion");
            List<Product> products = _productsrv.getProductByCategoryId(categories.CategoryId);
            ViewData["FashionProducts"] = products;
            return View();
        }
    }

    public IActionResult Home_Farniture()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Categories categories = _categorysrv.getCategoryByName("Home & Furniture");
            List<Product> products = _productsrv.getProductByCategoryId(categories.CategoryId);
            ViewData["Home_FarnitureProducts"] = products;
            return View();
        }
    }


    public IActionResult Beauty_PersonalCare()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Categories categories = _categorysrv.getCategoryByName("Beauty & Personal Care");
            List<Product> products = _productsrv.getProductByCategoryId(categories.CategoryId);
            ViewData["Beauty_PersonalCareProducts"] = products;
            return View();
        }
    }
    
  
    public IActionResult Toys_Books()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Categories categories = _categorysrv.getCategoryByName("Toys & Books");
            List<Product> products = _productsrv.getProductByCategoryId(categories.CategoryId);
            ViewData["Toys_BooksProducts"] = products;
            return View();
        }
    }

    public IActionResult Grocery()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Categories categories = _categorysrv.getCategoryByName("Grocery");
            List<Product> products = _productsrv.getProductByCategoryId(categories.CategoryId);
            ViewData["GroceryProducts"] = products;
            return View();
        }
    }
     


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
