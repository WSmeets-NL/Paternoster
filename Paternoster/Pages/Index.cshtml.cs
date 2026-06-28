using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace Paternoster.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if(User.Identity.IsAuthenticated == false)
            {
                return Challenge();
            }
            else
            {
                return RedirectToPage("/Inventory");
            }
        }
    }
}
