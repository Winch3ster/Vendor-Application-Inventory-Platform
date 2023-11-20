using Microsoft.VisualBasic;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Threading.Tasks;
using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.User.Data.Services
{



    public class ExcelGenerationService : IExcelGenerationService
    {
        static ExcelGenerationService()
        {
            // Set the license context before using EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
        }


        public async Task<byte[]> GenerateExcelAsync(SoftwareCompanyVM softwareCompanyVM)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SoftwareReport");


                //List to string is working
                //Get country working
                //City working
                //Get Contact is working
                //Get address

                //string country = GetCountryStringFromData(softwareCompanyVM.CompanyContactData); //"Location Countries";
                //string cities = GetCityStringFromData(softwareCompanyVM.CompanyContactData); //"Location Cities";
                //string c = GetContactNumberFromData(softwareCompanyVM.CompanyContactData);
                string address = GetAddressFromData(softwareCompanyVM.CompanyContactData);

                //System.Diagnostics.Debug.WriteLine(country);
                //System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine(address);
                //System.Diagnostics.Debug.WriteLine(" ");


                
                //Headers
                //Software
                worksheet.Cells["A1"].Value = "Software Name";
                worksheet.Cells["B1"].Value = "Description";
                worksheet.Cells["C1"].Value = "Type Of Software";
                worksheet.Cells["D1"].Value = "Last Demo Date";
                worksheet.Cells["E1"].Value = "Last Reviewed Date";
                worksheet.Cells["F1"].Value = "Business Areas";
                worksheet.Cells["G1"].Value = "Modules";
                worksheet.Cells["H1"].Value = "Financial Services Client Types";
                worksheet.Cells["I1"].Value = "Cloud";

                //Company
                worksheet.Cells["J1"].Value = "Company Name";
                worksheet.Cells["K1"].Value = "Company Website";
                worksheet.Cells["L1"].Value = "Company established";
                worksheet.Cells["M1"].Value = "Location Countries";
                worksheet.Cells["N1"].Value = "Location Cities";
                worksheet.Cells["O1"].Value = "Contact Telephone No.";
                worksheet.Cells["P1"].Value = "Address";
                worksheet.Cells["Q1"].Value = "Number of employees";
                worksheet.Cells["R1"].Value = "Internal Professional Services";



                // Add data
                //Software
                worksheet.Cells["A2"].Value = softwareCompanyVM.SoftwareName;
                worksheet.Cells["B2"].Value = softwareCompanyVM.SoftwareDescription;
                worksheet.Cells["C2"].Value = ListToString(softwareCompanyVM.TypeOfSoftware);//"Type Of Software";
                worksheet.Cells["D2"].Value = softwareCompanyVM.LastDemoDate;
                worksheet.Cells["E2"].Value = softwareCompanyVM.LastReviewDate;
                worksheet.Cells["F2"].Value = ListToString(softwareCompanyVM.BusinessAreas); //"Business Areas";
                worksheet.Cells["G2"].Value = ListToString(softwareCompanyVM.Modules);//"Modules";
                worksheet.Cells["H2"].Value = ListToString(softwareCompanyVM.FinancialServicesClientTypes);//"Financial Services Client Types";
                worksheet.Cells["I2"].Value = softwareCompanyVM.Cloud;


                //Company
                worksheet.Cells["J2"].Value = softwareCompanyVM.CompanyName;
                worksheet.Cells["K2"].Value = softwareCompanyVM.CompanyWebsite;
                worksheet.Cells["L2"].Value = softwareCompanyVM.CompanyEstablished;
                worksheet.Cells["M2"].Value = GetCountryStringFromData(softwareCompanyVM.CompanyContactData); //"Location Countries";
                worksheet.Cells["N2"].Value = GetCityStringFromData(softwareCompanyVM.CompanyContactData); //"Location Cities";
                worksheet.Cells["O2"].Value = GetContactNumberFromData(softwareCompanyVM.CompanyContactData); //"Contact Telephone No.";
                worksheet.Cells["P2"].Value = GetContactNumberFromData(softwareCompanyVM.CompanyContactData); //"Address";
                worksheet.Cells["Q2"].Value = softwareCompanyVM.NumberOfEmployees;
                worksheet.Cells["R2"].Value = softwareCompanyVM.InternalProfessionalServices;

                
                // Save the workbook to a stream
                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    return stream.ToArray();
                }
                
            }
        }

        private string GetAddressFromData(Dictionary<string, Dictionary<string, Dictionary<string, string>>> data)
        {
            string result = "";
            if (data == null)
            {
                return "";
            }



            foreach (var country in data)
            {
                foreach (var city in country.Value)
                {
                    foreach (var detail in city.Value)
                    {
                        if (detail.Key == "Address")
                        {
                            string phoneNumber = $"[{city.Key}] +{detail.Value}" + ", " + Environment.NewLine;
                            result += phoneNumber;
                            System.Diagnostics.Debug.WriteLine(phoneNumber);
                        }
                    }
                }
            }

            return result;

        }

        private string GetContactNumberFromData(Dictionary<string, Dictionary<string, Dictionary<string, string>>> data)
        {
            string result = "";
            if (data == null)
            {
                return "";
            }

           
            foreach (var country in data)
            {
                foreach (var city in country.Value)
                {
                    foreach (var detail in city.Value)
                    {
                        if (detail.Key == "Contact Number")
                        {
                            string phoneNumber = $"[{city.Key}] +{detail.Value}" + ", " + Environment.NewLine;
                            result += phoneNumber;
                            System.Diagnostics.Debug.WriteLine(phoneNumber);
                        }
                    }
                }
            }

            return result;
        }

        private string GetCityStringFromData(Dictionary<string, Dictionary<string, Dictionary<string, string>>> data)
        {
            string result = "";

            //To get the cities
            //Step 1: Go to the first key
                //Step 2: Go to the value of the first key (another dictionary)
                //Step 3: Now I am in the dictionary, get all the keys for this dictionary
                //Step 4: Get out of this loop
            //Step 5: Go to the next key (GO back to step 1)
            if (data == null)
            {
                return "";
            }

            // Print all values Testing purposes
            foreach (var country in data)
            {
               
                foreach (var city in country.Value)
                {
                    System.Diagnostics.Debug.WriteLine($"[{country.Key}] {city.Key}");
                    string countryCity = $"[{country.Key}] {city.Key}" + ", " + Environment.NewLine;
                    result += countryCity;
                }
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine(" ");
            }


            return result;
        }

        private string GetCountryStringFromData(Dictionary<string, Dictionary<string, Dictionary<string, string>>> data)
        {

            if (data == null)
            {
                return "";
            }

            string concatenatedCountries = string.Join(", ", data.Keys);
            return concatenatedCountries;
        }



        private string ListToString(List<string> datalist)
        {
            if (datalist == null)
            {
                return "";
            }
            string combinedString = string.Join(",", datalist.ToArray());

            return combinedString;

        }
    }





}
