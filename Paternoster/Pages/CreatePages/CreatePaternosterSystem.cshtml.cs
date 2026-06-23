using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages.CreatePages;

public class CreatePaternosterSystemModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public CreatePaternosterSystemModel(PaternosterDbContext context)
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
    public async Task<IActionResult> OnPostAsync(string name)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        PaternosterSystem paternosterSystem = new PaternosterSystem()
        {
            Name = name
        };

        _context.PaternosterSystems.Add(paternosterSystem);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
