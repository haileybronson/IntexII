namespace IntexII.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    //public int PageCount { get; set; }

    public double Price { get; set; }
}