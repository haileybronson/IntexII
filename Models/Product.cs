namespace IntexII.Models;

public partial class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    
    public int Year { get; set; } 
    
    public int num_Parts { get; set; }
    public int Price { get; set; }
    
    public string img_Link { get; set; }
    
    public string primary_Color { get; set; }
    
    public string secondary_Color { get; set; }
    
    public string Description { get; set; }
    public string Category { get; set; } = null!;
    
}