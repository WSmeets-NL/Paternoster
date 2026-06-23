using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages.CreatePages;

public class CreatePaternosterModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public List<PaternosterSystem> PaternosterSystems { get; set; }

    public CreatePaternosterModel(PaternosterDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        PaternosterSystems = _context.PaternosterSystems.ToList();
        return Page();
    }

    [BindProperty]
    public Models.Paternoster Paternoster { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Paternosters.Add(Paternoster);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
