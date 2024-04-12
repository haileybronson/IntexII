using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using IntexII.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IntexII.Controllers
{
    public class Brian1Controller : Controller
    {
        private readonly ICrudURepository<Customers> _repo;

        public Brian1Controller(ICrudURepository<Customers> temp)
        {
            _repo = temp;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CrudUsers()
        {
            // Retrieve most recent 50 customers
            var customers = _repo.GetMostRecent50Customers();


            // Pass the customers to the view
            return View(customers);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCustomer(int id)
        {
            var customer = _repo.GetCustomersById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCustomer(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateCustomers(customer);
                return RedirectToAction(nameof(CrudUsers));
            }
            return View(customer);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCustomers(int id)
        {
            var customer = _repo.GetCustomersById(id);
            if (customer == null)
            {
                return NotFound();
            }


            _repo.DeleteCustomers(id);
            return RedirectToAction(nameof(CrudUsers));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCustomer(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertCustomers(customer);
                return RedirectToAction(nameof(CrudUsers));
            }


            // If ModelState is not valid, return the form with validation errors
            return View(customer);
        }
    }
}

