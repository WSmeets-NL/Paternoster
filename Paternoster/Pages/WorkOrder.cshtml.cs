using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages;

public class WorkOrderModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    public List<Product> Products { get; set; } = new List<Product>();
    public List<ProductPart> ProductParts{ get; set; } = new List<ProductPart>();

    public List<Part> Parts { get; set; } = new List<Part>();

    public bool IsFinishable { get; set; }

    public WorkOrderModel(PaternosterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Order Order { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {

        if (id is null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
        if (order is null)
        {
            return NotFound();
        }

        Order = order;
        OrderLines.AddRange(_context.OrderLines.ToList().Where(ol => ol.OrderId == order.Id));

        foreach(OrderLine orderLine in OrderLines) 
        {
            Products.AddRange(_context.Products.ToList().Where(p => p.Id == orderLine.ProductId));
        }

        foreach(Product product in Products)
        {
           ProductParts.AddRange(_context.ProductParts.ToList().Where(pp => pp.ProductId == product.Id));
        }

        foreach(ProductPart productPart in ProductParts)
        {
            Parts.AddRange(_context.Parts.ToList().Where(p => p.Id == productPart.PartId));
            Parts.OrderBy(p => p.Id);
        }
    

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(Order.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }
}
