using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public class ReviewNotificationService : BackgroundService
    {
        private readonly AppDbContext _dbContext;
        private readonly EmailService _emailService; // Replace with your actual email service

        public ReviewNotificationService(AppDbContext dbContext, EmailService emailService)
        {
            _dbContext = dbContext;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get models that need to be reviewed
                var softwareToReview = _dbContext.Softwares
                    .Where(model => DateTime.UtcNow.Date >= model.LastReviewDate.AddDays(model.NotificationDays))
                    .ToList();

                foreach (var s in softwareToReview)
                {
                    SoftwareToBeReviewed softwareToBeReviewed = new SoftwareToBeReviewed()
                    {
                        software = s
                    };
                    _dbContext.softwareToBeRevieweds.Add(softwareToBeReviewed);
                }

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                // Sleep for a certain period before checking again
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Adjust the delay as needed
            }
        }
    }

}
