using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages.EditPages;

public class EditPartsModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public List<PaternosterContainer> PaternosterContainers { get; set; }

    public EditPartsModel(PaternosterDbContext context)
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
        Part = part;
        PaternosterContainers = _context.PaternosterContainers.Where(c => c.PartId == Part.Id).ToList();
        PaternosterContainers.AddRange(_context.PaternosterContainers.Where(c => c.PartId == null).ToList());
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        _context.Attach(Part).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PartExists(Part.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("/FigurinePart", new { partId = Part.Id} );
    }

    private bool PartExists(int id)
    {
        return _context.Parts.Any(e => e.Id == id);
    }
}
