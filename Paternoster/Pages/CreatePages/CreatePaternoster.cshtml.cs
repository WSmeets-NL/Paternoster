using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages.CreatePages;

[Authorize(Roles = "Administrator, Inventory")]
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

        _context.Paternosters.Add(Paternoster);
        await _context.SaveChangesAsync();

        int containerNumber = 1;
        List<PaternosterContainer> newContainers = new List<PaternosterContainer>();
        int containerCodeLength = Paternoster.NumberOfContainers.ToString().Length;
    
        while(containerNumber <= Paternoster.NumberOfContainers)
        {
            newContainers.Add(new PaternosterContainer
            {
                Id = 0,
                ContainerCode = ($"{Paternoster.PaternosterCode}_" + containerNumber.ToString($"D{containerCodeLength}")),
                PartAmount = 0,
                PaternosterId = Paternoster.Id
            });

            containerNumber++;
        }
        _context.PaternosterContainers.AddRange(newContainers);
        await _context.SaveChangesAsync();

        return RedirectToPage("/ItemAdded", new { itemType = "Paternoster"});
    }
}