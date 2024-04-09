namespace IntexII.Models;

public partial class Users
{
    public int user_ID { get; set; }
    public int customer_Id { get; set; } = null!;

    public int admin_Id { get; set; }

    public string user_Name { get; set; }
    public string user_Password { get; set; }

    public int user_Role { get; set; }
}
