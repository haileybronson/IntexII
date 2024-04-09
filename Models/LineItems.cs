namespace IntexII.Models;

public partial class LineItems
{
    public int transaction_Id { get; set; }
    public int ProductId { get; set; } = null!;

    public int qty { get; set; }

    public int rating { get; set; }
}

