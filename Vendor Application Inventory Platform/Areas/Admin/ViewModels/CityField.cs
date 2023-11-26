using System.ComponentModel.DataAnnotations;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;

public class CityField
{
    public bool HasAddress { get; set; }
    public bool HasContact { get; set; }
    public string Country { get; set; }
    public string CityName { get; set; }
    
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Address Line 1 must be at least 10 characters up to 100 characters.")]
    public string? Address1 { get; set; }
    
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Address Line 2 must be at least 10 characters up to 100 characters.")]
    public string? Address2 { get; set; }
    
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Post Code must be at least 4 characters up to 100 characters.")]
    public string? Postcode { get; set; }
    
    public string? State { get; set; }
    
    [StringLength(15, MinimumLength = 9, ErrorMessage = "Contact number must be a number with a length between 9 and 15 characters.")]
    public string? ContactNumber { get; set; }
}