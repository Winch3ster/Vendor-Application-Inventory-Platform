using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{
    [Authorize(Policy = "Authentication")]
    [Authorize(Policy = "AdminOrAccountPolicy")]
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeServices _service; //Inject the service of employee in here
        private readonly NotificationService _notificationService;
        private readonly EmailService _emailService;
        public EmployeeController(IEmployeeServices service, NotificationService notificationService, EmailService emailService)
        {
            _notificationService = notificationService;
            _service = service;
            _emailService = emailService;
        }

        [Route("~/Employee")]
        [Route("~/Employee/Index")]
        public async Task<IActionResult> Index() //This method will be called by default
        {
            //get data from actors table only as this is controller for actors data only

            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list


            //For testing purposes The email will be send to one of the developer's email
            //_notificationService.NotifyUser(, "View all employee");
            // Send email
            _emailService.SendEmail("kingstonlee96@gmail.com", "Subject", "Body of the email");

            // Disconnect from the SMTP server after sending the email
            _emailService.Disconnect();

            System.Diagnostics.Debug.WriteLine("The mail should be sent");
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
        public async Task<IActionResult> Create([Bind("FirstName, LastName, Email, Password, IsAdmin, companyAccess, softwareAccess, accountAccess")] Employee employee)
        {

            ModelState.Remove("reviews");
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(employee);
                //What do the IsValid check?  --> If if all required fields are filled by [Required] (implemented in the employee class)

            }
            await _service.AddAsync(employee);// If the data is valid, add to database (This Add() is from the service class)


            //For testing purposes The email will be send to one of the developer's email
            _notificationService.NotifyUser("kingstonlee96@gmail.com", "Create");

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
        public async Task<IActionResult> Edit([Bind("EmployeeID, FirstName, LastName, Email, Password, IsAdmin, companyAccess, softwareAccess, accountAccess")] Employee employee)
        {

            ModelState.Remove("Password");
            ModelState.Remove("reviews");
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");
                return View(employee);
                //What do the IsValid check?  --> If if all required fields are filled by [Required] (implemented in the employee class)

            }
            await _service.UpdateAsync(employee.EmployeeID, employee);// If the data is valid, add to database (This Add() is from the service class)
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
