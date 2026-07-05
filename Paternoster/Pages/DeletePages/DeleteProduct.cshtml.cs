using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages.DeletePages;

[Authorize(Roles = "Administrator, Sales")]
public class DeleteProductModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public List<ProductPart> ProductParts { get; set; } = new List<ProductPart>();
    public DeleteProductModel(PaternosterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Product Product { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product is null)
        {
            return NotFound();
        }
        else
        {
            Product = product;
            ProductParts.AddRange(_context.ProductParts.ToList().Where(pp => pp.ProductId == Product.Id));
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            Product = product;
            _context.Products.Remove(Product);
            foreach(ProductPart productPart in ProductParts)
            {
                _context.ProductParts.Remove(productPart);
            }
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/Products");
    }
}
