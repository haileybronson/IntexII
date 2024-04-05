using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;

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

}
