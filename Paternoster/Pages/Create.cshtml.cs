using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages
{
    [Authorize(Roles = "Administrator, Sales, Inventory")]
    public class CreateModel : PageModel
    {

        public IEnumerable<string> PossibleItemsToCreate { get; } = ["Paternostersysteem", "Paternoster", "Paternostercontainer", "Onderdeel", "Product"];

        public IEnumerable<string> InventoryItemsToCreate { get; } = ["Paternostersysteem", "Paternoster", "Paternostercontainer", "Onderdeel"];

        public IEnumerable<string> SalesItemsToCreate { get; } = ["Product"];

        public CreateModel() { }

        public IActionResult OnGet(string? itemToCreate)
        {
            if (PossibleItemsToCreate.Contains(itemToCreate) != true)
            {
                return Page();
            }

            if ((User.IsInRole("Administrator")) || 
               (User.IsInRole("Sales") && SalesItemsToCreate.Contains(itemToCreate)) || 
               (User.IsInRole("Inventory") && InventoryItemsToCreate.Contains(itemToCreate)))
            {
                switch (itemToCreate)
                {
                    case "Paternostersysteem":
                        {
                            return RedirectToPage("/CreatePages/CreatePaternosterSystem");
                        }
                    case "Paternoster":
                        {
                            return RedirectToPage("/CreatePages/CreatePaternoster");
                        }
                    case "Paternostercontainer":
                        {
                            return RedirectToPage("/CreatePages/CreatePaternosterContainer");
                        }
                    case "Product":
                        {
                            return RedirectToPage("/CreatePages/CreateProduct");
                        }
                    case "Onderdeel":
                        {
                            return RedirectToPage("/CreatePages/CreatePart");
                        }
                    default:
                        {
                            return Page();
                        }
                }
            }
            else
            {
                return Page();
            }
        }
    }
}   

