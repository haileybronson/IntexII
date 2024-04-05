namespace IntexII.Models;

public class EFProductRepository : IProductRepository
{  
    private ProductDBContext _context;

    public EFProductRepository(ProductDBContext temp) {
        _context = temp;
    }
    public IQueryable<Product> Products => _context.Products;
}