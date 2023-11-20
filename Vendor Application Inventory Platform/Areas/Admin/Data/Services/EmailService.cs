using MimeKit;
using MailKit.Net.Smtp;


namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    // EmailService.cs
    // EmailService.cs
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            _smtpClient = new SmtpClient();

            // Connect to the SMTP server
            _smtpClient.Connect(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), useSsl: true);

            // Authenticate with the server
            _smtpClient.Authenticate(emailSettings["UserName"], emailSettings["Password"]);
        }

        public void SendEmail(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Vendor Application Inventory Platform", "JohnWilliamCitiSoft@gmail.com"));

            // Ensure that the 'to' parameter is a valid email address
            if (MailboxAddress.TryParse(to, out var toAddress))
            {
                message.To.Add(toAddress);
            }
            else
            {
                // Handle invalid email address (throw an exception, log an error, etc.)
                throw new ArgumentException("Invalid 'to' email address", nameof(to));
            }

            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            // Send the email
            _smtpClient.Send(message);
        }

         
        public void Disconnect()
        {
            // Disconnect from the SMTP server
            _smtpClient.Disconnect(true);
            _smtpClient.Dispose();
        }
    }
}
