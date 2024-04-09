using Microsoft.EntityFrameworkCore;

namespace IntexII.Models;

public partial class UsersDBContext : DbContext
{
    public UsersDBContext(DbContextOptions<UsersDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; }
}
