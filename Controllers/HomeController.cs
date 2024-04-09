using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using IntexII.Models.ViewModels;

namespace IntexII.Controllers;

public class HomeController : Controller
{
    private IProductRepository _repo;
    
    public HomeController(IProductRepository temp)
    {
        _repo = temp;
    }

    //public IActionResult Index(int pageNum, string? productType)
    public ViewResult Index(string? productType, int pageNum =1)
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
