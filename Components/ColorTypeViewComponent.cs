using Microsoft.AspNetCore.Mvc;
using IntexII.Models;

namespace IntexII.Components;

public class ColorTypeViewComponent : ViewComponent
{
    private IProductRepository _colorRepo;
    
    public ColorTypeViewComponent(IProductRepository temp)
    {
        _colorRepo = temp;
    }
    
    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedColorType = RouteData?.Values["colorType"];
    
        var colorTypes = _colorRepo.Products
            .Select(x => x.primary_Color)
            .Distinct()
            .OrderBy(x => x);
        return View(colorTypes);
    }
}
