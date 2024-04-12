using Microsoft.EntityFrameworkCore;

namespace IntexII.Models;

public partial class OrdersDBContext : DbContext
{
    public OrdersDBContext(DbContextOptions<OrdersDBContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("ProductConnection");
            // Replace "YourProductConnectionConnectionString" with your actual connection string
        }
    }
}