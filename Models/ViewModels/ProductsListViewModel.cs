namespace IntexII.Models.ViewModels;

public class ProductsListViewModel
{
    public IQueryable<Product> Products {get; set; }

    public PaginationInfo PaginationInfo {get; set; } = new PaginationInfo();
    
    public string? CurrentProductType { get; set; }
    public string? CurrentColorType { get; set; }
    public int[] AvailablePageSizes => new[] { 5, 10, 20 };
    
    public int SelectedPageSize { get; set; }
    
    
}