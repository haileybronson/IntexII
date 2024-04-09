using System.ComponentModel.DataAnnotations;

namespace IntexII.Models;

public partial class LineItems
{
    [Key]
    public int transaction_Id { get; set; }
    
    public int ProductId { get; set; }

    public int qty { get; set; }

    public int rating { get; set; }
}

