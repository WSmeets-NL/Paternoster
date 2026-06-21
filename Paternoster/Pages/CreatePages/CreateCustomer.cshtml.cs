using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages.CreatePages;

public class CreateCustomerModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public List<Customer> Customers { get; set; } = new List<Customer>();
    public CreateCustomerModel(PaternosterDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        Customers.AddRange(_context.Customers.ToList());
        return Page();
    }

    [BindProperty]
    public Customer Customer { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Customers.Add(Customer);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
