using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalogService,IBasketService basketService,ILogger<ProductListModel>logger) : PageModel
    {
        public  IEnumerable<ProductModel>ProductList { get; set; }=[];
        public  IEnumerable<string>CategoryList { get; set; }=[];
        [BindProperty(SupportsGet = true)] 
        public string SelectedCategory { get; set; } = string.Empty;    

        public async Task<IActionResult> OnGetAsync(string CategoryName)
        {
            var response = await catalogService.GetProducts();
            CategoryList = response.Products.SelectMany(x => x.Category).Distinct();
            if(!string.IsNullOrEmpty(CategoryName))
            {
                ProductList = response.Products.Where(x => x.Category.Contains(CategoryName));
                SelectedCategory = CategoryName;    

            }
            else
            {
                ProductList = response.Products;
            }
            return Page();
        }

        public async Task<IActionResult>OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to Cart Button clicked");
            var productResponse = await catalogService.GetProduct(productId);
            var basket = await basketService.LoadUserBasket();
            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });
            await basketService.StoreBasket(new StoreBasketRequest(basket));
            return RedirectToPage("Cart");
        }
    }
}
