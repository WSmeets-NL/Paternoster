using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages.DeletePages;

[Authorize(Roles = "Administrator, Inventory")]
public class DeletePartModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public PaternosterContainer PartContainer { get; set; }
    public List<ProductPart> ProductParts { get; set; } = new List<ProductPart>();

    public DeletePartModel(PaternosterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Part Part { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var part = await _context.Parts.FirstOrDefaultAsync(m => m.Id == id);
        if (part is null)
        {
            return NotFound();
        }
        else
        {
            Part = part;
            PartContainer = await _context.PaternosterContainers.Where(c => c.PartId == part.Id).FirstOrDefaultAsync();
            ProductParts.AddRange(_context.ProductParts.ToList().Where(pp => pp.PartId == Part.Id));
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var part = await _context.Parts.FindAsync(id);
        if (part != null)
        {
            
            Part = part;
            _context.Parts.Remove(Part);
            PartContainer.PartAmount = 0;
            PartContainer.PartId = null;
            foreach(ProductPart productPart in ProductParts)
            {
                _context.ProductParts.Remove(productPart);
            }
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
