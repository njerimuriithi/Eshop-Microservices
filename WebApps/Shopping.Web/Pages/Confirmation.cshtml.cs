using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;
        public void OnGetContact()
        {
            Message = " Your Email was sent";
        }
        public void OnGetOrderSubmitted()
        {
            Message = " Your order was successfully submitted. You will receive an email with the order details.";
        }
    }
}
