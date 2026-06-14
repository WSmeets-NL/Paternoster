using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Paternoster.DAL;
using Paternoster.Models;

namespace Paternoster.Pages
{
    public class FigurinePartModel : PageModel
    {
        private readonly PaternosterDbContext _context;

        public IEnumerable<Part> Parts { get; set; }

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
