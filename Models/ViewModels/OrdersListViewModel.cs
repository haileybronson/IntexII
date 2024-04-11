namespace IntexII.Models.ViewModels;

public class OrdersListViewModel
{
    public IQueryable<Orders> Orders {get; set; }

    public PaginationInfo PaginationInfo {get; set; } = new PaginationInfo();
    
    public string? CurrentOrderType { get; set; }
    
}