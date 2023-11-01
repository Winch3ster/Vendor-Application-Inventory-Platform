using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Vendor_Application_Inventory_Platform.Models;
using Vendor_Application_Inventory_Platform.Data.Services;

namespace Vendor_Application_Inventory_Platform.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _service; //Inject the service of actors in here

        public EmployeeController(IEmployeeServices service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost] //We will be handling post request. Therefore this annotation is required
        public async Task<IActionResult> Create([Bind("EmployeeID, FirstName, LastName, Email, Password, IsAdmin")] Employee employee)
        {
            //In Actors class, we have 4 attributes but we only let the user enter 3 attibutes
            //This is because the id is excluded. So we need to bind the data

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");
                return View(employee);
                //What do the IsValid check?  --> If if all required fields are filled by [Required] (implemented in the employee class)

            }
            await _service.AddAsync(employee);// If the data is valid, add to database (This Add() is from the service class)
            return RedirectToAction(nameof(Index)); //Redirect back to the Employee's index view
        }



        public async Task<IActionResult> ListEmployee() //This method will be called by default
        {
            //get data from actors table only as this is controller for actors data only


            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list

            return View(allEmployeesData); //pass the data list to the view
        }





        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

    }
}
