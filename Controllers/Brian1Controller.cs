using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using IntexII.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

/*
namespace IntexII.Controllers
{
    public class Brian1Controller : Controller
    {
        private readonly ICrudURepository<AspNetUsers> _repo;

        public Brian1Controller(ICrudURepository<AspNetUsers> temp)
        {
            _repo = temp;
        }

        
        [Authorize]
        public IActionResult CrudUsers()
        {
            var users = _repo.GetAllUsers();
            return View(users);
        }

        [Authorize]
        public IActionResult UpdateUsers(int id)
        {
            var user = _repo.GetUsersById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateUsers(AspNetUsers user)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateUsers(user);
                return RedirectToAction(nameof(CrudUsers));
            }
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteUsers(int id)
        {
            var user = _repo.GetUsersById(id);
            if (user == null)
            {
                return NotFound();
            }

            _repo.DeleteUsers(user);
            return RedirectToAction(nameof(CrudUsers));
        }

        public IActionResult AddUsers()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUsers(AspNetUsers user)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertUsers(user);
                return RedirectToAction(nameof(CrudUsers));
            }

            // If ModelState is not valid, return the form with validation errors
            return View(user);
        }
    }
}

*/