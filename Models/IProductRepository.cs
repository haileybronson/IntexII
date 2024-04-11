using Microsoft.AspNetCore.Mvc;

namespace IntexII.Models;

public interface IProductRepository
{
    public IQueryable<Product> Products {get; }
    
    Product GetProductById(int productId);
    void SaveOrder(Orders order);
    List<OrderFraudPrediction> GetOrderFraudPredictions();
    Orders GetOrderById(int orderId);
}