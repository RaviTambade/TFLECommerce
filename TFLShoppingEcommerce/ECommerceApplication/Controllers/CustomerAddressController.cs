using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.Services;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Controllers;

public class CustomerAddressController : Controller
{
    private readonly ILogger<CustomerAddressController> _logger;

    ICustomerService _AuthSrv;

    ICustomerAddressRepository _Addrepo;

    public CustomerAddressController(ILogger<CustomerAddressController> logger, ICustomerService authsrv, ICustomerAddressRepository addrepo)
    {
        _logger = logger;
        _AuthSrv = authsrv;
        _Addrepo = addrepo;
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
            Customer customer = _AuthSrv.getCustomerByEmail(email);
            List<ShippingAddress> customerAddress = _Addrepo.getAllCustomerAddresses(customer.CustomerId);
            ViewData["allcustomeraddress"] = customerAddress;
            return View();

        }

    }


    public IActionResult AddAddress(int id)
    {
        return View();
    }


    [HttpPost]
    public IActionResult AddAddress(int addressid, string address, string city, string state, int zipcode, string country)
    {
        string email = HttpContext.Session.GetString("Email");
        Customer customer = _AuthSrv.getCustomerByEmail(email);

        ShippingAddress shippingAddress = new ShippingAddress
        {
            AddressId = addressid,
            Address = address,
            City = city,
            State = state,
            Country = country,
            ZipCode = zipcode
        };

        bool status = _Addrepo.addCustomerAddress(shippingAddress,customer.CustomerId);
        if (status)
        {
            return RedirectToAction("index", "CustomerAddress");
        }
        else
        {
            return RedirectToAction("AddAddress", "CustomerAddress");
        }
    }



    public IActionResult UpdateAddress(int id)
    {
        ShippingAddress shipaddress = _Addrepo.getCustomerAddressById(id);
        return View(shipaddress);
    }


    [HttpPost]
    public IActionResult UpdateAddress(int addressid, string address, string city, string state, int zipcode,string country)
    {
        string email = HttpContext.Session.GetString("Email");
        Customer customer = _AuthSrv.getCustomerByEmail(email);

        ShippingAddress shippingAddress = new ShippingAddress
        {
            AddressId= addressid,
            Address = address,
            City = city,
            State = state,
            Country = country,
            ZipCode = zipcode
        };

        bool status = _Addrepo.updateCustomerAddress(shippingAddress);
        if (status)
        {
            return RedirectToAction("index", "CustomerAddress");
        }
        else
        {
            return RedirectToAction("UpdateAddress", "CustomerAddress");
        }
    }


    public IActionResult DeleteAddress(int id)
    {
        bool status = _Addrepo.deleteCustomerAddress(id);
        if (status)
        {
            return RedirectToAction("Index", "Profile");
        }
        return RedirectToAction("Index");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
