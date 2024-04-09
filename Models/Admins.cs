using System.ComponentModel.DataAnnotations;

namespace IntexII.Models;

public partial class Admins
{
    [Key]
    public int admin_Id { get; set; }
    
    public string first_Name { get; set; } = null!;

    public string last_Name { get; set; }

    public string birth_date { get; set; }
    
    public string country_of_Residence { get; set; }

    public string gender { get; set; }

    public float age { get; set; }
}

//testing awesomeness
// Double testing awesomenessness
