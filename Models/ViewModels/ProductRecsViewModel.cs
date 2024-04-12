namespace IntexII.Models.ViewModels;


public class ProductRecsViewModel
{
    public Product Product { get; set; }
    public List<string> RecommendationUrls { get; set; } // This should handle URLs as strings.
}