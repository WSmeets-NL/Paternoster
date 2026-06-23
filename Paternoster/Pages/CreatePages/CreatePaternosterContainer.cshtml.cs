using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages.CreatePages;

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

        return RedirectToPage("./Create");
    }
}
