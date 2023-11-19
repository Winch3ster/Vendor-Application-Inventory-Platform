namespace Vendor_Application_Inventory_Platform.Models;

public class PdfDocument
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] Content { get; set; }
    public int softwareId { get; set; }
}