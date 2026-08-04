using Microsoft.AspNetCore.Mvc;
using ECommerceApplication.Models;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ICustomerService _customerService;

        public ReviewController(IReviewService reviewService, ICustomerService customerService)
        {
            _reviewService = reviewService;
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult AddReview(int productId, int rating, string reviewText, int? orderId)
        {
            string? email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Login", "Authentication");
            }

            if (rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(reviewText))
            {
                TempData["ReviewError"] = "Please provide a valid rating (1-5) and review text.";
                if (orderId.HasValue)
                {
                    return RedirectToAction("OrderItemDetails", "OrderProcessing", new { id = orderId.Value });
                }
                return RedirectToAction("DetailsWithId", "Catelog", new { id = productId });
            }

            Customer user = _customerService.getCustomerByEmail(email);

            bool status = _reviewService.AddReview(productId, user.CustomerId, rating, reviewText.Trim());
            TempData["ReviewMessage"] = status ? "Review added successfully." : "Failed to add review.";

            if (orderId.HasValue)
            {
                return RedirectToAction("OrderItemDetails", "OrderProcessing", new { id = orderId.Value });
            }

            return RedirectToAction("OrdersDetails", "OrderProcessing");
        }

        [HttpGet]
        public IActionResult GetReviews(int productId)
        {
            var reviews = _reviewService.GetReviews(productId);
            return Json(reviews);
        }
    }
}
