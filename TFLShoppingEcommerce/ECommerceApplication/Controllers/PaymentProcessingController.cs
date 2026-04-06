using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using System.Collections.Generic;
using System.Globalization;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Controllers
{
    public class PaymentProcessingController : Controller
    {
        private readonly ILogger<PaymentProcessingController> _logger;
        private readonly IConfiguration _config;

        private readonly ICustomerService _AuthSrv;
        private readonly IPaymentProcessingService _PaymentSrv;
        private readonly IShoppingCartService _Cartsrv;
        ICustomerAddressService _custAddRepo;
        IOrderProcessingService _orderSrv;

        public PaymentProcessingController(
            ILogger<PaymentProcessingController> logger,
            IPaymentProcessingService paymentsrv,
            IConfiguration config,
            ICustomerService authSrv,
            IShoppingCartService cartsrv, ICustomerAddressService custAddRepo, IOrderProcessingService orderSrv)
        {
            _config = config;
            _logger = logger;
            _PaymentSrv = paymentsrv;
            _AuthSrv = authSrv;
            _Cartsrv = cartsrv;
            _custAddRepo = custAddRepo;
            _orderSrv = orderSrv;
        }

        // ✅ STEP 1: Create Razorpay Order
        public IActionResult CreateOrder()
        {
            string email = HttpContext.Session.GetString("Email");
            var customer = _AuthSrv.getCustomerByEmail(email);

            int orderId = HttpContext.Session.GetInt32("OrderId").Value;

            double amount = (double)_orderSrv.GetOrderTotal(orderId);

            HttpContext.Session.SetString("Amount", amount.ToString());

            // 👇 ADD THESE
            ViewData["allcustomeraddress"] = _custAddRepo.getAllCustomerAddresses(customer.CustomerId);
            ViewData["allItems"] = _Cartsrv.getAllItem(customer.CustomerId);

            string key = _config["Razorpay:Key"];
            string secret = _config["Razorpay:Secret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            var options = new Dictionary<string, object>()
    {
        { "amount", amount * 100 },
        { "currency", "INR" },
        { "receipt", "order_" + Guid.NewGuid().ToString("N").Substring(0, 20)}
    };

            Order order = client.Order.Create(options);

            ViewBag.OrderId = order["id"].ToString();
            ViewBag.Key = key;
            ViewBag.Amount = amount;

            return View("Checkout");
        }
        // ✅ STEP 2: Verify Payment
        [HttpPost]
        public IActionResult Verify([FromBody] RazorpayVerifyRequest? razorpayResponse)
        {
            var amountText = HttpContext.Session.GetString("Amount");
            var orderIdValue = HttpContext.Session.GetInt32("OrderId");

            if (string.IsNullOrWhiteSpace(amountText) || !orderIdValue.HasValue)
            {
                return Json(new { redirectUrl = Url.Action("Index", "ShoppingCart") });
            }

            try
            {
                decimal amount = decimal.Parse(amountText, CultureInfo.InvariantCulture);
                int orderId = orderIdValue.Value;
                string statusText = string.IsNullOrWhiteSpace(razorpayResponse?.razorpay_payment_id)
                    ? "Failed"
                    : "Completed";

                bool status = _PaymentSrv.SavePayment(
                    orderId: orderId,
                    paymentDate: DateTime.Now,
                    paymentAmount: (int)amount,
                    paymentMethod: "PayPal",
                    paymentStatus: statusText
                );

                if (status)
                {
                    return Json(new { redirectUrl = Url.Action("Index", "Catelog") });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment save failed for orderId={OrderId}", orderIdValue.Value);
            }

            return Json(new { redirectUrl = Url.Action("Index", "ShoppingCart") });
        }

        [HttpPost]
        public IActionResult SaveCodPayment()
        {
            var amountText = HttpContext.Session.GetString("Amount");
            var orderIdValue = HttpContext.Session.GetInt32("OrderId");

            if (string.IsNullOrWhiteSpace(amountText) || !orderIdValue.HasValue)
            {
                return Json(new { redirectUrl = Url.Action("Index", "ShoppingCart") });
            }

            try
            {
                decimal amount = decimal.Parse(amountText, CultureInfo.InvariantCulture);
                bool status = _PaymentSrv.SavePayment(
                    orderId: orderIdValue.Value,
                    paymentDate: DateTime.Now,
                    paymentAmount: (int)amount,
                    paymentMethod: "Bank Transfer",
                    paymentStatus: "Pending"
                );

                if (status)
                {
                    return Json(new { redirectUrl = Url.Action("Index", "Catelog") });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "COD payment save failed for orderId={OrderId}", orderIdValue.Value);
            }

            return Json(new { redirectUrl = Url.Action("Index", "ShoppingCart") });
        }
    }

    public class RazorpayVerifyRequest
    {
        public string? razorpay_order_id { get; set; }
        public string? razorpay_payment_id { get; set; }
        public string? razorpay_signature { get; set; }
    }
}