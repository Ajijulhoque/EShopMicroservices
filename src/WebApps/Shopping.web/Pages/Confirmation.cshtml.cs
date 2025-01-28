using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;

        public void OnGetContact()
        {
            Message = "Email sent";
        }

        public void OnGetOrderSubmitted()
        {
            Message = "Order submitted successfully";
        }
    }
}
