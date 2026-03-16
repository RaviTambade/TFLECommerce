using Microsoft.AspNetCore.Mvc;

public class OtpController : Controller
{
    private readonly OtpService _otpService;
    private readonly EmailService _emailService;

    public OtpController(OtpService otpService, EmailService emailService)
    {
        _otpService = otpService;
        _emailService = emailService;
    }

    [HttpPost]
    public IActionResult SendOtp(int SelectedAddressId)
    {
        HttpContext.Session.SetInt32("SelectedAddressId", SelectedAddressId);

        string email = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("SendOtp"); // email not in session
        }

        var otp = _otpService.GenerateOtp(email);
        _emailService.SendEmail(email, otp);

        ViewBag.Message = "OTP sent successfully";

        return RedirectToAction("VerifyOtp");
    }

    // Show Verify OTP Page
    public IActionResult VerifyOtp()
    {
        return View();
    }

    // Verify OTP
    [HttpPost]
    public IActionResult VerifyOtp(string otp)
    {
        string? email = HttpContext.Session.GetString("Email");
        int? addressId = HttpContext.Session.GetInt32("SelectedAddressId");

        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("SendOtp");
        }

        var result = _otpService.VerifyOtp(email, otp);

        if (!result)
        {
            ViewBag.Error = "Invalid or Expired OTP";
            return View();
        }

        ViewBag.Message = "OTP Verified Successfully";

        return RedirectToAction("PlaceOrder","OrderProcessing",
          new { SelectedAddressId = addressId });
    }
}