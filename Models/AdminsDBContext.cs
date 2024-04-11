using Microsoft.EntityFrameworkCore;

namespace IntexII.Models;

public partial class AdminsDBContext : DbContext
{
    public AdminsDBContext(DbContextOptions<AdminsDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admins> Admins { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("ProductConnection");
            // Replace "YourProductConnectionConnectionString" with your actual connection string
        }
    }
}

