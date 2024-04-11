using Microsoft.EntityFrameworkCore;

namespace IntexII.Models;

public partial class CustomersDBContext : DbContext
{
    public CustomersDBContext(DbContextOptions<CustomersDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customers> Customers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("ProductConnection");
            // Replace "YourProductConnectionConnectionString" with your actual connection string
        }
    }
}
