using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages
{
    public class CreateModel : PageModel
    {

        public IEnumerable<string> PossibleItemsToCreate { get; } = ["paternostersysteem", "paternoster", "paternostercontainer", "onderdeel", "product"];

        public CreateModel() { }

        public IActionResult OnGet(string? itemToCreate)
        {
            if (PossibleItemsToCreate.Contains(itemToCreate) != true)
            {
                return Page();
            }

            else
            {
                switch (itemToCreate)
                {
                    case "paternostersysteem":
                        {
                           return RedirectToPage("./CreatePages/CreatePaternosterSystem");
                        }

                    case "paternoster":
                        {
                            return RedirectToPage("./CreatePages/CreatePaternoster");
                        }

                    case "paternostercontainer":
                        {
                            return RedirectToPage("./CreatePages//CreatePaternosterContainer");
                        }
                    case "onderdeel":
                        {
                            return RedirectToPage("./CreatePages//CreatePart");
                        }

                    case "product":
                        {
                            return RedirectToPage("./CreatePages//CreateProduct");
                        }

                    default:
                        {
                            return Page();
                        }

                }
            }
        }
    }   
}
