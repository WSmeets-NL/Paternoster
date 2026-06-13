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
        public IEnumerable<Product> OnGet(string? name)
        {
            try
            {
                if (name != null)
                {
                    Products = _context.Products.ToList().Where(p => p.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    Products = _context.Products.ToList();
                }
                return Products;

            }
            catch (SqliteException ex)
            {
                Console.WriteLine("Sorry, maar ik krijg geen verbinding met de database.");
                return Products = new List<Product>();
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, maar er is iets misgegaan.");
                return Products = new List<Product>();
            }
        }
    }
}
