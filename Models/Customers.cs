﻿using System.ComponentModel.DataAnnotations;


namespace IntexII.Models;


public partial class Customers
{
    [Key]
    public int customer_Id { get; set; }
  
    public string first_Name { get; set; } = null!;


    public string last_Name { get; set; }


    public string birth_date { get; set; }
  
    public string country_of_Residence { get; set; }


    public string gender { get; set; }


    public decimal age { get; set; }
}