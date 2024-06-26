using Microsoft.AspNetCore.Mvc;

namespace IntexII.Models;

public interface IProductRepository
{
    public IQueryable<Product> Products {get; }
    
    IEnumerable<string> GetRecommendationIdsByProductId(int productId);
    
    Product GetProductById(int productId);
    void SaveOrder(Orders order);
    (IEnumerable<OrderFraudPrediction>, int totalCount) GetOrderFraudPredictions(int page, int pageSize);
    Orders GetOrderById(int orderId);
}