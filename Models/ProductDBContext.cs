using Microsoft.EntityFrameworkCore;

namespace IntexII.Models;

public partial class ProductDBContext : DbContext
{
    public ProductDBContext(DbContextOptions<ProductDBContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Product> Products { get; set; }
    
    public virtual DbSet<Orders> Orders { get; set; }
}

