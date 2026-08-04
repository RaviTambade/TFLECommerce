using ECommerceApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost]
        public async Task<IActionResult> Apply(string code, decimal totalAmount)
        {
            var result = await _couponService.applyCouponAsync(code, totalAmount);

            if (result.Success && !string.IsNullOrWhiteSpace(result.Code))
            {
                HttpContext.Session.SetString("AppliedCouponCode", result.Code);
                HttpContext.Session.SetString("AppliedCouponDiscount", result.Discount.ToString());
                HttpContext.Session.SetString("AppliedFinalAmount", result.FinalAmount.ToString());
            }
            else
            {
                HttpContext.Session.Remove("AppliedCouponCode");
                HttpContext.Session.Remove("AppliedCouponDiscount");
                HttpContext.Session.Remove("AppliedFinalAmount");
            }

            return Json(new
            {
                success = result.Success,
                discount = result.Discount,
                finalAmount = result.FinalAmount,
                message = result.Message
            });
        }
    }
}
