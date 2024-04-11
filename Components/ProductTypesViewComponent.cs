using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using IntexII.Models;

namespace IntexII.Components;

//this is what you change for the filter buttons 
public class ProductTypesViewComponent: ViewComponent
{
    private IProductRepository _productRepo;
    
    public ProductTypesViewComponent(IProductRepository temp)
    {
        _productRepo = temp;
    }

    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedProductType = RouteData?.Values["productType"];
        
        var productTypes = _productRepo.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x);
        return View(productTypes);
    }

    
}