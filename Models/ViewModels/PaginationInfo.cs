namespace IntexII.Models.ViewModels;

public class PaginationInfo
{
    public int TotalItems {get; set; }
    public int ItemsPerPage {get; set; }
    public int CurrentPage {get; set; }

    // cast into decimal first to divide, then do ceiling function 
    //to cast next highest integer for pages, then cast back to int

    public int TotalNumPages => (int) (Math.Ceiling((decimal) TotalItems / ItemsPerPage));
}