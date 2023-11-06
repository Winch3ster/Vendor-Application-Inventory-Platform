using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services
{
    public interface ISoftwareServices
    {

        //Add Software in database
        Task AddAsync(Software software);

        Task<IEnumerable<Software>> GetAllAsync();

        //Update the actor data and return the updated result to user
        Task<Software> UpdateAsync(int id, Software newSoftwareData);

        Task<Software> GetByIdAsync(int id);

        Task DeleteAsync(int id);

    }
}
