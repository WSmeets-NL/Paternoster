using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Paternoster.DAL;
using Paternoster.Models;
using Microsoft.AspNetCore.Hosting;

namespace Paternoster.Pages.CreatePages;

[Authorize(Roles = "Administrator, Inventory")]
public class CreatePartModel : PageModel
{
    private readonly PaternosterDbContext _context;
    private IWebHostEnvironment _environment;

    public CreatePartModel(PaternosterDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public List<PaternosterContainer> PaternosterContainers { get; set; }

    public IActionResult OnGet()
    {
        PaternosterContainers = _context.PaternosterContainers.Where(c => c.PartId == null).ToList();
        return Page();
    }

    [BindProperty]
    public Part Part { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        _context.Parts.Add(Part);
        await _context.SaveChangesAsync();


        if (Part.PartImage != null)
        {
            string imageName = Part.PartCode + Path.GetExtension(Part.PartImage.FileName);
            var imageFile = Path.Combine(_environment.WebRootPath, "Images", "ProductImages", imageName);
            using (var filestream = new FileStream(imageFile, FileMode.Create))
            {
                await Part.PartImage.CopyToAsync(filestream);
            }
        }

        PaternosterContainer paternosterContainer = (PaternosterContainer) _context.PaternosterContainers.Where(c => c.Id == Part.ContainerId).FirstOrDefault();
        if (paternosterContainer == null)
        {
            return NotFound();
        }

        paternosterContainer.PartId = Part.Id;

        await _context.SaveChangesAsync();

        return RedirectToPage($"/FigurinePart?Name={Part.Name}");
    }
}
