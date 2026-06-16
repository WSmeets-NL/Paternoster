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

        public IEnumerable<Part> Parts { get; set; }

        public List<PaternosterContainer> Containers { get; set; } = new List<PaternosterContainer>();

        public List<Models.Paternoster> Paternosters { get; set; } = new List<Models.Paternoster>();

        public FigurinePartModel(PaternosterDbContext context)
        {
            _context = context;
        }

        public async void OnGet(string? name)
        {
            try
            {
                if(name != null)
                {
                    Parts = _context.Parts.ToList().Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    Parts = _context.Parts.ToList();
                }

            foreach(Part part in Parts)
                {
                    Containers.AddRange(_context.PaternosterContainers.ToList().Where(pc => pc.Id == part.ContainerId));
                }

            foreach(PaternosterContainer paternosterContainer in Containers)
                {
                    Paternosters.AddRange((_context.Paternosters.ToList().Where(p => p.Id == paternosterContainer.PaternosterId)));
                }

                Paternosters.Distinct();

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
