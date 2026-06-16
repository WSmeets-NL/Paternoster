using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly PaternosterDbContext _context;

        public IEnumerable<Product> Products { get; set; } 

        public List<ProductPart> ProductParts { get; set; } = new List<ProductPart>();

        public List<Part> Parts { get; set; } = new List<Part>();
        public ProductsModel(PaternosterDbContext context)
        {
            _context = context;
        }
        public async void OnGetAsync(string? name)
        {
            try
            {
                if (name != null)
                {
                    Products = _context.Products.ToList().Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    Products = _context.Products.ToList();
                }

                foreach(Product product in Products) 
                {
                    ProductParts.AddRange(_context.ProductParts.ToList().Where(pp => pp.ProductId == product.Id));
                }

                foreach(ProductPart productPart in ProductParts)
                {
                    Parts.AddRange(_context.Parts.ToList().Where(p => p.Id == productPart.PartId));
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
