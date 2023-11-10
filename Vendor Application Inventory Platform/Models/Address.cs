using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vendor_Application_Inventory_Platform.Models;

public class Address
{
    public int AddressID { get; set; }
    
    public int CityID { get; set; }

    [Required]
    [DisplayName("Address Line 1")]
    public string? AddressLine1 { get; set; }

    [DisplayName("Address Line 2")]
    public string? AddressLine2 { get; set; }

    [Required]
    [DisplayName("Postcode")]
    public string? PostCode { get; set; }
        
    public virtual City city { get; set; }

}