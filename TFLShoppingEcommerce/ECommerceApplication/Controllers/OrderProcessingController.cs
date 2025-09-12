using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using Org.BouncyCastle.Security;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Controllers;

public class OrderProcessingController : Controller
{
    private readonly ILogger<OrderProcessingController> _logger;
    IOrderProcessingService _orderSrv;
    ICustomerService _AuthSrv;
    ICustomerAddressRepository _custAddRepo;
    public OrderProcessingController(ILogger<OrderProcessingController> logger, IOrderProcessingService ordersrv, ICustomerService authrepo, ICustomerAddressRepository custAddRepo)
    {
        _logger = logger;
        _orderSrv = ordersrv;
        _AuthSrv = authrepo;
        _custAddRepo = custAddRepo;
    }


    public IActionResult PlaceOrder()
    {
        string email = HttpContext.Session.GetString("Email");
        Customer customers = _AuthSrv.getCustomerByEmail(email);

        ShippingAddress shippingAddress = _custAddRepo.getAllCustomerAddresses(customers.CustomerId).First();
        bool status = _orderSrv.placeOrder(customers.CustomerId, shippingAddress.AddressId);
        if (status)
        {
            return RedirectToAction("index", "Catelog");
        }
        else
        {
            return RedirectToAction("index", "ShoppingCart");
        }
    }

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
