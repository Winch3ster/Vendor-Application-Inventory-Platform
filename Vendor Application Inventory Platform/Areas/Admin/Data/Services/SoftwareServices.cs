using Vendor_Application_Inventory_Platform.Areas.User.ViewModels;
using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public class SoftwareServices : ISoftwareServices
    {
        
        private readonly AppDbContext _db;
       
        public SoftwareServices(AppDbContext db)
        {
            _db = db;
           
        }

        public List<Company?> ListAllCompanies()
        {
            return _db.Companies.ToList();
        }

        public List<BusinessArea> ListAllBusinessAreas()
        {
            return _db.BusinessAreas.ToList();
        }

        public List<SoftwareModule> ListAllModules()
        {
            return _db.SoftwareModules.ToList();
        }

        public List<FinancialServicesClientType> ListAllFinancialServicesClientTypes()
        {
            return _db.FinancialServicesClientTypes.ToList();
        }

        public List<SoftwareType> ListAllSoftwareType()
        {
            return _db.SoftwareTypes.ToList();
        }
        
        public List<Software> ListAllSoftware()
        {
            return _db.Softwares.ToList();
        }
        
        public List<string> GetBusinessAreaNames(int softwareId)
        {
            var businessAreaIds = _db.Software_Areas.Where(area => area.softwareID == softwareId).Select(area=>area.areaID).ToList();

            var matchingBusinessAreas =
                _db.BusinessAreas.Where(area => businessAreaIds.Contains(area.BusinessAreaID)).Select(area=>area.Description).ToList();

            return matchingBusinessAreas;
        }
        
        public List<string> GetModuleNames(int softwareId)
        {
            var softwareModuleIds = _db.Software_Modules.Where(module => module.softwareID == softwareId).Select(module=>module.moduleID).ToList();

            var matchingSoftwareModules =
                _db.SoftwareModules.Where(modules => softwareModuleIds.Contains(modules.SoftwareModuleID)).Select(module=>module.Module).ToList();

            return matchingSoftwareModules;
        }
        
        public List<string> GetFinancialServicesClientTypeNames(int softwareId)
        {
            var financialIds = _db.Software_FinancialServicesClientTypes.Where(financial => financial.softwareID == softwareId).Select(financial=>financial.financialServicesClientTypeID).ToList();

            var matchingFinancial =
                _db.FinancialServicesClientTypes.Where(financial => financialIds.Contains(financial.FinancialServicesClientTypeID)).Select(financial=>financial.Description).ToList();

            return matchingFinancial;
        }
        
        public List<string> GetSoftwareTypeNames(int softwareId)
        {
            var softwareTypeIds = _db.Software_Types.Where(type => type.softwareID == softwareId).Select(type=>type.typeID).ToList();

            var matchingSoftwareTypes =
                _db.SoftwareTypes.Where(type => softwareTypeIds.Contains(type.SoftwareTypeID)).Select(type=>type.Type).ToList();

            return matchingSoftwareTypes;
        }

        public BusinessArea CreateBusinessArea(BusinessArea businessArea)
        {
            var existingArea = _db.BusinessAreas.FirstOrDefault(area => area.Description == businessArea.Description);

            if (existingArea != null)
            {
                return existingArea;
            }
            
            _db.BusinessAreas.Add(businessArea);
            _db.SaveChanges();
            return businessArea;
        }

        public SoftwareType CreateSoftwareType(SoftwareType softwareType)
        {
            var existingSoftwareType = _db.SoftwareTypes.FirstOrDefault(type => type.Type == softwareType.Type);
            if (existingSoftwareType != null)
            {
                return softwareType;
            }

            _db.SoftwareTypes.Add(softwareType);
            _db.SaveChanges();
            return softwareType;
        }

        public SoftwareModule CreateSoftwareModule(SoftwareModule softwareModule)
        {
            var existingModule = _db.SoftwareModules.FirstOrDefault(module => module.Module == softwareModule.Module);
            if (existingModule != null)
            {
                return existingModule;
            }

            _db.SoftwareModules.Add(softwareModule);
            _db.SaveChanges();
            return softwareModule;
        }

        public FinancialServicesClientType CreateFinancialService(
            FinancialServicesClientType financialServicesClientType)
        {
            var existingFinancial =
                _db.FinancialServicesClientTypes.FirstOrDefault(f =>
                    f.Description == financialServicesClientType.Description);
            if (existingFinancial != null)
            {
                return existingFinancial;
            }

            _db.FinancialServicesClientTypes.Add(financialServicesClientType);
            _db.SaveChanges();
            return financialServicesClientType;
        }

        public Software CreateNewSoftware(Software software)
        {
            _db.Softwares.Add(software);
            _db.SaveChanges();

         
            return software;
        }

        public void Link_Software_Type(int softwareId, List<int> softwareTypeIds)
        {
            // check if software is already linked to type
            var existingSoftwareTypes = _db.Software_Types.Where(type => type.softwareID == softwareId).ToList();
            if (existingSoftwareTypes != null)
            {
                //remove existing links
                _db.Software_Types.RemoveRange(existingSoftwareTypes);
            }
            foreach (var id in softwareTypeIds)
            {
                var newType = new Software_Type()
                {
                    softwareID = softwareId,
                    typeID = id
                };

                _db.Software_Types.Add(newType);
            }

            _db.SaveChanges();
        }

        public void Link_Software_Module(int softwareId, List<int> softwareModuleIds)
        {
            //check if software is already linked to module
            var existingSoftwareModules = _db.Software_Modules.Where(module => module.softwareID == softwareId).ToList();

            if (existingSoftwareModules != null)
            {
                //remove existing links
                _db.Software_Modules.RemoveRange(existingSoftwareModules);
            }
            
            foreach (var id in softwareModuleIds)
            {
                var newModule = new Software_Module()
                {
                    softwareID = softwareId,
                    moduleID = id
                };

                _db.Software_Modules.Add(newModule);
            }

            _db.SaveChanges();
        }

        public void Link_Software_Financial(int softwareId, List<int> financialIds)
        {
            var existingFinancial = _db.Software_FinancialServicesClientTypes.Where(financial => financial.softwareID == softwareId).ToList();
            
            if(existingFinancial != null)
            {
                _db.Software_FinancialServicesClientTypes.RemoveRange(existingFinancial);
            }
            
            foreach (var id in financialIds)
            {
                var newFinancial = new Software_FinancialServicesClientType()
                {
                    softwareID = softwareId,
                    financialServicesClientTypeID = id
                };

                _db.Software_FinancialServicesClientTypes.Add(newFinancial);
            }

            _db.SaveChanges();
        }

        public void Link_Software_Area(int softwareId, List<int> areaIds)
        {
            var existingAreas = _db.Software_Areas.Where(area => area.softwareID == softwareId).ToList();
            
            if(existingAreas != null)
            {
                _db.Software_Areas.RemoveRange(existingAreas);
            }
            
            foreach (var id in areaIds)
            {
                var newArea = new Software_Area()
                {
                    softwareID = softwareId,
                    areaID = id
                };

                _db.Software_Areas.Add(newArea);
            }

            _db.SaveChanges();
        }
        
        public List<BusinessArea> GetBusinessAreas(int softwareId)
        {
            var businessAreaIds = _db.Software_Areas.Where(area => area.softwareID == softwareId).Select(area=>area.areaID).ToList();

            var matchingBusinessAreas =
                _db.BusinessAreas.Where(area => businessAreaIds.Contains(area.BusinessAreaID)).ToList();

            return matchingBusinessAreas;
        }

        public List<SoftwareModule> GetModules(int softwareId)
        {
            var softwareModuleIds = _db.Software_Modules.Where(module => module.softwareID == softwareId).Select(module=>module.moduleID).ToList();

            var matchingSoftwareModules =
                _db.SoftwareModules.Where(modules => softwareModuleIds.Contains(modules.SoftwareModuleID)).ToList();

            return matchingSoftwareModules;
        }

        public List<FinancialServicesClientType> GetFinancialServicesClientTypes(int softwareId)
        {
            var financialIds = _db.Software_FinancialServicesClientTypes.Where(financial => financial.softwareID == softwareId).Select(financial=>financial.financialServicesClientTypeID).ToList();

            var matchingFinancial =
                _db.FinancialServicesClientTypes.Where(financial => financialIds.Contains(financial.FinancialServicesClientTypeID)).ToList();

            return matchingFinancial;
        }

        public List<SoftwareType> GetSoftwareType(int softwareId)
        {
            var softwareTypeIds = _db.Software_Types.Where(type => type.softwareID == softwareId).Select(type=>type.typeID).ToList();

            var matchingSoftwareTypes =
                _db.SoftwareTypes.Where(type => softwareTypeIds.Contains(type.SoftwareTypeID)).ToList();

            return matchingSoftwareTypes;
        }

        public Software? FindSoftware(int softwareId)
        {
            return _db.Softwares.FirstOrDefault(s => s.SoftwareID == softwareId);
        }

        public void UpdateSoftware(int softwareId, Software software)
        {
            var existingSoftware = _db.Softwares.FirstOrDefault(s=>s.SoftwareID == softwareId);
            
            if (existingSoftware != null)
            {
                existingSoftware.SoftwareName= software.SoftwareName;
                existingSoftware.Description = software.Description;
                existingSoftware.CompanyID = software.CompanyID;
                existingSoftware.Cloud = software.Cloud;
                existingSoftware.DocumentAttached = software.DocumentAttached;

                _db.SaveChanges();


           

            }
        }
        
        public void deleteSoftware(int softwareId)
        {
            var existingSoftware = _db.Softwares.FirstOrDefault(s=>s.SoftwareID == softwareId);
            
            if (existingSoftware != null)
            {
                _db.Softwares.Remove(existingSoftware);
                _db.SaveChanges();


            }
        }

        public void CreatePdf(PdfDocument pdfDocument)
        {
            //check if pdf already exists
            var existingPdf = _db.PdfDocuments.FirstOrDefault(pdf => pdf.softwareId == pdfDocument.softwareId);
            
            if (existingPdf != null)
            {
                _db.PdfDocuments.Remove(existingPdf);
            }
            _db.PdfDocuments.Add(pdfDocument);
            _db.SaveChanges();
        }

        public PdfDocument? GetPdf(int softwareId)
        {
            if (softwareId == 0)
            {
                return null;
            }

            return _db.PdfDocuments.FirstOrDefault(pdf => pdf.softwareId == softwareId);
        }

    }
    
}
