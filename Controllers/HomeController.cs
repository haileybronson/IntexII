using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using IntexII.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IntexII.Controllers;

public class HomeController : Controller
{
    private IProductRepository _repo;
    
    public HomeController(IProductRepository temp)
    {
        _repo = temp;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult AboutUs()
    {
        return View();
    }
    
    [Authorize]
    public IActionResult CrudProducts()
    {
        return View();
    }
    
    [Authorize]
    public IActionResult CrudUsers()
    {
        return View();
    }
    
    [Authorize]
    public IActionResult ReviewOrders()
    {
        return View();
    }
    public ViewResult Products(string? productType, int pageNum =1)
    {
        int pageSize = 10;

        var blah = new ProductsListViewModel()
        {
            Products = _repo.Products
                .Where(x=> x.ProductName == productType || productType == null)
                .OrderBy(x=> x.ProductName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum, 
                ItemsPerPage = pageSize, 
                TotalItems = productType == null ? _repo.Products.Count() : _repo.Products.Where(x => x.ProductName == productType).Count()
            },
            CurrentProductType = productType
        };
            
        return View(blah);
    }

}
