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

        string emailby = _config["EmailService:email"];
        string password = _config["EmailService:password"];
        string host = _config["EmailService:host"];
        int port = int.Parse(_config["EmailService:port"]);

        email.From.Add(MailboxAddress.Parse(emailby));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = "OTP Verification";

        email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = $"Your OTP is: {otp}"
        };

        using var smtp = new SmtpClient();
        smtp.Connect(
            host,
           port,
            false
        );

        smtp.Authenticate(
            emailby,
            password
        );

        smtp.Send(email);
        smtp.Disconnect(true);
    }
}