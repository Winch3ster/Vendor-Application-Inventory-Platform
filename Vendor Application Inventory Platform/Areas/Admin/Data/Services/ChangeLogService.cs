using System;
using Vendor_Application_Inventory_Platform.Data.Enum;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
 
    public class ChangeLogService : IChangeLogService
    {
        private readonly AppDbContext _db;

        public ChangeLogService(AppDbContext db)
        {
            _db = db;
        }

        public void AddChange(string entitiyName, Actions action)
        {
            ChangeLog change = new ChangeLog()
            {
                EntityName = entitiyName,
                ActionPerformed = action,
                time = DateTime.Now,
            };

            _db.changeLogs.Add(change);
            _db.SaveChanges();
        }

        public void AddChangeDeleteSoftwareById(int id, Actions action)
        {
            var software = _db.Softwares.FirstOrDefault(s => s.SoftwareID == id);   
            ChangeLog change = new ChangeLog()
            {
                EntityName = software.SoftwareName,
                ActionPerformed = action,
                time = DateTime.Now,
            };

            _db.changeLogs.Add(change);
            _db.SaveChanges();
        }



        public void AddChangeDeleteCompanyById(int id, Actions action)
        {
            var company = _db.Companies.FirstOrDefault(c => c.CompanyID == id);
            ChangeLog change = new ChangeLog()
            {
                EntityName = company.CompanyName,
                ActionPerformed = action,
                time = DateTime.Now,
            };

            _db.changeLogs.Add(change);
            _db.SaveChanges();
        }

    }
}
