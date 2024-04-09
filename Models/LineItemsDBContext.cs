using Microsoft.EntityFrameworkCore;

namespace IntexII.Models;

public partial class LineItemsDBContext : DbContext
{
    public LineItemsDBContext(DbContextOptions<LineItemsDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LineItems> LineItems { get; set; }
}


