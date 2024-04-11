using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using IntexII.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace IntexII.Controllers
{
   public class BrianController : Controller
   {
       private readonly ICrudRepository<Product> _repo;


       public BrianController(ICrudRepository<Product> temp)
       {
           _repo = temp;
       }


       [Authorize]
       public IActionResult CrudProducts()
       {
           var products = _repo.GetAll();
           return View(products);
       }
      
       [Authorize]
       public IActionResult UpdateProduct(int id)
       {
           var product = _repo.GetById(id);
           if (product == null)
           {
               return NotFound();
           }
           return View(product);
       }
      
       [HttpPost]
       [Authorize]
       public IActionResult UpdateProduct(Product product)
       {
           if (ModelState.IsValid)
           {
               _repo.Update(product);
               return RedirectToAction(nameof(CrudProducts));
           }
           return View(product);
       }
      
       [HttpPost]
       [Authorize]
       public IActionResult DeleteProduct(int id)
       {
           var product = _repo.GetById(id);
           if (product == null)
           {
               return NotFound();
           }


           _repo.Delete(id);
           return RedirectToAction(nameof(CrudProducts));
       }
      
       public IActionResult AddProduct()
       {
           return View();
       }
      
       [HttpPost]
       [Authorize]
       public IActionResult AddProduct(Product product)
       {
           if (ModelState.IsValid)
           {
               _repo.Insert(product);
               return RedirectToAction(nameof(CrudProducts));
           }


           // If ModelState is not valid, return the form with validation errors
           return View(product);
       }
   }
}
