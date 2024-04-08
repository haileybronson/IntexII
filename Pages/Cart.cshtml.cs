using IntexII.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using IntexII.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntexII.Pages;

public class CartModel : PageModel
{
    private IProductRepository _repo;

    public Cart Cart { get; set; }
    
    public CartModel(IProductRepository temp, Cart cartService)
    {
        _repo = temp;
        Cart = cartService;
    }
    public string ReturnUrl { get; set; } = "/";
    
    //getting info from json that will then be used to add cart upon itself 
    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
        //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
    }

    //taking the products chosen and will calculate adding the item 
    public IActionResult OnPost(int productId, string returnUrl)
    {
        Product prod = _repo.Products
            .FirstOrDefault(x => x.ProductId == productId);

        //checking to see if cart is not empty, and will add item to existing cart 
        if (prod != null)
        {
            //comment out, because this is handled in the subclass SessionCart
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddItem(prod, 1);
            //HttpContext.Session.SetJson("cart", Cart);
        }

        //goes back to where user was previously when going back to home 
        return RedirectToPage (new { returnUrl = returnUrl });
    }

    public IActionResult OnPostRemove(int productId, string returnUrl)
    {
        Cart.RemoveLine(Cart.Lines.First(x=> x.Product.ProductId == productId).Product);

        return RedirectToPage(new { returnUrl = returnUrl });
    }
    
}