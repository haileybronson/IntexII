using Microsoft.EntityFrameworkCore;

namespace IntexII.Models;

public partial class CustomersDBContext : DbContext
{
    public CustomersDBContext(DbContextOptions<CustomersDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customers> Customers { get; set; }
}
