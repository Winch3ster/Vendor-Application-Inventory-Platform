using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using System;
using System.Security.Claims;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;
using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    [Route("[controller]/[action]/")]
    [Authorize(Policy = "Authentication")]
    [Authorize(Policy = "AdminOrSoftwarePolicy")]
    [Area("Admin")]
    public class SoftwareController : Controller
    {
        private readonly ISoftwareServices _services;
        private readonly IConfiguration _configuration; //To get the upload file path
        private readonly IChangeLogService _changeLogService;
        private readonly EmailService _emailService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public SoftwareController(ISoftwareServices s, IConfiguration configuration, IChangeLogService changeLogService, EmailService emailService, IHubContext<NotificationHub> hubContext)
        {
            _services = s;
            _configuration = configuration;
            _changeLogService = changeLogService;
            _emailService = emailService;
            _hubContext = hubContext;
        }

        [Route("~/Software")]
        [Route("~/Software/Index")]
        public IActionResult Index()
        {
            List<Software> software = _services.ListAllSoftware();
            List<SoftwareViewModel> viewModelList = software.Select(software => new SoftwareViewModel
            {
                SoftwareID = software.SoftwareID,
                SoftwareName = software.SoftwareName,
                Description = software.Description,
                Cloud = software.Cloud,
                DocumentAttached = software.DocumentAttached,
                FinancialServicesClientTypes = _services.GetFinancialServicesClientTypeNames(software.SoftwareID),
                SoftwareTypes = _services.GetSoftwareTypeNames(software.SoftwareID),
                SoftwareModules = _services.GetModuleNames(software.SoftwareID),
                BusinessAreas = _services.GetBusinessAreaNames(software.SoftwareID)

            }).ToList();

            return View(viewModelList);
        }

        public IActionResult CreateSoftware(int? id)
        {
            var companies = _services.ListAllCompanies();
            var businessAreas = _services.ListAllBusinessAreas();
            var softwareModules = _services.ListAllModules();
            var financialServicesClientTypes = _services.ListAllFinancialServicesClientTypes();
            var softwareTypes = _services.ListAllSoftwareType();
            var pdfDocument = _services.GetPdf(id ?? 0);

            // Create the ViewModel and populate it with the retrieved data
            var viewModel = new CreateSoftwareViewModel
            {
                Companies = companies,
                BusinessAreas = businessAreas,
                SoftwareModules = softwareModules,
                FinancialServicesClientTypes = financialServicesClientTypes,
                SoftwareTypes = softwareTypes
            };

            if (id != null && id != 0)
            {
                int softwareId = id ?? 0;

                Software? software = _services.FindSoftware(softwareId);

                if (software == null)
                {
                    return NotFound();
                }

                var linkedBusinessArea = _services.GetBusinessAreas(softwareId);
                var linkedSoftwareType = _services.GetSoftwareType(softwareId);
                var linkedModules = _services.GetModules(softwareId);
                var linkedFinancial = _services.GetFinancialServicesClientTypes(softwareId);

                var updateViewModel = new UpdateSoftwareViewModel()
                {
                    Companies = companies,
                    BusinessAreas = businessAreas,
                    SoftwareModules = softwareModules,
                    FinancialServicesClientTypes = financialServicesClientTypes,
                    SoftwareTypes = softwareTypes,
                    companyId = software.CompanyID,
                    software = software,
                    pdfDocument = pdfDocument,
                    LinkedBusinessAreas = linkedBusinessArea,
                    LinkedSoftwareTypes = linkedSoftwareType,
                    LinkedSoftwareModules = linkedModules,
                    LinkedFinancialServicesClientTypes = linkedFinancial
                };

                return View("UpdateSoftware", updateViewModel);
            }

            return View("CreateSoftware", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSoftware()
        {


            if (ModelState.IsValid)
            {
                var imageFile = HttpContext.Request.Form.Files["Image"];
                System.Diagnostics.Debug.WriteLine(imageFile);

                var uploadFolder = _configuration.GetValue<string>("ImagesUploadSettings:UploadFolder");
                System.Diagnostics.Debug.WriteLine(imageFile);

                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadFolder);
                System.Diagnostics.Debug.WriteLine(imageFile);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsFolderPath, uniqueFileName); //Create an absolute path to the image (including C:/.... )
                filePath = filePath.Replace("\\", "/");

                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // Construct the relative file path
                var relativeImagePath = Path.Combine(uploadFolder, uniqueFileName);



                var companyId = int.Parse(HttpContext.Request.Form["CompanyID"]);
                var softwareName = HttpContext.Request.Form["SoftwareName"];
                var description = HttpContext.Request.Form["Description"];
                var cloudOption = HttpContext.Request.Form["CloudOption"];
                var file = HttpContext.Request.Form.Files["File"];

                var businessArea = HttpContext.Request.Form["BusinessArea[]"].Select(int.Parse).ToList();
                var softwareType = HttpContext.Request.Form["SoftwareTypes[]"].Select(int.Parse).ToList();
                var modules = HttpContext.Request.Form["Modules[]"].Select(int.Parse).ToList();
                var financial = HttpContext.Request.Form["FinancialServiceClientType[]"].Select(int.Parse).ToList();


                //check is business area is null

                if (businessArea.Count != 0)
                {
                    bool hasDuplicate = businessArea.Count != businessArea.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Business Area Field" });
                    }
                }

                //check if software types is null
                if (softwareType.Count != 0)
                {
                    bool hasDuplicate = softwareType.Count != softwareType.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Type Field" });
                    }
                }

                //check if modules is null
                if (modules.Count != 0)
                {
                    bool hasDuplicate = modules.Count != modules.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Modules Field" });
                    }
                }

                //check if financial is null

                if (financial.Count != 0)
                {
                    bool hasDuplicate = financial.Count != financial.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new
                        { success = false, message = "Duplicate Value in Financial Service Client Type Field" });
                    }
                }



                var software = new Software()
                {
                    SoftwareName = softwareName,
                    Description = description,
                    Cloud = Enum.Parse<CloudType>(cloudOption, true),
                    DocumentAttached = file != null,
                    CompanyID = companyId,
                    ImagePath = relativeImagePath
                };

                var createdSoftware = _services.CreateNewSoftware(software);
                ////////////////////////////////////////////////////
                ///




                _changeLogService.AddChange(software.SoftwareName, Actions.added);
                _hubContext.Clients.All.SendAsync("notification", "update");

                _emailService.SendEmail(User.FindFirstValue(ClaimTypes.NameIdentifier), "Employee Data Edit", "Software", software.SoftwareName, "added");



                ////////////////////////////////////////////////////






                //store file to db
                if (file != null)
                {
                    if (createdSoftware != null)
                    {
                        using var memoryStream = new MemoryStream();
                        file.CopyTo(memoryStream);
                        var pdfDocument = new PdfDocument()
                        {
                            FileName = file.FileName,
                            Content = memoryStream.ToArray(),
                            softwareId = createdSoftware.SoftwareID
                        };
                        _services.CreatePdf(pdfDocument);
                    }

                }

                _services.Link_Software_Type(createdSoftware.SoftwareID, softwareType);
                _services.Link_Software_Area(createdSoftware.SoftwareID, businessArea);
                _services.Link_Software_Module(createdSoftware.SoftwareID, modules);
                _services.Link_Software_Financial(createdSoftware.SoftwareID, financial);


                return Json(new { success = true, message = "Software Created" });
            }

            return Json(new { success = false, message = "Failed to create software" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSoftware(int? id)
        {
            if (ModelState.IsValid)
            {
                var companyId = int.Parse(HttpContext.Request.Form["CompanyID"]);
                var softwareName = HttpContext.Request.Form["SoftwareName"];
                var description = HttpContext.Request.Form["Description"];
                var cloudOption = HttpContext.Request.Form["CloudOption"];
                var file = HttpContext.Request.Form.Files["File"];
                var image = HttpContext.Request.Form.Files["Image"];


                var businessArea = HttpContext.Request.Form["BusinessArea[]"].Select(int.Parse).ToList();
                var softwareType = HttpContext.Request.Form["SoftwareTypes[]"].Select(int.Parse).ToList();
                var modules = HttpContext.Request.Form["Modules[]"].Select(int.Parse).ToList();
                var financial = HttpContext.Request.Form["FinancialServiceClientType[]"].Select(int.Parse).ToList();



                var imageFile = HttpContext.Request.Form.Files["Image"];
                System.Diagnostics.Debug.WriteLine(imageFile);





                //check is business area is null

                if (businessArea.Count != 0)
                {
                    bool hasDuplicate = businessArea.Count != businessArea.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Business Area Field" });
                    }
                }

                //check if software types is null
                if (softwareType.Count != 0)
                {
                    bool hasDuplicate = softwareType.Count != softwareType.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Type Field" });
                    }
                }

                //check if modules is null
                if (modules.Count != 0)
                {
                    bool hasDuplicate = modules.Count != modules.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Modules Field" });
                    }
                }

                //check if financial is null

                if (financial.Count != 0)
                {
                    bool hasDuplicate = financial.Count != financial.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new
                        { success = false, message = "Duplicate Value in Financial Service Client Type Field" });
                    }
                }

                int softwareId = id ?? 0;
                var software = _services.FindSoftware(softwareId);

                //Check if a new image is uploaded 
                string imagePath = software.ImagePath;

                if (imageFile != null)
                {

                    var uploadFolder = _configuration.GetValue<string>("ImagesUploadSettings:UploadFolder");
                    System.Diagnostics.Debug.WriteLine(uploadFolder);

                    var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    System.Diagnostics.Debug.WriteLine(uploadsFolderPath);

                    //delete the old image
                    string filePath = Path.Combine(uploadsFolderPath, imagePath);
                    System.Diagnostics.Debug.WriteLine(filePath);

                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (IOException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error deleting file: {ex.Message}");

                    }



                    //set the new image
                    uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadFolder);
                    System.Diagnostics.Debug.WriteLine(imageFile);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var newfilePath = Path.Combine(uploadsFolderPath, uniqueFileName); //Create an absolute path to the image (including C:/.... )
                    newfilePath = newfilePath.Replace("\\", "/");
                    System.Diagnostics.Debug.WriteLine(newfilePath);

                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }

                    using (var stream = new FileStream(newfilePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }
                    // Construct the relative file path
                    var newRelativeImagePath = Path.Combine(uploadFolder, uniqueFileName);

                    System.Diagnostics.Debug.WriteLine(newRelativeImagePath);
                    imagePath = newRelativeImagePath;
                }


                System.Diagnostics.Debug.WriteLine(imagePath);

                if (software != null)
                {
                    var updatedSoftware = new Software()
                    {
                        SoftwareName = softwareName,
                        Description = description,
                        Cloud = Enum.Parse<CloudType>(cloudOption, true),
                        DocumentAttached = file != null,
                        CompanyID = companyId,
                        ImagePath = imagePath
                    };


                    _services.UpdateSoftware(software.SoftwareID, updatedSoftware);


                    ///////////////////////////////////////////////////////////




                    _changeLogService.AddChange(software.SoftwareName, Actions.edited);
                    _hubContext.Clients.All.SendAsync("notification", "update");

                    _emailService.SendEmail(User.FindFirstValue(ClaimTypes.NameIdentifier), "Software Updated", "Software", software.SoftwareName, "edited");



                    ////////////////////////////////////////////////////
                    ///

                


                //store file to db
                if (file != null)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);
                    var pdfDocument = new PdfDocument()
                    {
                        FileName = file.FileName,
                        Content = memoryStream.ToArray(),
                        softwareId = software.SoftwareID
                    };
                    _services.CreatePdf(pdfDocument);

                }

                _services.Link_Software_Type(softwareId, softwareType);
                _services.Link_Software_Area(softwareId, businessArea);
                _services.Link_Software_Module(softwareId, modules);
                _services.Link_Software_Financial(softwareId, financial);
            }
            else
            {
                return Json(new { success = false, message = "Software not found" });
            }



            return Json(new { success = true, message = "Software Updated" });
        }

            return Json(new { success = false, message = "Failed to update software" });
        }
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBusinessArea(BusinessArea businessArea)
        {
            _services.CreateBusinessArea(businessArea);
            var businessAreaList = _services.ListAllBusinessAreas();
            return Json(new { success = true, list = businessAreaList });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSoftwareType(SoftwareType softwareType)
        {
            _services.CreateSoftwareType(softwareType);
            var softwareTypeList = _services.ListAllSoftwareType();
            return Json(new { success = true, list = softwareTypeList });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSoftwareModule(SoftwareModule softwareModule)
        {
            _services.CreateSoftwareModule(softwareModule);
            var softwareModuleList = _services.ListAllModules();
            return Json(new { success = true, list = softwareModuleList });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFinancialService(FinancialServicesClientType financialServicesClientType)
        {
            _services.CreateFinancialService(financialServicesClientType);
            var financialList = _services.ListAllFinancialServicesClientTypes();
            return Json(new { success = true, list = financialList });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSoftware(int id)
        {
            _changeLogService.AddChangeDeleteSoftwareById(id, Actions.deleted);
            _services.deleteSoftware(id);

            ////////////////////////////////////////////////////
            ///


            _hubContext.Clients.All.SendAsync("notification", "update");





            _emailService.SendEmail(User.FindFirstValue(ClaimTypes.NameIdentifier), "Software Removed", "Software", "A software", "removed");




            ////////////////////////////////////////////////////
            return RedirectToAction("Index");
        }

        public IActionResult ViewPdf(int id)
        {
            
            var pdfDocument = _services.GetPdf(id);

            if (pdfDocument != null)
            {
                // Set Content-Disposition to display inline
                var contentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = pdfDocument.FileName,
                    Size = pdfDocument.Content.Length
                };

                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

                return File(pdfDocument.Content, "application/pdf");
            }

            // Handle case where the PDF document is not found
            return NotFound();
            
        }

        public IActionResult DownloadPdf(int id)
        {
            var pdfDocument = _services.GetPdf(id);

            if (pdfDocument != null)
            {
                return File(pdfDocument.Content, "application/pdf", pdfDocument.FileName);
            }

            // Handle case where the PDF document is not found
            return NotFound();
        }
    }
}