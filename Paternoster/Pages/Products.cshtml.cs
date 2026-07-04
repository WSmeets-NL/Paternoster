using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Paternoster.DAL;
using Paternoster.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.Eventing.Reader;

namespace Paternoster.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly PaternosterDbContext _context;

        public IWebHostEnvironment _environment { get; }

        public IEnumerable<Product> Products { get; set; } 

        public List<ProductPart> ProductParts { get; set; } = new List<ProductPart>();

        public List<string> Affiliations { get; set; } = new List<string>();

        public List<Part> Parts { get; set; } = new List<Part>();

        public ProductsModel(PaternosterDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async void OnGetAsync(string? name, string? orderByName, string? affiliation)
        {
            try
            {
                if (affiliation != null)
                {
                    if (name != null)
                    {
                        Products = _context.Products.ToList().Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase) && p.Affiliation == affiliation);
                    }
                    else
                    {
                        Products = _context.Products.ToList().Where(p => p.Affiliation == affiliation);
                    }
                }
                else if (name != null)
                {
                    Products = _context.Products.ToList().Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    Products = _context.Products.ToList();
                }


            foreach (Product product in Products)
                {
                    ProductParts.AddRange(_context.ProductParts.ToList().Where(pp => pp.ProductId == product.Id));
                    Affiliations.Add(product.Affiliation);
                }

            Affiliations = Affiliations.Distinct().ToList();

            foreach (ProductPart productPart in ProductParts)
                {
                    Parts.AddRange(_context.Parts.ToList().Where(p => p.Id == productPart.PartId));
                }

            if (orderByName == "on")
                {
                    Products = Products.OrderBy(p => p.Name).ToList();
                }



                
            }
            catch (SqliteException ex)
            {
                Console.WriteLine("Sorry, maar ik krijg geen verbinding met de database.");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Sorry, maar er is iets misgegaan.");
            }
        }
    }
}
