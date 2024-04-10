using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using IntexII.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IntexII.Controllers;

public class BrianController : Controller
{
    private IProductRepository _repo;
    
    public BrianController(IProductRepository temp)
    {
        _repo = temp;
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
}