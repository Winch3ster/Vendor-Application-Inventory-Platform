using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Vendor_Application_Inventory_Platform.Data.Services;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Controllers
{

    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IUserEmployeeServices _service; //Inject the service of actors in here

        public EmployeeController(IUserEmployeeServices service)
        {
            _service = service;
        }
        
        [Route("~/Employee")]
        [Route("~/Employee/Index")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        [Route("~/Employee/List")]
        public async Task<IActionResult> List() //This method will be called by default
        {
            //get data from actors table only as this is controller for actors data only


            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list

            return View(allEmployeesData); //pass the data list to the view
        }



        public async Task<IActionResult> Create()
        {
            return View();
        }





        [HttpPost] //We will be handling post request. Therefore this annotation is required
        public async Task<IActionResult> Create([Bind("EmployeeID, FirstName, LastName, Email, Password, IsAdmin")] Employee employee)
        {
         
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");
                return View(employee);
                //What do the IsValid check?  --> If if all required fields are filled by [Required] (implemented in the employee class)

            }
            await _service.AddAsync(employee);// If the data is valid, add to database (This Add() is from the service class)
            return RedirectToAction(nameof(Index)); //Redirect back to the Employee's index view
        }




        public async Task<IActionResult> EditEmployee(int id)
        {
            //check if that actor exist in database
            var employeeDetails = await _service.GetByIdAsync(id);

            //If dowsnt exist return message
            if (employeeDetails == null)
            {
                // return View("NotFound");
            }
            return View(employeeDetails);

        }


        [HttpPost] //We will be handling post request. Therefore this annotation is required
        public async Task<IActionResult> EditEmployee(int id, [Bind("EmployeeID, FirstName, LastName, Email, Password, IsAdmin")] Employee employee)
        {

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");
                return View(employee);
                //What do the IsValid check?  --> If if all required fields are filled by [Required] (implemented in the employee class)

            }
            await _service.UpdateAsync(id, employee);// If the data is valid, add to database (This Add() is from the service class)
            return RedirectToAction(nameof(Index)); //Redirect back to the Employee's index view
        }





        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

    }
}
