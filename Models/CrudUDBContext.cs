using Microsoft.EntityFrameworkCore;
using IntexII.Models;


namespace IntexII.Data
{
    public class CrudUDBContext : DbContext
    {
        public CrudUDBContext(DbContextOptions<CrudUDBContext> options)
            : base(options)
        {
        }


        public DbSet<AspNetUsers> Users { get; set; } // Add this DbSet property


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configure your entity relationships, constraints, etc. here
        }
    }
}