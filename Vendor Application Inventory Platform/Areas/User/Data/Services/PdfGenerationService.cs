using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;

namespace Vendor_Application_Inventory_Platform.Areas.User.Data.Services;

public class PdfGenerationService : IPdfGenerationService
{
    public Byte[] GeneratePdf(SoftwareCompanyVM softwareCompanyVM)
    {
        var pdfContent = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginHorizontal(40);
                page.MarginTop(40);
                page.MarginBottom(20);

                page.Header()
                    .Height(60)
                    .BorderBottom(1, Unit.Point)
                    .BorderColor(Colors.BlueGrey.Lighten2)
                    .AlignCenter()
                    .AlignMiddle()
                    .Text("Report").FontSize(24);

                page.Content()
                    .AlignCenter()
                    .PaddingTop(30)
                    .Column(x =>
                    {
                        x.Item().Row(row =>
                        {
                            row.RelativeItem().Grid(grid =>
                            {
                                grid.Columns(3);
                                grid.Item().Text(softwareCompanyVM.SoftwareName).FontSize(18).Bold();
                                grid.Item().AlignRight().Text("Cloud: " + softwareCompanyVM.Cloud).Bold().FontSize(13);
                                grid.Item().AlignRight().Text("Rating: "+softwareCompanyVM.rating).Bold().FontSize(13);
                            });
                        });
                        x.Item().PaddingTop(20).Text("Software Description").Bold().FontSize(16);
                        x.Item().PaddingTop(10).Text(softwareCompanyVM.SoftwareDescription);

                        x.Item().PaddingTop(30).BorderTop(1).Row(y =>
                        {
                            y.ConstantItem(1);
                            y.RelativeItem().AlignCenter().Column(x =>
                            {
                                x.Item().PaddingVertical(10).Text("Business Area").Bold();
                                foreach (var area in softwareCompanyVM.BusinessAreas)
                                {
                                    x.Item().Text(area);
                                }
                            });
                    
                            y.RelativeItem().AlignCenter().Column(x =>
                            {
                                x.Item().PaddingVertical(10).Text("Software Module").Bold();
                                foreach (var module in softwareCompanyVM.Modules)
                                {
                                    x.Item().Text(module);
                                }
                            });
                    
                            y.RelativeItem().AlignCenter().Column(x =>
                            {
                                x.Item().PaddingVertical(10).Text("Software Type").Bold();
                                foreach(var type in softwareCompanyVM.TypeOfSoftware)
                                {
                                    x.Item().Text(type);
                                }
                            });
                    
                            y.RelativeItem().AlignCenter().Column(x =>
                            {
                                x.Item().PaddingVertical(10).Text("Financial Services Client Type").Bold();
                                foreach(var type in softwareCompanyVM.FinancialServicesClientTypes)
                                {
                                    x.Item().Text(type);
                                }
                            });
                        });

                        x.Item().PageBreak();

                        x.Item().Row(row =>
                        {
                            row.RelativeItem().Grid(grid =>
                            {
                                grid.Columns(3);
                                grid.Item().Text(softwareCompanyVM.CompanyName).FontSize(18).Bold();
                                grid.Item().AlignRight().Text("Established Date: " + softwareCompanyVM.CompanyEstablished.ToShortDateString()).Bold().FontSize(13);
                                grid.Item().AlignRight().Text("Employees: " + softwareCompanyVM.NumberOfEmployees).Bold().FontSize(13);
                            });
                        });
                        x.Item().PaddingTop(5).Text(softwareCompanyVM.WebsiteURL).Underline().FontColor(Colors.Blue.Accent1);
                        x.Item().PaddingTop(20).Text("Company Description").Bold().FontSize(16);
                        x.Item().PaddingTop(10).Text(softwareCompanyVM.CompanyDescription);

                        x.Item().PaddingTop(20).Row(row =>
                        {
                            row.RelativeItem().Grid(grid =>
                            {
                                grid.Columns(2);
                                grid.Item().Text("Last Demo Date: " + softwareCompanyVM.LastDemoDate).Bold().FontSize(13);
                                grid.Item().AlignRight().Text("Last Review Date: " + softwareCompanyVM.LastReviewDate).Bold().FontSize(13);
                            });
                        });

                        x.Item().PageBreak();

                        x.Item().PaddingTop(20).Text("Company Details").Bold().FontSize(16);

                        x.Item().PaddingTop(10).Row(row =>
                        {
                            row.RelativeItem().Padding(10).Grid(grid =>
                            {
                                grid.Columns(4);
                                grid.Item().AlignCenter().Text("Country").Bold().FontSize(14);
                                grid.Item().AlignCenter().Text("City").Bold().FontSize(14);
                                grid.Item().AlignCenter().Text("Address").Bold().FontSize(14);
                                grid.Item().AlignCenter().Text("Contacts").Bold().FontSize(14);
                            });
                        });
                        foreach (var country in softwareCompanyVM.CompanyContactData.Keys)
                        {
                            // Check if there are cities for the current country
                            if (softwareCompanyVM.CompanyContactData[country].Any())
                            {
                                foreach (var city in softwareCompanyVM.CompanyContactData[country].Keys)
                                {
                                    var address = softwareCompanyVM.CompanyContactData[country][city].GetValueOrDefault("Address", "-");
                                    var contact = softwareCompanyVM.CompanyContactData[country][city].GetValueOrDefault("Contact Number", "-");

                                    x.Item().Border(1).Row(row =>
                                    {
                                        row.RelativeItem().Padding(10).Grid(grid =>
                                        {
                                            grid.Columns(4);
                                            grid.Item().AlignCenter().Text(country);

                                            grid.Item().AlignCenter().Text(city);

                                            grid.Item().AlignCenter().Text(address);

                                            grid.Item().AlignCenter().Text(contact);
                                        });
                                    });
                                }
                            }
                            else
                            {
                                // If no cities exist for the country, display a row with hyphens
                                x.Item().Border(1).Row(row =>
                                {
                                    row.RelativeItem().Padding(10).Grid(grid =>
                                    {
                                        grid.Columns(4);
                                        grid.Item().AlignCenter().Text(country);
                                        grid.Item().AlignCenter().Text("-");
                                        grid.Item().AlignCenter().Text("-");
                                        grid.Item().AlignCenter().Text("-");
                                    });
                                });
                            }
                        }

                    });


                page.Footer()
                    .Height(30)
                    .AlignCenter()
                    .AlignMiddle()
                    .Text("CitiSoft All right reserved")
                    .FontColor(Colors.Blue.Accent3);
            });
        }).GeneratePdf();

        return pdfContent;
    }
    
    
}