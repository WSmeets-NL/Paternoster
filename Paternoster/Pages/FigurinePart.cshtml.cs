using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages
{
    public class FigurinePartModel : PageModel
    {
        private readonly PaternosterDbContext _context;

        public List<Part> Parts { get; set; } = new List<Part>();

        public List<PaternosterContainer> Containers { get; set; } = new List<PaternosterContainer>();

        public List<Models.Paternoster> Paternosters { get; set; } = new List<Models.Paternoster>();

        public List<ProductPart> ProductParts { get; set; } = new List<ProductPart>();
         
        public List<Product> Products { get; set; } = new List<Product>();


        public FigurinePartModel(PaternosterDbContext context)
        {
            _context = context;
        }

        public async void OnGet(string? name, int? productId, int? partId)
        {
            try
            {
                if(partId != null)
                {
                    ProductParts.AddRange(_context.ProductParts.ToList().Where(pp => pp.PartId == partId));
                }
                else if(productId != null)
                {
                    ProductParts.AddRange(_context.ProductParts.ToList().Where(pp => pp.ProductId == productId));
                }
                else
                {
                    ProductParts.AddRange(_context.ProductParts.ToList());
                }


                foreach(ProductPart productPart in ProductParts)
                {

                    Products.AddRange(_context.Products.ToList().Where(p => p.Id == productPart.ProductId).DistinctBy(p => p.Id));

                    if (name != null)
                    {
                        Parts.AddRange(_context.Parts.ToList().Where(p => p.Id == productPart.PartId && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
                    }

                    else
                    {
                        Parts.AddRange(_context.Parts.ToList().Where(p => p.Id == productPart.PartId).DistinctBy(p => p.Id));
                    }
                }

                foreach(Part part in Parts)
                {
                    Containers.AddRange(_context.PaternosterContainers.ToList().Where(c => c.PartId == part.Id));
                }

                foreach(PaternosterContainer paternosterContainer in Containers)
                {
                    Paternosters.AddRange((_context.Paternosters.ToList().Where(p => p.Id == paternosterContainer.PaternosterId)));
                }

                Paternosters.DistinctBy(p => p.Id).ToList();
                Products = Products.DistinctBy(p => p.Id).ToList();
                Parts = Parts.DistinctBy(p => p.Id).ToList();

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
