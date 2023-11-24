namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public class NotificationService
    {
        private readonly EmailService _emailService;

        public NotificationService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void NotifyUser(string userEmail, string action, string entity, string entityName)
        {
            string subject = $"Notification: {action} Action";
            string body = $"Dear User,<br/><br/> An {action} action has been performed in the system.<br/><br/>Regards,<br/>Citisoft Vendor Application Inventory Platform";
          
            _emailService.SendEmail(userEmail, subject, entity, entityName, action);
        }
    }
}
