using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;
using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    [Route("[controller]/[action]/")]
    [Authorize(Policy = "Authentication")]
    [Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
    public class SoftwareController : Controller
    {
        private readonly ISoftwareServices _services;

        public SoftwareController(ISoftwareServices s)
        {
            _services = s;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("~/Software")]
        [Route("~/Software/CreateSoftware")]
        public IActionResult CreateSoftware(int? id)
        {
            var companies = _services.ListAllCompanies();
            var businessAreas = _services.ListAllBusinessAreas();
            var softwareModules = _services.ListAllModules();
            var financialServicesClientTypes = _services.ListAllFinancialServicesClientTypes();
            var softwareTypes = _services.ListAllSoftwareType();

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
        public IActionResult CreateSoftware([FromBody] CreateSoftwareField createSoftwareField)
        {
            if (ModelState.IsValid)
            {
                if (createSoftwareField?.BusinessArea != null)
                {
                    var businessAreaList = createSoftwareField.BusinessArea;
                    bool hasDuplicate = businessAreaList.Count != businessAreaList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Business Area Field" });
                    }
                }

                if (createSoftwareField?.SoftwareTypes != null)
                {
                    var softwareTypeList = createSoftwareField.SoftwareTypes;
                    bool hasDuplicate = softwareTypeList.Count != softwareTypeList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Type Field" });
                    }
                }

                if (createSoftwareField?.Modules != null)
                {
                    var modulesList = createSoftwareField.Modules;
                    bool hasDuplicate = modulesList.Count != modulesList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Modules Field" });
                    }
                }

                if (createSoftwareField?.FinancialServiceClientType != null)
                {
                    var financialList = createSoftwareField.FinancialServiceClientType;
                    bool hasDuplicate = financialList.Count != financialList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new
                            { success = false, message = "Duplicate Value in Financial Service Client Type Field" });
                    }
                }

                if (createSoftwareField != null)
                {
                    var software = new Software()
                    {
                        SoftwareName = createSoftwareField.SoftwareName,
                        Description = createSoftwareField.Description,
                        Cloud = Enum.Parse<CloudType>(createSoftwareField.CloudOption, true),
                        DocumentAttached = false,
                        CompanyID = int.Parse(createSoftwareField.CompanyID)
                    };

                    var createdSoftware = _services.CreateNewSoftware(software);

                    _services.Link_Software_Type(createdSoftware.SoftwareID, createSoftwareField.SoftwareTypes);
                    _services.Link_Software_Area(createdSoftware.SoftwareID, createSoftwareField.BusinessArea);
                    _services.Link_Software_Module(createdSoftware.SoftwareID, createSoftwareField.Modules);
                    _services.Link_Software_Financial(createdSoftware.SoftwareID,
                        createSoftwareField.FinancialServiceClientType);
                }

                return Json(new { success = true, message = "Software Created" });
            }

            return Json(new { success = false, message = "Failed to create software" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSoftware([FromBody] CreateSoftwareField createSoftwareField, int? id)
        {
            if (ModelState.IsValid)
            {
                if (createSoftwareField?.BusinessArea != null)
                {
                    var businessAreaList = createSoftwareField.BusinessArea;
                    bool hasDuplicate = businessAreaList.Count != businessAreaList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Business Area Field" });
                    }
                }

                if (createSoftwareField?.SoftwareTypes != null)
                {
                    var softwareTypeList = createSoftwareField.SoftwareTypes;
                    bool hasDuplicate = softwareTypeList.Count != softwareTypeList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Type Field" });
                    }
                }

                if (createSoftwareField?.Modules != null)
                {
                    var modulesList = createSoftwareField.Modules;
                    bool hasDuplicate = modulesList.Count != modulesList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new { success = false, message = "Duplicate Value in Software Modules Field" });
                    }
                }

                if (createSoftwareField?.FinancialServiceClientType != null)
                {
                    var financialList = createSoftwareField.FinancialServiceClientType;
                    bool hasDuplicate = financialList.Count != financialList.Distinct().Count();
                    if (hasDuplicate)
                    {
                        return Json(new
                            { success = false, message = "Duplicate Value in Financial Service Client Type Field" });
                    }
                }

                if (createSoftwareField != null)
                {
                    int softwareId = id ?? 0;
                    var software = _services.FindSoftware(softwareId);

                    if (software != null)
                    {
                        var updatedSoftware = new Software()
                        {
                            SoftwareName = createSoftwareField.SoftwareName,
                            Description = createSoftwareField.Description,
                            Cloud = Enum.Parse<CloudType>(createSoftwareField.CloudOption, true),
                            DocumentAttached = false,
                            CompanyID = int.Parse(createSoftwareField.CompanyID)
                        };
                        
                        _services.UpdateSoftware(software.SoftwareID, updatedSoftware);
                        
                        _services.Link_Software_Type(softwareId, createSoftwareField.SoftwareTypes);
                        _services.Link_Software_Area(softwareId, createSoftwareField.BusinessArea);
                        _services.Link_Software_Module(softwareId, createSoftwareField.Modules);
                        _services.Link_Software_Financial(softwareId,
                            createSoftwareField.FinancialServiceClientType);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Software not found" });
                    }

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
    }
}