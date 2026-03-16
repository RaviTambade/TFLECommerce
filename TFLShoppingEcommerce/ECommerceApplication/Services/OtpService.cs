public class OtpService
{
    private static Dictionary<string, OtpEntry> otpStore = new();

    public string GenerateOtp(string email)
    {
        var otp = new Random().Next(100000, 999999).ToString();

        otpStore[email] = new OtpEntry
        {
            Otp = otp,
            ExpiryTime = DateTime.UtcNow.AddMinutes(5) // OTP valid for 5 minutes
        };

        return otp;
    }

    public bool VerifyOtp(string email, string otp)
    {
        if (!otpStore.ContainsKey(email))
            return false;

        var entry = otpStore[email];

        // Check expiry
        if (DateTime.UtcNow > entry.ExpiryTime)
        {
            otpStore.Remove(email);
            return false;
        }

        if (entry.Otp == otp)
        {
            otpStore.Remove(email);
            return true;
        }

        return false;
    }
}