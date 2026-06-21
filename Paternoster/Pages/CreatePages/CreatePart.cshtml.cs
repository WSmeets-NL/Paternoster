using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages.CreatePages;

public class CreatePartModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public CreatePartModel(PaternosterDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Part Part { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Parts.Add(Part);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
