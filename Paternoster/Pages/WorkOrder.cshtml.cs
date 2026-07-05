using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages;

[Authorize(Roles = "Administrator, Manufacturing")]
public class WorkOrderModel : PageModel
{
    private readonly PaternosterDbContext _context;

    public Customer OrderCustomer { get; set; }
    public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    public List<Product> Products { get; set; } = new List<Product>();
    public List<Part> RequiredParts{ get; set; } = new List<Part>();

    public List<PaternosterContainer> Containers { get; set; } = new List<PaternosterContainer>();


    public List<Part> Parts { get; set; } = new List<Part>();
    public List<Part> MissingParts { get; set; } = new List<Part>();

    public bool IsFinishable { get; set; } = true;

    public WorkOrderModel(PaternosterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Order Order { get; set; }

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
        OrderCustomer = (Customer) await _context.Customers.FirstOrDefaultAsync(c => c.Id == Order.CustomerId);

        OrderLines.AddRange(_context.OrderLines.ToList().Where(ol => ol.OrderId == order.Id));

        foreach(OrderLine orderLine in OrderLines) 
        {
            Products.AddRange(_context.Products.ToList().Where(p => p.Id == orderLine.ProductId));
        }

        List<ProductPart> partsNeeded = new List<ProductPart>();
        foreach(Product product in Products)
        {
            partsNeeded.AddRange(_context.ProductParts.ToList().Where(p => p.ProductId == product.Id));
        }

        foreach(ProductPart productPart in partsNeeded)
        {
            Parts.AddRange(_context.Parts.ToList().Where(p => p.Id == productPart.PartId));
        }

        foreach(Part part in Parts)
        {
            int requiredAmount = RequiredParts.Count(p => p.Id == part.Id);
            PaternosterContainer container = await _context.PaternosterContainers.FirstOrDefaultAsync(c => c.PartId == part.Id);
            int availableAmount = container.PartAmount;

            if (availableAmount > requiredAmount)
            {
                continue;
            }
            else
            {
                IsFinishable = false;
                MissingParts.Add(part);
            }
        }

        MissingParts.DistinctBy(p => p.Id);

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        List<PaternosterContainer> usedContainers = new List<PaternosterContainer>();

        foreach (Product createdProduct in Products)
        {
            foreach (ProductPart productPart in createdProduct.ProductParts)
            if (usedContainers.Contains(productPart.Part.Container) == false)
            {
                productPart.Part.Container.PartAmount -= productPart.PartAmount;
                usedContainers.Add(productPart.Part.Container);
            }
            else
            {
                PaternosterContainer containerUsed = usedContainers.FirstOrDefault(c => c.Id == productPart.Part.ContainerId);
                containerUsed.PartAmount -= productPart.PartAmount;
            }
        }

        foreach(PaternosterContainer container in usedContainers)
        {
            _context.Attach(container).State = EntityState.Modified;
        }

        Order.IsFinished = true;
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
