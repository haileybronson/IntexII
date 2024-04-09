namespace IntexII.Models;

public partial class Orders
{
    public int transaction_Id { get; set; }
    public int customer_Id { get; set; } = null!;

    public string date { get; set; }

    public string day_of_Week { get; set; }
    public int time { get; set; }

    public string entry_Mode { get; set; }
    public int amount { get; set; }
    public string type_of_Transaction { get; set; }
    public string country_of_Transaction { get; set; }
    public string shipping_Address { get; set; }
    public string bank { get; set; }
    public string type_of_Card { get; set; }
    public int fraud { get; set; }
}