﻿using System.Data;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using OfficeOpenXml;
using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Data_Access_Layer
{
    public class DatabaseSeeder
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Reference to appdbcontext file. This file is used to send and get data from the database

                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                //context is like an instance of the database
                context.Database.EnsureCreated();

                //var hashedPassword = BCrypt.Net.BCrypt.HashPassword("abc123");

                //Check if employee table is empty

                //if the table is empty, add employee data
                 /*
                 context.Employees.AddRange(new List<Employee>()
                 {
                     new Employee()
                     {
                         FirstName = "John",
                         LastName = "William",
                         Email = "John@example.com",
                         Password = hashedPassword,
                         companyAccess = true,
                         accountAccess = true,
                         softwareAccess = true,
                         IsAdmin = true,
                     },
                     new Employee()
                     {
                         FirstName = "Henrich",
                         LastName = "Eddinburg",
                         Email = "Henrich@example.com",
                         Password = hashedPassword,
                         companyAccess = true,
                         accountAccess = true,
                         softwareAccess = true,
                         IsAdmin = false,
                     }
                 });
                 context.SaveChanges();

                */
                /*
          
                 List<BusinessArea> businessAreaList = new List<BusinessArea>()
                 {
                     new BusinessArea() {
                         Description = "Data Aggregation"
                     },
                     new BusinessArea() {
                         Description = "Analytic"
                     },
                     new BusinessArea() {
                         Description = "Portfolio Reporting"
                     },
                 };

                 //Database seeding
                 context.BusinessAreas.AddRange(businessAreaList);
                 context.SaveChanges();
                 
                 

              
                 SoftwareModule softwareModule = new SoftwareModule()
                 {
                     Module = "Addepar"
                 };

                 context.SoftwareModules.Add(softwareModule);
                 context.SaveChanges();
                 

                 
             
                 SoftwareType softwareType = new SoftwareType() { Type = "Wealth Management" };

                 context.SoftwareTypes.Add(softwareType);

                 context.SaveChanges();
                 
                
                
                 List<FinancialServicesClientType> financialServicesClientTypes = new List<FinancialServicesClientType>()
                 {
                     new FinancialServicesClientType() {
                         Description = "Wealth Management"
                     },
                     new FinancialServicesClientType() {
                         Description = "Family Offices"
                     },
                     new FinancialServicesClientType() {
                         Description = "Broker Dealers"
                     },
                 };

                context.FinancialServicesClientTypes.AddRange(financialServicesClientTypes);



                context.SaveChanges();



                List<Country> countryList = new List<Country>()
                 {
                     new Country()
                     {
                         CountryName = "United States of America".ToUpper()
                     },
                     new Country()
                     {
                         CountryName = "United Kingdom".ToUpper()
                     },
                     new Country()
                     {
                         CountryName = "Ireland".ToUpper()
                     },
                     new Country()
                     {
                         CountryName = "India".ToUpper()
                     },
                 };
                 context.Countries.AddRange(countryList);
                 context.SaveChanges();

               




                 List<City> cityList = new List<City>()
                 {
                     new City()
                     {
                         CityName = "Mountain View".ToUpper(),
                         country = countryList[0],
                         CountryID = countryList[0].CountryID,

                     },
                     new City()
                     {
                         CityName = "New York City".ToUpper(),
                         country = countryList[0],
                         CountryID = countryList[0].CountryID,

                     },
                     new City()
                     {
                         CityName = "Chicago".ToUpper(),
                         country = countryList[0],
                         CountryID = countryList[0].CountryID,

                     },
                     new City()
                     {
                         CityName = "Salt Lake City".ToUpper(),
                         country = countryList[0],
                         CountryID = countryList[0].CountryID,

                     },
                     new City()
                     {
                         CityName = "Dublin".ToUpper(),
                         country = countryList[2],
                         CountryID = countryList[2].CountryID,

                     },
                     new City()
                     {
                         CityName = "Edinburgh".ToUpper(),
                         country = countryList[1],
                         CountryID = countryList[1].CountryID,

                     },
                     new City()
                     {
                         CityName = "London".ToUpper(),
                         country = countryList[1],
                         CountryID = countryList[1].CountryID,

                     },
                     new City()
                     {
                         CityName = "Pune".ToUpper(),
                         country = countryList[3],
                         CountryID = countryList[3].CountryID,

                     }
                 };
                 context.Cities.AddRange(cityList);
                 context.SaveChanges();



                 //Contact number
                 ContactNumber contactNumber = new ContactNumber()
                 {
                     Number = 18554646268,
                     city = cityList[0],
                     CityID = cityList[0].CityID,
                 };
                 context.ContactNumbers.Add(contactNumber);
                 context.SaveChanges();

                 
                 //Address 
                 List<Address> addresses = new List<Address>()
                 {
                     new Address()
                     {
                         city = cityList[0],
                         CityID = cityList[0].CityID,
                         AddressLine1 = "787 Madison Ave",
                         AddressLine2 = "Castro St. Mountain View",
                         State = "CA",
                         PostCode = "94041"
                     },
                     new Address()
                     {
                         city = cityList[1],
                         CityID = cityList[1].CityID,
                         AddressLine1 = "335 Madison Ave",
                         AddressLine2 = "25th floor, New York",
                         State = "NY",
                         PostCode = "10017"
                     },
                     new Address()
                     {
                         city = cityList[2],
                         CityID = cityList[2].CityID,
                         AddressLine1 = "167 N Green Street",
                         AddressLine2 = "25th floor, New York",
                         State = "IL",
                         PostCode = "60607"
                     },
                     new Address()
                     {
                         city = cityList[3],
                         CityID = cityList[3].CityID,
                         AddressLine1 = "460 West 50 North",
                         AddressLine2 = "Suite 125 Salt Lake City",
                         State = "UT",
                         PostCode = "84101"
                     },
                     new Address()
                     {
                         city = cityList[4],
                         CityID = cityList[4].CityID,
                         AddressLine1 = "10 Ely Place",
                         AddressLine2 = "Dublin 2 Salt Lake City",
                         State = "Random",
                         PostCode = "D02 HR98"
                     },
                     new Address()
                     {
                         city = cityList[5],
                         CityID = cityList[5].CityID,
                         AddressLine1 = "80 George Street Office",
                         AddressLine2 = "01-101 Viman Nagar",
                         State = "Edinburgh",
                         PostCode = "EH2 3BU"
                     },
                     new Address()
                     {
                         city = cityList[6],
                         CityID = cityList[6].CityID,
                         AddressLine1 = "23 Bedford Square (1st Floor)",
                         AddressLine2 = "Castro St. Mountain View",
                         State = "London",
                         PostCode = "WC1B 3HH"
                     },
                     new Address()
                     {
                         city = cityList[7],
                         CityID = cityList[7].CityID,
                         AddressLine1 = "10th Floor, Fountainhead Tower 1",
                         AddressLine2 = "Viman Nagar",
                         State = "Pune",
                         PostCode = "411014"
                     }
                 };

                 context.Addresses.AddRange(addresses);
                 context.SaveChanges();





                 Company company = new Company()
                 {
                     CompanyName = "ADDEPAR, INC.",
                     WebsiteURL = "https:addepar.com",
                     Description = "",
                     EstablishedDate = new DateTime(2009),
                     NumberOfEmployee = 1000,
                     InternalProfessionalServices = false,
                     LastDemoDate = new DateTime(2023, 1, 18),
                     LastReviewDate = new DateTime(2023, 1, 18)

                 };
                 context.Companies.Add(company);
                 context.SaveChanges();


                 Software software = new Software()
                 {
                     CompanyID = 1,
                     Company = company,
                     SoftwareName = "ADDEPAR",
                     Description = "An American wealth management platform for registered investment advisors, specializing in data aggregation, analytics, and portfolio reporting. Addepar software facilitates both visualizing an investment portfolio's exposures at the individual asset class and also tabulating the portfolio's total value according to real time value of the assets under management.\r\nIn September 2016, Salesforce.com announced their partnership with Addepar for Salesforce's Wave Financial Services Cloud for financial advisers, making it easier to see across asset classes and produce a single visual for their clients.",
                     Cloud = CloudType.Based,
                     DocumentAttached = false
                 };

                 context.Softwares.Add(software);
                 context.SaveChanges();



                 //Seeding the join table (establish relationship)

                 //For each area inside the list create an instance in the join table
                 foreach (var area in businessAreaList)
                 {
                     Software_Area s_a = new Software_Area()
                     {
                         areaID = area.BusinessAreaID,
                         businessArea = area,
                         software = software,
                         softwareID = software.SoftwareID
                     };

                     //Add to table
                     context.Software_Areas.Add(s_a);
                 }
                 context.SaveChanges();

                 //Software Modules join table
                 context.Software_Modules.Add(
                     new Software_Module()
                     {
                         moduleID = softwareModule.SoftwareModuleID,
                         softwareModule = softwareModule,
                         software = software,
                         softwareID = software.SoftwareID
                     });
                 context.SaveChanges();


                 //Software Type join table
                 context.Software_Types.Add(
                     new Software_Type()
                     {
                         softwareType = softwareType,
                         typeID = softwareType.SoftwareTypeID,
                         software = software,
                         softwareID = software.SoftwareID
                     });
                 context.SaveChanges();



                 //Software financial type join table
                 foreach (var ft in financialServicesClientTypes)
                 {
                     Software_FinancialServicesClientType s_f = new Software_FinancialServicesClientType()
                     {
                         financialServicesClientType = ft,
                         financialServicesClientTypeID = ft.FinancialServicesClientTypeID,
                         software = software,
                         softwareID = software.SoftwareID
                     };
                     context.Software_FinancialServicesClientTypes.Add(s_f); 
                 }
                 context.SaveChanges();


                 //company and cities
                 foreach(var country in countryList)
                 {
                     Company_Country c_c = new Company_Country()
                     {
                         country = country,
                         countryID = country.CountryID,
                         company = company,
                         companyID = company.CompanyID,
                     };
                     context.Add(c_c);
                 }
                 context.SaveChanges();

                */


                //Rating seeding
                //Delete review first
                //Based on how many review given
                //update the review





                //Reviews seeding
                /*
                //Step 1: Get the user from database (This is similar to user logging into the system)
                Employee userLoggedIn = context.Employees.FirstOrDefault(e => e.EmployeeID == 1); //Assuming the first user drop the comment 



                //Step 2: Get the software from database (This is similar to user viewing the product/software)
                Software softwareInView = context.Softwares.FirstOrDefault(s => s.SoftwareID == 1);

                //Step 3: Create the review  and link to employee and software
                List<Review> reviews = new List<Review>() {

                     new Review() {
                         employee = userLoggedIn,
                         employeeFirstName = userLoggedIn.FirstName,    
                         employeeLastName = userLoggedIn.LastName,  
                         EmployeeID = userLoggedIn.EmployeeID,
                         software = softwareInView,
                         SoftwareID = softwareInView.SoftwareID,
                         givenStar = 5,
                         Description = "A great Software. Helped my client solve their problems",
                         ReviewDate = DateTime.Now
                     },
                     new Review() {
                         employee = userLoggedIn,
                         employeeFirstName = userLoggedIn.FirstName,
                         employeeLastName = userLoggedIn.LastName,
                         EmployeeID = userLoggedIn.EmployeeID,
                         software = softwareInView,
                         SoftwareID = softwareInView.SoftwareID,
                         givenStar =3,
                         Description = "A new comment",
                         ReviewDate = DateTime.Now
                     }

                 };


                //Step 5: Save it in databse
                context.Reviews.AddRange(reviews);
                context.SaveChanges();

                
                //update the software rating
                var software = context.Softwares.FirstOrDefault(s => s.SoftwareID == 1); //rating the first software

                //Extract all review that has softwareId==1 from the reviews table

                var softwareRating = context.Reviews.Where(r => r.SoftwareID == 1).ToList();
                //Calculate new rating sum of givenStar / number of returned entries
                float newrating = 0;

                foreach(var givenrating in softwareRating)
                {
                    newrating += givenrating.givenStar;

                    System.Diagnostics.Debug.WriteLine($"The given rating: {givenrating.givenStar}");
                    System.Diagnostics.Debug.WriteLine($"The rating: {newrating}");
                }

                newrating = (newrating / softwareRating.Count);

                System.Diagnostics.Debug.WriteLine($"The new rating: {newrating}");
                System.Diagnostics.Debug.WriteLine($"The Number of rating: {softwareRating.Count}");

                //update rating in database
                software.rating = newrating;
                context.Update(software);
                context.SaveChanges();

                */

                //Create the link for user history table


                /*

                //New software
          
                 List<BusinessArea> businessAreaList = new List<BusinessArea>()
                 {
                     new BusinessArea() {
                         Description = "Enterprise Architecture"
                     },
               
                 };

                 //Database seeding
                 context.BusinessAreas.AddRange(businessAreaList);
                 context.SaveChanges();
                 
                 

              
                 SoftwareModule softwareModule = new SoftwareModule()
                 {
                     Module = "Enterprise Architecture Suite"
                 };

                 context.SoftwareModules.Add(softwareModule);
                 context.SaveChanges();
                 

                 
             
                 SoftwareType softwareType = new SoftwareType() { Type = "Enterprise Architecture" };

                 context.SoftwareTypes.Add(softwareType);

                 context.SaveChanges();
                 
                
                
              



                List<Country> countryList = new List<Country>()
                 {
                     new Country()
                     {
                         CountryName = "Austria".ToUpper()
                     },
                     new Country()
                     {
                         CountryName = "France".ToUpper()
                     },
                     new Country()
                     {
                         CountryName = "Germany".ToUpper()
                     },
                     new Country()
                     {
                         CountryName = "Greece".ToUpper()
                     },
                      new Country()
                     {
                         CountryName = context.Countries.FirstOrDefault(c => c.CountryID == 3).CountryName
                     },
                       new Country()
                     {
                         CountryName = "Poland".ToUpper()
                     },
                        new Country()
                     {
                         CountryName = "Spain".ToUpper()
                     },
                 };
                 context.Countries.AddRange(countryList);
                 context.SaveChanges();

               




                 List<City> cityList = new List<City>()
                 {
                     new City()
                     {
                         CityName = "Wein".ToUpper(),
                         country = countryList[0],
                         CountryID = countryList[0].CountryID,

                     },
                     new City()
                     {
                         CityName = "Paris".ToUpper(),
                         country = countryList[1],
                         CountryID = countryList[1].CountryID,

                     },
                     new City()
                     {
                         CityName = "Berlin".ToUpper(),
                         country = countryList[2],
                         CountryID = countryList[2].CountryID,

                     },
                     new City()
                     {
                         CityName = "Chalikida".ToUpper(),
                         country = countryList[3],
                         CountryID = countryList[3].CountryID,

                     },
                     new City()
                     {
                         CityName = "Dublin".ToUpper(),
                         country = countryList[4],
                         CountryID = countryList[4].CountryID,

                     },
                     new City()
                     {
                         CityName = "Warzsawa".ToUpper(),
                         country = countryList[5],
                         CountryID = countryList[5].CountryID,

                     },
                     new City()
                     {
                         CityName = "Madrid".ToUpper(),
                         country = countryList[6],
                         CountryID = countryList[6].CountryID,

                     },
                
                 };
                 context.Cities.AddRange(cityList);
                 context.SaveChanges();




                 //Contact number
                 List<ContactNumber> contactNumberList = new List<ContactNumber>() {

                     new ContactNumber()
                     {
                         Number = 43190510810,
                         city = cityList[0],
                         CityID = cityList[0].CityID,
                     },
                     new ContactNumber()
                     {
                         Number = 33153245383,
                         city = cityList[1],
                         CityID = cityList[1].CityID,
                     },
                     new ContactNumber()
                     {
                         Number = 493022692510,
                         city = cityList[2],
                         CityID = cityList[2].CityID,
                     },
                     new ContactNumber()
                     {
                         Number = 302221308676,
                         city = cityList[3],
                         CityID = cityList[3].CityID,
                     },
                     new ContactNumber()
                     {
                         Number = 35312870129,
                         city = cityList[4],
                         CityID = cityList[4].CityID,
                     },
                     new ContactNumber()
                     {
                         Number = 48226280015,
                         city = cityList[5],
                         CityID = cityList[5].CityID,
                     },
                     new ContactNumber()
                     {
                         Number =  34917811880,
                         city = cityList[6],
                         CityID = cityList[6].CityID,
                     }

                    


                 };

                 context.ContactNumbers.AddRange(contactNumberList);
                 context.SaveChanges();

                 
                 //Address 
                 List<Address> addresses = new List<Address>()
                 {
                     new Address()
                     {
                        
                         city = cityList[0],
                         CityID = cityList[0].CityID,
                         AddressLine1 = "Operngasse",
                         AddressLine2 = "20b",
                         State = " ",
                         PostCode = "1040"
                     },
                     new Address()
                     {
                        
                         city = cityList[1],
                         CityID = cityList[1].CityID,
                         AddressLine1 = "5",
                         AddressLine2 = "Rue du Helder",
                         State = "Paris",
                         PostCode = "75009"
                     },
                     new Address()
                     {
                         //Berlin Naglerstrabe 5 10245 Berlin
                         city = cityList[2],
                         CityID = cityList[2].CityID,
                         AddressLine1 = "Naglerstrabe",
                         AddressLine2 = "5",
                         State = "Berlin",
                         PostCode = "10245"
                     },
                     new Address()
                     {
                         //Greece
                        
                         city = cityList[3],
                         CityID = cityList[3].CityID,
                         AddressLine1 = "4,",
                         AddressLine2 = "Dimarxou Sarafianou Street",
                         State = "Evia",
                         PostCode = "34100"
                     },
                     new Address()
                     {
                         
                         //ireland
                         city = cityList[4],
                         CityID = cityList[4].CityID,
                         AddressLine1 = "21",
                         AddressLine2 = "Priory Office Park",
                         State = "Stillorgan",
                         PostCode = "Bunlin A94 F660"
                     },
                     new Address()
                     {
                         
                         //Poland
                         city = cityList[5],
                         CityID = cityList[5].CityID,
                         AddressLine1 = "1. Panska 96 lok",
                         AddressLine2 = " ",
                         State = "Warszawa",
                         PostCode = "59 00-837"
                     },
                     new Address()
                     {
                         //spain
                         city = cityList[6],
                         CityID = cityList[6].CityID,
                         AddressLine1 = "C. de Velázquez",
                         AddressLine2 = "S.L.U. Velazquez",
                         State = "28006",
                         PostCode = "WC1B 3HH"
                     },
                   
                 };

                 context.Addresses.AddRange(addresses);
                 context.SaveChanges();





                 Company company = new Company()
                 {
                     CompanyName = "BOC Group",
                     WebsiteURL = "https://www.boc-group.com/en/adoit/",
                     Description = "ADOIT is an enterprise architecture management tool that provides functionalities and methods for enterprise analysis, design, planning, and implementation. The platform supports alignment and improvement of dependencies between business and IT as well as manages and analyses the dependencies between different organizational assets.",
                     EstablishedDate = new DateTime(1995),
                     NumberOfEmployee = 1000,
                     InternalProfessionalServices = true,
                     LastDemoDate = new DateTime(2023, 8, 3),
                     LastReviewDate = new DateTime(2023, 8, 3)

                 };
                 context.Companies.Add(company);
                 context.SaveChanges();


                 Software software = new Software()
                 {
                     CompanyID = 1,
                     Company = company,
                     SoftwareName = "ADOIT",
                     Description = "ADOIT is an enterprise architecture management tool that provides functionalities and methods for enterprise analysis, design, planning, and implementation. The platform supports alignment and improvement of dependencies between business and IT as well as manages and analyses the dependencies between different organizational assets.",
                     Cloud = CloudType.Based,
                     DocumentAttached = false
                 };

                 context.Softwares.Add(software);
                 context.SaveChanges();



                 //Seeding the join table (establish relationship)

                 //For each area inside the list create an instance in the join table
                 foreach (var area in businessAreaList)
                 {
                     Software_Area s_a = new Software_Area()
                     {
                         areaID = area.BusinessAreaID,
                         businessArea = area,
                         software = software,
                         softwareID = software.SoftwareID
                     };

                     //Add to table
                     context.Software_Areas.Add(s_a);
                 }
                 context.SaveChanges();

                 //Software Modules join table
                 context.Software_Modules.Add(
                     new Software_Module()
                     {
                         moduleID = softwareModule.SoftwareModuleID,
                         softwareModule = softwareModule,
                         software = software,
                         softwareID = software.SoftwareID
                     });
                 context.SaveChanges();


                 //Software Type join table
                 context.Software_Types.Add(
                     new Software_Type()
                     {
                         softwareType = softwareType,
                         typeID = softwareType.SoftwareTypeID,
                         software = software,
                         softwareID = software.SoftwareID
                     });
                 context.SaveChanges();



              

                 //company and cities
                 foreach(var country in countryList)
                 {
                     Company_Country c_c = new Company_Country()
                     {
                         country = country,
                         countryID = country.CountryID,
                         company = company,
                         companyID = company.CompanyID,
                     };
                     context.Add(c_c);
                 }
                 context.SaveChanges();

                


                */



                //Seed changeLog
                /*
                List<ChangeLog> changes = new List<ChangeLog>() {
                    new ChangeLog() {
                        EntityName = "Bitsey Studio",
                        ActionPerformed = Actions.deleted,
                        time = DateTime.Now
                    },
                    
                };

                context.AddRange(changes);

                context.SaveChanges();  
                */

                var software = context.Softwares.FirstOrDefault(s => s.SoftwareID == 1);
                var software2 = context.Softwares.FirstOrDefault(s => s.SoftwareID == 2);
                //Seed to be reviewed 
                List<SoftwareToBeReviewed> tobereviewed = new List<SoftwareToBeReviewed>()
                {
                    new SoftwareToBeReviewed()
                    {
                        software = software
                    },
                    new SoftwareToBeReviewed()
                    {
                        software = software2
                    },

                };

                context.softwareToBeRevieweds.AddRange(tobereviewed);
                context.SaveChanges();
                /*
                var employee1 = context.Employees.FirstOrDefault(e => e.EmployeeID == 1);
                employee1.LastRetrieveChangeLog = DateTime.Now;

                context.Update(employee1);
                context.SaveChanges();*/
                
                /*
                var employee2 = context.Employees.FirstOrDefault(e => e.EmployeeID == 2);
                employee2.LastRetrieveChangeLog = DateTime.Now;


                context.Update(employee2);
                context.SaveChanges();*/































            }

        }
    }
}
