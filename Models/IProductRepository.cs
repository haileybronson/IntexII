namespace IntexII.Models;

public interface IProductRepository
{
    public IQueryable<Product> Products {get; }
}