using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly ICustomerService _customerService;

        public WishlistController(IWishlistService wishlistService, ICustomerService customerService)
        {
            _wishlistService = wishlistService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            string? email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Login", "Authentication");
            }

            Customer user = _customerService.getCustomerByEmail(email);
            var items = _wishlistService.GetWishlist(user.CustomerId);
            ViewData["wishlistItems"] = items;
            return View();
        }

        public IActionResult Add(int productId)
        {
            string? email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Login", "Authentication");
            }

            Customer user = _customerService.getCustomerByEmail(email);
            _wishlistService.AddToWishlist(user.CustomerId, productId);
            return RedirectToAction("Index", "Catelog");
        }

        public IActionResult Remove(int productId)
        {
            string? email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Login", "Authentication");
            }

            Customer user = _customerService.getCustomerByEmail(email);
            _wishlistService.RemoveFromWishlist(user.CustomerId, productId);
            return RedirectToAction("Index");
        }
    }
}
