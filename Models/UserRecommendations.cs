using System.ComponentModel.DataAnnotations;

namespace IntexII.Models;

public class UserRecommendations
{
    [Key]
    public int user_ID { get; set; }
    
    public int selected_product_ID { get; set; }

    public string? recommendation_1 { get; set; }

    public string? recommendation_2 { get; set; }

    public string? recommendation_3 { get; set; }

    public string? recommendation_4 { get; set; }

    public string? recommendation_5 { get; set; }
    
}