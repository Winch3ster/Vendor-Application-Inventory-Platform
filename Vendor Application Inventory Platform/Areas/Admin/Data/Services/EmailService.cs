using MimeKit;
using MailKit.Net.Smtp;


namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    /// <summary>
    /// For testing purposes
    /// 
    /// To use this functionality use this URL once signed in your_localhost/Employee/Index
    /// </summary>


    //The user will receive email of notification for any changes made to the system's data like create, update and delete
    public class EmailService
    {
        private readonly SmtpClient _smtpClient; //Get the client services


        public EmailService(IConfiguration configuration)
        {
            //Get the email services from the appsetting.json
            var emailSettings = configuration.GetSection("EmailSettings");

            _smtpClient = new SmtpClient(); //Intantiate a new client object

            // Connect to the SMTP server, gmail smtp server is used (as specified in appsetting.json)
            _smtpClient.Connect(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), useSsl: true);

            // Authenticate the user with the specified server
            _smtpClient.Authenticate(emailSettings["UserName"], emailSettings["Password"]);
        }
        

        //Send mail to user
        public void SendEmail(string toEmail,  string subject, string entity, string entityName, string actionPerformed)
        {
            //[entity] (entity name) was (action performed)
            string messageToSend = $"[{entity}] {entityName} was {actionPerformed}";

            var mail = new MimeMessage(); //create a new empty mail 

            //Add addresses for the mail
            mail.From.Add(new MailboxAddress("Vendor Application Inventory Platform", "JohnWilliamCitiSoft@gmail.com"));

            // Ensure that the 'to' parameter is a valid email address
            if (MailboxAddress.TryParse(toEmail, out var toAddress))
            {
                mail.To.Add(toAddress); //add the toEmail to the email to be sent to
            }
            else
            {
                // Handle invalid email address (throw an exception by loggin the error)
                throw new ArgumentException("Invalid 'to' email address", nameof(toEmail));
            }

            mail.Subject = subject; //Set the subject of the mail to the parameter 

            //Create the email body and render the message to the body
            var emailBody = new BodyBuilder { HtmlBody = messageToSend };


            mail.Body = emailBody.ToMessageBody();

            // Send the email
            _smtpClient.Send(mail);
        }

         
        public void Disconnect()
        {
            // Disconnect from the SMTP server and free up resource by deleting the smtpClient object
            _smtpClient.Disconnect(true);
            _smtpClient.Dispose();
        }
    }
}
