using Microsoft.EntityFrameworkCore;
using IntexII.Models;


namespace IntexII.Data
{
    public class CrudDBContext : DbContext
    {
        public CrudDBContext(DbContextOptions<CrudDBContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Add this DbSet property


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configure your entity relationships, constraints, etc. here
        }
    }
}