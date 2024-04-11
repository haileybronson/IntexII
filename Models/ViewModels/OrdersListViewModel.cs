namespace IntexII.Models.ViewModels;

public class OrdersListViewModel
{
    public IEnumerable<OrderFraudPrediction> Predictions { get; set; }
    
    public IQueryable<Orders> Orders {get; set; }

    public PaginationInfo PaginationInfo {get; set; } = new PaginationInfo();
    
    public string? CurrentOrderType { get; set; }
    
}