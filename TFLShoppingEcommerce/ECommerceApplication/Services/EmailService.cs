using MailKit.Net.Smtp;
using MimeKit;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendEmail(string toEmail, string otp)
    {
        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse(_config["EmailSettings:Email"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = "OTP Verification";

        email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = $"Your OTP is: {otp}"
        };

        using var smtp = new SmtpClient();
        smtp.Connect(
            _config["EmailSettings:Host"],
            int.Parse(_config["EmailSettings:Port"]),
            false
        );

        smtp.Authenticate(
            _config["EmailSettings:Email"],
            _config["EmailSettings:Password"]
        );

        smtp.Send(email);
        smtp.Disconnect(true);
    }
}