using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages.PaternosterSystemPages;

public class CreateModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public CreateModel(PaternosterDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public PaternosterSystem PaternosterSystem { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.PaternosterSystems.Add(PaternosterSystem);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
