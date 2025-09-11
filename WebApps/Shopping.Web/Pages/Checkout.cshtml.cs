using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class CheckoutModel( IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default;
        public ShoppingCartModel Cart { get; set; } = default!;
        public async Task<IActionResult> OnGeAsync()
        {
            Cart = await basketService.LoadUserBasket();
            return Page();

        }
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            logger.LogInformation("Checkout Page visited");
            Cart = await basketService.LoadUserBasket();
            if (!ModelState.IsValid)
            {
               
                return Page();
            }
            Order.CustomerId = new Guid("f59034c8-e5bd-4e1f-b2d4-ce01246bf49e");
            Order.UserName = Cart.UserName;
            Order.TotalPrice = Cart.TotalPrice;
            await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
           /// await basketService.DeleteBasket(Cart.UserName);
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
