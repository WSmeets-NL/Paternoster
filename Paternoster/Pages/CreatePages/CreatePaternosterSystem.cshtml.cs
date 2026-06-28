using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages.CreatePages;

[Authorize(Roles = "Administrator, Inventory")]
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
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }


        _context.PaternosterSystems.Add(PaternosterSystem);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Inventory");
    }
}
