using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace Paternoster.Pages
{ 

    public class ItemAddedModel : PageModel
    {
        public string AddedItem { get; set; }
        public IEnumerable<string> PossibleItemsToCreate { get; } = ["Paternostersysteem", "Paternoster", "Onderdeel", "Product"];

        public IActionResult OnGet(string? itemType)
        {
            if (itemType != null && PossibleItemsToCreate.Contains(itemType))
            {
                AddedItem = itemType;
                return Page();
            }

            else
            {
                return RedirectToPage("/Inventory");
            }
        }
    }
}
