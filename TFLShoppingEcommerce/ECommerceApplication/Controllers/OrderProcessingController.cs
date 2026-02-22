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
    ICustomerAddressService _custAddRepo;

    IShoppingCartService _cartSrc;
    public OrderProcessingController(ILogger<OrderProcessingController> logger, IOrderProcessingService ordersrv, ICustomerService authrepo, ICustomerAddressService custAddRepo, IShoppingCartService cartService)
    {
        _logger = logger;
        _orderSrv = ordersrv;
        _AuthSrv = authrepo;
        _custAddRepo = custAddRepo;
        _cartSrc=cartService;
    }


    [HttpPost]
    public IActionResult PlaceOrder(int SelectedAddressId)
    {
        string email = HttpContext.Session.GetString("Email");
        Customer customers = _AuthSrv.getCustomerByEmail(email);

        // ShippingAddress shippingAddress = _custAddRepo.getAllCustomerAddresses(customers.CustomerId);
        bool status = _orderSrv.placeOrder(customers.CustomerId, SelectedAddressId);
        if (status)
        {
            return RedirectToAction("index", "Catelog");
        }
        else
        {
            return RedirectToAction("index", "ShoppingCart");
        }
    }

    public IActionResult OrdersDetails()
    {
        string email = HttpContext.Session.GetString("Email");
        Customer customers = _AuthSrv.getCustomerByEmail(email);

        List<Order> orders = _orderSrv.getOrderByUserId(customers.CustomerId);
        ViewData["allCustomerOrders"] = orders;
        return View();
    }

    public IActionResult OrderItemDetails(int id)
    {
        List<OrderItem> orderItems = _orderSrv.getOrderItem(id);
        ViewData["allOrdersItems"] = orderItems;
        return View();
    }

    public IActionResult CancelOrder(int orderId)
    {
        bool status = _orderSrv.cancelOrder(orderId);
        if(status)
        {
            return RedirectToAction("OrdersDetails", "OrderProcessing");
        }
        else
        {
            return RedirectToAction("OrdersDetails", "OrderProcessing");
        }
    }


    public IActionResult ConfirmOrder()
    {

        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
        {
            return RedirectToAction("Login", "Authentication");
        }
        else
        {
            string email = HttpContext.Session.GetString("Email");
            Customer customer = _AuthSrv.getCustomerByEmail(email);
            List<ShippingAddress> customerAddress = _custAddRepo.getAllCustomerAddresses(customer.CustomerId);
            ViewData["allcustomeraddress"] = customerAddress;

            List<Item> items = _cartSrc.getAllItem(customer.CustomerId);
            ViewData["allItems"] = items;
            return View();

        }

    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
