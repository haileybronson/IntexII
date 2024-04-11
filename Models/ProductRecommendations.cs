using System.ComponentModel.DataAnnotations;

namespace IntexII.Models;

public class ProductRecommendations
{
    [Key]
    public int ProductId { get; set; }
    
    public string? recommendation_1 { get; set; }
    
    public string? recommendation_2 { get; set; }
    
    public string? recommendation_3 { get; set; }
    
    public string? recommendation_4 { get; set; }
    
    public string? recommendation_5 { get; set; }
    
}