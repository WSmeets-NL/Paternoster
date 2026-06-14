using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages
{
    public class InventoryModel : PageModel
    {
        private readonly PaternosterDbContext _context;
        public IEnumerable<Part> Parts { get; set; } = new List<Part>();
        public IEnumerable<PaternosterContainer> Containers { get; set; } = new List<PaternosterContainer>();
        public IEnumerable<Models.Paternoster> Paternosters { get; set; } = new List<Models.Paternoster>();
        public IEnumerable<PaternosterSystem> PaternosterSystems { get; set; } = new List<PaternosterSystem>(); 

        public InventoryModel(PaternosterDbContext context)
        {
            _context = context;
        }

        public async void OnGet(int? paternosterId)
        {
            try
            {
                PaternosterSystems = _context.PaternosterSystems.ToList();
                if (paternosterId != null)
                {
                    Parts = _context.Parts.ToList().Where(p => p.Container.PaternosterId == paternosterId);
                    Containers = _context.Containers.ToList().Where(c => c.PaternosterId == paternosterId);
                    Paternosters = _context.Paternosters.ToList().Where(p => p.Id == paternosterId);

                }

                else
                {
                   Parts =  _context.Parts.ToList();
                   Containers = _context.Containers.ToList();
                   Paternosters = _context.Paternosters.ToList();
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
