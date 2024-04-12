namespace IntexII.Models.ViewModels;

public class ProductRecsViewModel
{
    public Product Product {get; set; }
    
    public string ImgLink { get; set; }
    public List<Product> Recommendations { get; set; } 
}