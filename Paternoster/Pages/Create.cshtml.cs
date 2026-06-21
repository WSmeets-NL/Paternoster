using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages
{
    public class CreateModel : PageModel
    {
        private readonly PaternosterDbContext _context;

        public string ItemToCreate { get; set; }

        public IEnumerable<string> PossibleItemsToCreate { get; } = ["paternostersysteem", "paternoster", "paternostercontainer", "onderdeel", "product", "order", "klant"];
         

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public IActionResult OnGet(string? itemToCreate)
        {
            if (PossibleItemsToCreate.Contains(itemToCreate) != true)
            {
                return RedirectToPage();
            }

            else
            {
                switch (itemToCreate)
                {
                    case "paternostersysteem":
                        {
                           return RedirectToPage("./CreatePaternosterSystem");
                        }

                    case "paternoster":
                        {
                            return RedirectToPage("./CreatePaternoster");
                        }
                }
            }
    }
}}
