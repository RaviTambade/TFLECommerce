using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;

    ICustomerService _AuthSrv;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService authsrv)
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
            if (email == "sanika0239@gmail.com")
            {
                List<Customer> customers = _AuthSrv.getAllCustomers();
                ViewData["allCustomers"] = customers;
                return View();
            }
            else
            {
                return Error();
            }
        }

    }



    public IActionResult UpdateProfile(int id)
    {
        Customer customer = _AuthSrv.getCustomerById(id);
        return View(customer);
    }


    [HttpPost]
    public IActionResult Update(int id, string name, string password, string email,  string city)
    {
        Customer customer = new Customer
        {
            CustomerId = id,
            UserName = name,
            Password = password,
            Email = email,
            Address = city
        };
        
        bool status = _AuthSrv.updateCustomer(customer);
        if (status)
        {
            return RedirectToAction("index", "Profile");
        }
        else
        {
            return RedirectToAction("update", "Profile");
        }
    }


    // public IActionResult Delete(int id)
    // {
    //     bool status = _cartsrv.deleteItem(id);
    //     if (status)
    //     {
    //         return RedirectToAction("Index", "ShoppingCart");
    //     }
    //     return RedirectToAction("Index");
    // }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
