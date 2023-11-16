using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    [Authorize(Policy = "Authentication")]
    [Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeServices _service; //Inject the service of actors in here

        public EmployeeController(IEmployeeServices service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index() //This method will be called by default
        {
            //get data from actors table only as this is controller for actors data only


            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list

            return View(allEmployeesData); //pass the data list to the view
        }


        public IActionResult RandomPage()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost] //We will be handling post request. Therefore this annotation is required
        public async Task<IActionResult> Create([Bind("FirstName, LastName, Email, Password, IsAdmin")] Employee employee)
        {

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");
                return View(employee);
                //What do the IsValid check?  --> If if all required fields are filled by [Required] (implemented in the employee class)

            }
            await _service.AddAsync(employee);// If the data is valid, add to database (This Add() is from the service class)
            return RedirectToAction("Index", "Employee", new { area = "Admin" });  //Redirect back to the Employee's index view
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if(result == null)
            {
                return View("Error");
            }
            return View(result);
        }


        [HttpPost] //We will be handling post request. Therefore this annotation is required
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID, FirstName, LastName, Email, Password, IsAdmin")] Employee employee)
        {

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");
                return View(employee);
                //What do the IsValid check?  --> If if all required fields are filled by [Required] (implemented in the employee class)

            }
            await _service.UpdateAsync(id, employee);// If the data is valid, add to database (This Add() is from the service class)
            return RedirectToAction("Index", "Employee", new { area = "Admin" }); //Redirect back to the Employee's index view
        }




        public async Task<IActionResult> Delete(int id)
        {

            var actorDetails = await _service.GetByIdAsync(id);

            //If doesnt exist return message
            if (actorDetails == null)
            {
                // return View("NotFound");
            }

            await _service.DeleteAsync(id);

            return RedirectToAction("Index", "Employee", new { area = "Admin" }); //Redirect back to the actor's index view
        }


    }
}
