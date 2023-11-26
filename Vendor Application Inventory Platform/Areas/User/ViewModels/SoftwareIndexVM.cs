using System.Security.Claims;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.User.ViewModels
{
    public class SoftwareIndexVM
    {
        public List<Software> returnedSoftwares { get; set; }
        public List<Software> recentlyViewed { get; set; }
        public IEnumerable<Claim> userClaims { get; set; }
    }
}
