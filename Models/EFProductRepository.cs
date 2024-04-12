using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace IntexII.Models;

public class EFProductRepository : IProductRepository
{  
    //stuff with productDB
    private readonly ProductDBContext _context;
    private readonly InferenceSession _session;
    public EFProductRepository(ProductDBContext temp, InferenceSession session) {
        _context = temp;
        _session = session;
    }
    
    public IEnumerable<string> GetRecommendationIdsByProductId(int productId)
    {
        var productRecommendations = _context.ProductRecommendations
            .Where(pr => pr.ProductId == productId)
            .ToList();
        
        var recommendations = productRecommendations
            .SelectMany(pr => new List<string>
            {
                pr.recommendation_1,
                pr.recommendation_2,
                pr.recommendation_3,
                pr.recommendation_4,
                pr.recommendation_5
            })
            .ToList();

        return recommendations;
    }
    
    public void SaveOrder(Orders order)
    {
        // Assuming transaction_Id and customer_Id need manual setting
        var latestTransactionId = _context.Orders.Max(o => (int?)o.transaction_Id) ?? 0;
        var latestCustomerId = _context.Orders.Max(o => (int?)o.customer_Id) ?? 0;
        
        order.transaction_Id = latestTransactionId + 1;
        order.customer_Id = latestCustomerId + 1; // Adjust logic if customer_Id increments differently
        
        _context.Orders.Add(order);
        _context.SaveChanges(); //push to sql DB
    }
    
    public (IEnumerable<OrderFraudPrediction>, int totalCount) GetOrderFraudPredictions(int page, int pageSize)
   {
       int totalOrders = _context.Orders.Count();//total count for pagination
       var records = _context.Orders
           .OrderByDescending(o => o.transaction_Id)
           .Take(500)
           .ToList();  // Fetch 20 records first
      
       var predictions = new List<OrderFraudPrediction>();  // Your ViewModel for the view


       // Dictionary mapping the numeric prediction to an animal type
       var class_type_dict = new Dictionary<int, string>
       {
           { 0, "Not Fraud" },
           { 1, "Fraud" }


       };


       foreach (var record in records)
       {
           var input = new List<float>
           {


               (float)record.customer_Id,
               (float)record.time,
               (float)(record.amount ?? 0),


               record.day_of_week == "Fri" ? 1 : 0,
               record.day_of_week == "Mon" ? 1 : 0,
               record.day_of_week == "Sat" ? 1 : 0,
               record.day_of_week == "Sun" ? 1 : 0,
               record.day_of_week == "Thu" ? 1 : 0,
               record.day_of_week == "Tue" ? 1 : 0,
               record.day_of_week == "Wed" ? 1 : 0,


               record.entry_mode == "CVC" ? 1 : 0,
               record.entry_mode == "PIN" ? 1 : 0,
               record.entry_mode == "Tap" ? 1 : 0,


               record.type_of_transaction == "ATM" ? 1 : 0,
               record.type_of_transaction == "Online" ? 1 : 0,
               record.type_of_transaction == "POS" ? 1 : 0,


               record.country_of_transaction == "China" ? 1 : 0,
               record.country_of_transaction == "India" ? 1 : 0,
               record.country_of_transaction == "Russia" ? 1 : 0,
               record.country_of_transaction == "USA" ? 1 : 0,
               record.country_of_transaction == "United Kingdom" ? 1 : 0,


               record.bank == "Barclays" ? 1 : 0,
               record.bank == "HSBC" ? 1 : 0,
               record.bank == "Halifax" ? 1 : 0,
               record.bank == "Lloyds" ? 1 : 0,
               record.bank == "Metro" ? 1 : 0,
               record.bank == "Monzo" ? 1 : 0,
               record.bank == "RBS" ? 1 : 0,


               record.type_of_card == "MasterCard" ? 1 : 0,
               record.type_of_card == "Visa" ? 1 : 0,


           };
           var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });


           var inputs = new List<NamedOnnxValue>
           {
               NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
           };


           string predictionResult;
           using (var results = _session.Run(inputs))
           {
               var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
               predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
           }


           predictions.Add(new OrderFraudPrediction { Orders = record, Prediction = predictionResult }); // Adds the animal information and prediction for that animal to AnimalPrediction viewmodel
       }
       return (predictions, totalOrders);
       //return (predictions, totalOrders);
   }
    
    public IQueryable<Product> Products => _context.Products;
    
    public Product GetProductById(int productId)
    {
        return _context.Products.FirstOrDefault(p => p.ProductId == productId);
    }
    
    public Orders GetOrderById(int orderId)
    {
        return _context.Orders.FirstOrDefault(o => o.transaction_Id == orderId);
    }
}