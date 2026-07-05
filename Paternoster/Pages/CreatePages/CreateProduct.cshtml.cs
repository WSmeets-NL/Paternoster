using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;
using Microsoft.AspNetCore.Hosting;

namespace Paternoster.Pages.CreatePages;

[Authorize(Roles = "Administrator, Sales")]
public class CreateProductModel : PageModel
{
    private readonly PaternosterDbContext _context;

    private IWebHostEnvironment _environment;

    public List<Part> AvailableParts { get; set; } = new List<Part>();

    public List<Part> ChosenParts { get; set; } = new List<Part>();

    public List<ProductPart> ProductParts { get; set; } = new List<ProductPart>();

    [BindProperty]
    public Product Product { get; set; } = default!;


    public CreateProductModel(PaternosterDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public IActionResult OnGet()
    {
        AvailableParts.AddRange(_context.Parts.ToList());
        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        if(Product.ProductImage != null)
        {
            string imageName = Product.ProductCode + Path.GetExtension(Product.ProductImage.FileName);
            var imageFile = Path.Combine(_environment.WebRootPath, "Images", "ProductImages", imageName);
            using(var filestream = new FileStream(imageFile, FileMode.Create)) 
            {
                await Product.ProductImage.CopyToAsync(filestream);
            }
        }    

        _context.Products.Add(Product);

        await _context.SaveChangesAsync();

        Product createdProduct = await _context.Products.Where(p => p.ProductCode == Product.ProductCode).FirstOrDefaultAsync();

        foreach(Part part in ChosenParts)
        {
            ProductPart productPart = new ProductPart()
            {
                Id = 0,
                PartId = part.Id,
                ProductId = createdProduct.Id,
                PartAmount = 1
            };
            ProductParts.Append(productPart);
        }

        _context.ProductParts.AddRange(ProductParts);
        await _context.SaveChangesAsync();


        return RedirectToPage("/Products", new { name = Product.Name});
    }
}
