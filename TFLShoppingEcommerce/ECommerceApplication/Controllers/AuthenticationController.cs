using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;

    IAuthenticationService _AuthSrv;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authsrv)
    {
        _logger = logger;
        _AuthSrv = authsrv;
    }

    
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        Customer customer = _AuthSrv.getCustomerByEmail(email);

        if (customer != null && password == "sanika123" && email == "sanika0239@gmail.com")
        {
            HttpContext.Session.SetString("Email", email);
            return RedirectToAction("index", "Customer");
        }
        else if (customer != null && password == customer.Password)
        {
            HttpContext.Session.SetString("Email", email);
            return RedirectToAction("index", "Home");
        }
        else
        {
            return View();
        }
    }
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(int id, string name,  string email, string password, string city)
    {
        Customer customer = new Customer(id, name, password, email,  city);
        bool status = _AuthSrv.addCustomer(customer);
        if (status)
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            return View();
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Authentication");
    }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
