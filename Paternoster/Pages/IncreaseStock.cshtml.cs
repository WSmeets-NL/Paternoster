using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paternoster.DAL;
using Paternoster.Models;


namespace Paternoster.Pages
{
    public class IncreaseStockModel : PageModel
    {
        private readonly PaternosterDbContext _context;
        [BindProperty]
        public Part ChosenPart { get; set; }
        
        public PaternosterContainer Container { get; set; }


        public IncreaseStockModel(PaternosterDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                RedirectToPage("/Inventory");
            }
            ChosenPart = await _context.Parts.FindAsync(id);

            Container = _context.PaternosterContainers.FirstOrDefault(c => c.PartId == id);

            if(Container == null)
            {
                return RedirectToPage("/Inventory");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int addedAmount, int id)
        {
            Container = _context.PaternosterContainers.FirstOrDefault(c => c.PartId == id);
            Container.PartAmount = Container.PartAmount + addedAmount;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Inventory");
        }
    }
}
