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
    
    public virtual DbSet<ProductRecommendations> ProductRecommendations { get; set; }
    
    public virtual DbSet<UserRecommendations> UserRecommendations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("ProductConnection");
            // Replace "YourProductConnectionConnectionString" with your actual connection string
        }
    }
}