

namespace Shopping.Web.Pages
{
    public class OrderListModel(IOrderingService orderingService, ILogger<OrderListModel> logger) : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var customerId = new Guid("f01d79c5-4ea6-4aa4-a3d2-047781975436");
            var response = await orderingService.GetOrderByCustomer(customerId);
            Orders = response.Orders;
            return Page();
        }
    }
}
