using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using IntexII.Models.ViewModels;
using Microsoft.ML.OnnxRuntime;

namespace IntexII.Controllers;


public class HomeController : Controller
{
    private IProductRepository _repo;
    private readonly InferenceSession _session;
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(IProductRepository temp, ILogger<HomeController> logger)
    {
        _repo = temp;
        _logger = logger;
        
        // Initialize the InferenceSession here; ensure the path is correct.
        try
        {
            _session = new InferenceSession("decision_tree_model (1).onnx");
            _logger.LogInformation("ONNX model loaded successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error loading the ONNX model: {ex.Message}");
        }
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
    

    public IActionResult ReviewOrders(int pageNum = 1, int pageSize = 5)
    {
        var (predictions, totalOrders) = _repo.GetOrderFraudPredictions(pageNum, pageSize);
    
        var viewModel = new OrdersListViewModel
        {
            Predictions = predictions,
            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum,
                ItemsPerPage = pageSize,
                TotalItems = totalOrders
            }
        };
    
        return View(viewModel);
    }
    
    public ViewResult Products(string? productType, string? colorType, int pageNum =1, int pageSize = 5)
    {
        var availablePageSizes = new ProductsListViewModel().AvailablePageSizes;
        
        pageSize = availablePageSizes.Contains(pageSize) ? pageSize : 5;
            
        var blah = new ProductsListViewModel()
        {
            Products = _repo.Products
                .Where(x => (productType == null || x.Category == productType) &&
                            (colorType == null || x.primary_Color == colorType))
                .OrderBy(x => x.Category)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum, 
                ItemsPerPage = pageSize, 
                TotalItems = _repo.Products
                    .Where(x => (productType == null || x.ProductName == productType) &&
                                (colorType == null || x.primary_Color == colorType))
                    .Count()
            },
            CurrentProductType = productType,
            CurrentColorType = colorType
        };
            
        return View(blah);
    }

    public IActionResult ProductDetail(int productId)
    {
        // Find the product by its ID using the repo
        var product = _repo.GetProductById(productId);

        // Check if the product exists
        if (product == null)
        {
            // Handle the case where the product is not found
            return NotFound("Product out of stock, wait til later!");
        }

        // If the product is found, pass it to the view
        return View(product);
    }
    
    // GET action for the order form
    public IActionResult CreateOrder()
    {
        var newOrder = new Orders
        {
            // Pre-populate fields, edit
            date = DateTime.Now.ToString("MM/dd/yyyy"),
            day_of_week = DateTime.Now.DayOfWeek.ToString(),
            time = DateTime.Now.Hour,
            type_of_transaction = "Online"
        };
        return View(newOrder);
    }

    // POST action for form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateOrder(Orders order)
    {
        if (ModelState.IsValid)
        {
            _repo.SaveOrder(order);
            return RedirectToAction("OrderConfirmation" , new { orderId = order.transaction_Id }); 
        }
        return View(order);
    }
    
    public IActionResult OrderConfirmation(int orderId)
    {
        var order = _repo.GetOrderById(orderId);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

        
}
