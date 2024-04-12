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

        [Authorize(Roles = "ADMIN")]
        public IActionResult CrudCustomers()
        {
            var customers = _repo.GetAllCustomers();
            return View(customers);
        }

        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        public IActionResult UpdateCustomer(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateCustomers(customer);
                return RedirectToAction(nameof(CrudCustomers));
            }
            return View(customer);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _repo.GetCustomersById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _repo.DeleteCustomers(id);
            return RedirectToAction(nameof(CrudCustomers));
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult AddCustomer(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertCustomers(customer);
                return RedirectToAction(nameof(CrudCustomers));
            }

            // If ModelState is not valid, return the form with validation errors
            return View(customer);
        }
    }
}
