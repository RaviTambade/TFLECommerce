using MailKit.Net.Smtp;
using MimeKit;
using ECommerceApplication.Models;
using System.Text;

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

    public void SendOrderConfirmationEmail(
        string toEmail,
        string customerName,
        int orderId,
        string paymentMethod,
        string paymentStatus,
        List<OrderItem> items,
        decimal totalAmount,
        decimal discountAmount,
        decimal finalAmount)
    {
        var email = new MimeMessage();

        string emailby = _config["EmailService:email"];
        string password = _config["EmailService:password"];
        string host = _config["EmailService:host"];
        int port = int.Parse(_config["EmailService:port"]);

        email.From.Add(MailboxAddress.Parse(emailby));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = $"Order Confirmation - #{orderId}";

        var itemRows = new StringBuilder();
        foreach (var item in items)
        {
            decimal unitPrice = Convert.ToDecimal(item.product.UnitPrice);
            decimal subTotal = unitPrice * item.Quantity;
            itemRows.Append($@"
                <tr>
                    <td style='padding:8px;border:1px solid #ddd;'>{item.product.ProductTitle}</td>
                    <td style='padding:8px;border:1px solid #ddd;text-align:center;'>{item.Quantity}</td>
                    <td style='padding:8px;border:1px solid #ddd;text-align:right;'>₹{unitPrice:0.00}</td>
                    <td style='padding:8px;border:1px solid #ddd;text-align:right;'>₹{subTotal:0.00}</td>
                </tr>");
        }

        string html = $@"
            <div style='font-family:Arial,sans-serif;max-width:700px;margin:auto;'>
                <h2 style='color:#0f172a;'>Order Confirmed</h2>
                <p>Hi {customerName},</p>
                <p>Your order has been placed successfully. Here is your invoice summary:</p>
                <p><strong>Order ID:</strong> #{orderId}<br/>
                   <strong>Payment Method:</strong> {paymentMethod}<br/>
                   <strong>Payment Status:</strong> {paymentStatus}</p>

                <table style='width:100%;border-collapse:collapse;margin-top:12px;'>
                    <thead>
                        <tr style='background:#f1f5f9;'>
                            <th style='padding:8px;border:1px solid #ddd;text-align:left;'>Product</th>
                            <th style='padding:8px;border:1px solid #ddd;'>Qty</th>
                            <th style='padding:8px;border:1px solid #ddd;text-align:right;'>Price</th>
                            <th style='padding:8px;border:1px solid #ddd;text-align:right;'>Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>
                        {itemRows}
                    </tbody>
                </table>

                <div style='margin-top:14px;text-align:right;'>
                    <p style='margin:4px 0;'><strong>Total:</strong> ₹{totalAmount:0.00}</p>
                    <p style='margin:4px 0;'><strong>Discount:</strong> ₹{discountAmount:0.00}</p>
                    <p style='margin:4px 0;font-size:18px;'><strong>Final Amount:</strong> ₹{finalAmount:0.00}</p>
                </div>

                <p style='margin-top:20px;color:#475569;'>Thank you for shopping with us.</p>
            </div>";

        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };

        using var smtp = new SmtpClient();
        smtp.Connect(host, port, false);
        smtp.Authenticate(emailby, password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}