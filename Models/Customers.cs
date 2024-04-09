namespace IntexII.Models;

public partial class Customers
{
    public int customer_ID { get; set; }
    
    public string first_Name { get; set; } = null!;

    public string last_Name { get; set; }

    public string birth_date { get; set; }
    
    public string country_of_Residence { get; set; }

    public string gender { get; set; }

    public float age { get; set; }
}
