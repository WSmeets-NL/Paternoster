using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages.CreatePages;

[Authorize(Roles = "Administrator, Inventory")]
public class CreatePaternosterContainerModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public List<Models.Paternoster> Paternosters { get; set; }
    public CreatePaternosterContainerModel(PaternosterDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        Paternosters = (List<Models.Paternoster>)_context.Paternosters.Where(p => p.IsFull != true).ToList();
        return Page();
    }

    [BindProperty]
    public PaternosterContainer PaternosterContainer { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        _context.PaternosterContainers.Add(PaternosterContainer);
        await _context.SaveChangesAsync();

        return RedirectToPage($"/Inventory?paternosterId={PaternosterContainer.PaternosterId}");
    }
}
