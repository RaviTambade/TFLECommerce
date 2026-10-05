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

    ICustomerRepository _AuthSrv;
    private readonly OtpService _otpService;
    private readonly EmailService _emailService;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        ICustomerRepository authsrv,
        OtpService otpService,
        EmailService emailService)
    {
        _logger = logger;
        _AuthSrv = authsrv;
        _otpService = otpService;
        _emailService = emailService;
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
            HttpContext.Session.SetString("Role", "Admin");
            return RedirectToAction("index", "Customer");
        }
        else if (customer != null && password == customer.Password)
        {
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Role", "Customer");
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

    // ---------------------------
    // Forgot password flow (OTP)
    // ---------------------------
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        HttpContext.Session.Remove("ResetPasswordEmail");
        HttpContext.Session.Remove("ResetPasswordVerified");
        return View();
    }

    [HttpPost]
    public IActionResult ForgotPassword(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            ViewBag.Error = "Please enter your email.";
            return View();
        }

        // Don't leak whether the email exists.
        var customer = _AuthSrv.getCustomerByEmail(email);
        if (customer == null || customer.CustomerId == 0)
        {
            TempData["Success"] = "If the account exists, we will send an OTP to your email.";
            return View();
        }

        HttpContext.Session.SetString("ResetPasswordEmail", email);
        HttpContext.Session.SetInt32("ResetPasswordVerified", 0);

        var otp = _otpService.GenerateOtp(email);
        _emailService.SendEmail(email, otp);

        return View("VerifyForgotPasswordOtp", email);
    }

    [HttpGet]
    public IActionResult VerifyForgotPasswordOtp()
    {
        string? email = HttpContext.Session.GetString("ResetPasswordEmail");
        if (string.IsNullOrWhiteSpace(email))
        {
            return RedirectToAction("ForgotPassword");
        }

        return View("VerifyForgotPasswordOtp", email);
    }

    [HttpPost]
    public IActionResult VerifyForgotPasswordOtp(string otp)
    {
        string? email = HttpContext.Session.GetString("ResetPasswordEmail");
        if (string.IsNullOrWhiteSpace(email))
        {
            return RedirectToAction("ForgotPassword");
        }

        if (string.IsNullOrWhiteSpace(otp))
        {
            ViewBag.Error = "Please enter the OTP.";
            return View("VerifyForgotPasswordOtp", email);
        }

        bool result = _otpService.VerifyOtp(email, otp);
        if (!result)
        {
            ViewBag.Error = "Invalid or expired OTP.";
            return View("VerifyForgotPasswordOtp", email);
        }

        HttpContext.Session.SetInt32("ResetPasswordVerified", 1);
        return View("ResetPassword", email);
    }

    [HttpPost]
    public IActionResult ResendForgotPasswordOtp()
    {
        string? email = HttpContext.Session.GetString("ResetPasswordEmail");
        if (string.IsNullOrWhiteSpace(email))
        {
            return RedirectToAction("ForgotPassword");
        }

        var otp = _otpService.GenerateOtp(email);
        _emailService.SendEmail(email, otp);

        TempData["Success"] = "OTP resent successfully.";
        return RedirectToAction("VerifyForgotPasswordOtp");
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        string? email = HttpContext.Session.GetString("ResetPasswordEmail");
        int? verified = HttpContext.Session.GetInt32("ResetPasswordVerified");

        if (string.IsNullOrWhiteSpace(email) || verified != 1)
        {
            return RedirectToAction("ForgotPassword");
        }

        return View("ResetPassword", email);
    }

    [HttpPost]
    public IActionResult ResetPassword(string newPassword, string confirmPassword)
    {
        string? email = HttpContext.Session.GetString("ResetPasswordEmail");
        int? verified = HttpContext.Session.GetInt32("ResetPasswordVerified");

        if (string.IsNullOrWhiteSpace(email) || verified != 1)
        {
            return RedirectToAction("ForgotPassword");
        }

        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
        {
            ViewBag.Error = "Password must be at least 8 characters.";
            return View("ResetPassword", email);
        }

        if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
        {
            ViewBag.Error = "Passwords do not match.";
            return View("ResetPassword", email);
        }

        var customer = _AuthSrv.getCustomerByEmail(email);
        if (customer == null || customer.CustomerId == 0)
        {
            ViewBag.Error = "Account not found. Please try again.";
            return View("ResetPassword", email);
        }

        customer.Password = newPassword;
        bool status = _AuthSrv.updateCustomer(customer);
        if (!status)
        {
            ViewBag.Error = "Password update failed. Please try again.";
            return View("ResetPassword", email);
        }

        HttpContext.Session.Remove("ResetPasswordEmail");
        HttpContext.Session.Remove("ResetPasswordVerified");

        TempData["Success"] = "Password updated successfully. Please login.";
        return RedirectToAction("Login", "Authentication");
    }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
