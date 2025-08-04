using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    IAuthenticationService _AuthSrv;

    public ProfileController(ILogger<ProfileController> logger, IAuthenticationService authsrv)
    {
        _logger = logger;
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
            string email = HttpContext.Session.GetString("Email");
            Customer customers = _AuthSrv.getCustomerByEmail(email);
            ViewData["CustomerProfile"] = customers;
            return View();
        }
    }

    // public IActionResult Login()
    // {
    //     return View();
    // }

    // [HttpPost]
    // public IActionResult Login(string email, string password)
    // {
    //     Customer customer = _AuthSrv.getCustomerByEmail(email);

    //     if (customer != null && password == "sanika123" && email == "sanika0239@gmail.com")
    //     {
    //         return RedirectToAction("index", "Authentication");
    //     }
    //     else if (customer != null && password == customer.Password)
    //     {
    //         HttpContext.Session.SetString("Email", email);
    //         HttpContext.Session.SetString("Password", password);
    //         return RedirectToAction("index", "Home");
    //     }
    //     else
    //     {
    //         return View();
    //     }
    // }


    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
