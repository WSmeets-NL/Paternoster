using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages.CreatePages;

[Authorize(Roles = "Administrator, Inventory")]
public class CreatePartModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public CreatePartModel(PaternosterDbContext context)
    {
        _context = context;
    }

    public List<PaternosterContainer> PaternosterContainers { get; set; }

    public IActionResult OnGet()
    {
        PaternosterContainers = _context.PaternosterContainers.Where(c => c.PartId == null).ToList();
        return Page();
    }

    [BindProperty]
    public Part Part { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        _context.Parts.Add(Part);
        await _context.SaveChangesAsync();

        PaternosterContainer paternosterContainer = (PaternosterContainer) _context.PaternosterContainers.Where(c => c.Id == Part.ContainerId).FirstOrDefault();
        if (paternosterContainer == null)
        {
            return NotFound();
        }

        paternosterContainer.PartId = Part.Id;

        await _context.SaveChangesAsync();

        return RedirectToPage($"/FigurinePart?Name={Part.Name}");
    }
}
