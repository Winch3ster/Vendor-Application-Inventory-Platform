using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vendor_Application_Inventory_Platform.Areas.Admin.ViewModels;
using Vendor_Application_Inventory_Platform.Data.Services;
using Vendor_Application_Inventory_Platform.Models;

namespace Vendor_Application_Inventory_Platform.Areas.Admin.Controllers
{

    
    [Authorize(Policy = "Authentication")]
    [Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
    [Route("[controller]/[action]/")]
  
    public class AdminController : Controller
    {
        private readonly IUserEmployeeServices _service;
        public AdminController(IUserEmployeeServices service)
        {
            _service = service;
        }

        [Route("~/Admin")]
        [Route("~/Admin/Index")]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentlySignedInUser = await _service.GetCurrentUser(userEmail);

            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list

            var data = new EmployeeIndexVM()
            {
                signedInUser = currentlySignedInUser,
                Employees = allEmployeesData
            };


            return View(data); //pass the data list to the view
        }


        public IActionResult Create()
        {
            return View();
        }
        
        [Route("~/Admin/Employees")]
        public async Task<IActionResult> Employees() //This method will be called by default
        {
            //get data from actors table only as this is controller for actors data only
        
        
            //If here is,the interface and service class must be async as well
            var allEmployeesData = await _service.GetAllAsync();//Convert the data to list
        
            return View(allEmployeesData); //pass the data list to the view
        }
        

        //Get request Admin/Employees/id
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            //check if that actor exist in database
            var employeeDetails = await _service.GetByIdAsync(id);
        
            //If doesnt exist return message
            if (employeeDetails == null)
            {
                // return View("NotFound");
            }
            return View(employeeDetails);
        
        }




        // GET: /Employees/Update/1
        [HttpGet]
        [ActionName("Update")]
        //[Route("~/Admin/Employee/Update/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _service.GetByIdAsync(id);
        
            if (employee == null)
            {
                // return View("NotFound");
            }
        
            return View(employee);
        }

        // POST: /Products/Update/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        //[Route("~/Admin/Employee/Update/{id}")]
        //[Route("Employee/Update/{id}")]
        
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, employee);
                return RedirectToAction("Employees");
            }
        
            return View(employee);
        }

    }
    
}