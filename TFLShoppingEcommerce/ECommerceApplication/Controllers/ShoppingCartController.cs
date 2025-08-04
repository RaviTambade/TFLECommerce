using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class ShoppingCartController : Controller
{
    private readonly ILogger<ShoppingCartController> _logger;
    IShoppingCartService _cartsrv;
    IProductService _productsrv;
    IAuthenticationService _AuthSrv;

    public ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartService cartsrv, IProductService productsrv, IAuthenticationService authsrv)
    {
        _logger = logger;
        _cartsrv = cartsrv;
        _productsrv = productsrv;
        _AuthSrv = authsrv;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            Customer user = _AuthSrv.getCustomerByEmail(HttpContext.Session.GetString("Email"));

            List<Item> items = _cartsrv.getAllItem(user.CustomerId);
            ViewData["allItems"] = items;
            return View();
        }
    }

    public IActionResult Insert(int id)
    {
        Product product = _productsrv.getProductById(id);
        return View(product);
    }

    [HttpPost]
    public IActionResult Insert(int id, string title, string img, int unitprice, int quantity)
    {
        Product product = new Product
        {
            ProductId = id,
            ProductTitle = title,
            ProductImage = img,
            UnitPrice = unitprice
        };
        Item item = new Item(product, quantity);
        Customer user = _AuthSrv.getCustomerByEmail(HttpContext.Session.GetString("Email"));
        bool status = _cartsrv.addItem(item,user.CustomerId);
        if (status)
        {
            return RedirectToAction("index", "Catelog");
        }
        else
        {
            return RedirectToAction("Insert", "ShoppingCart");
        }
    }

    public IActionResult Update(int id)
    {
        Customer user = _AuthSrv.getCustomerByEmail(HttpContext.Session.GetString("Email"));
        Item item = _cartsrv.getItemById(id,user.CustomerId);
        return View(item);
    }

    [HttpPost]
    public IActionResult Update(int id, string title, string img, int unitprice, int quantity)
    {
        Product product = new Product
        {
            ProductId = id,
            ProductTitle = title,
            ProductImage = img,
            UnitPrice = unitprice
        };
        Item item = new Item(product, quantity);
        bool status = _cartsrv.updateItem(item);
        if (status)
        {
            return RedirectToAction("index", "ShoppingCart");
        }
        else
        {
            return RedirectToAction("update", "ShoppingCart");
        }
    }

    public IActionResult Delete(int id)
    {
        bool status = _cartsrv.deleteItem(id);
        if (status)
        {
            return RedirectToAction("Index", "ShoppingCart");
        }
        return RedirectToAction("Index");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
