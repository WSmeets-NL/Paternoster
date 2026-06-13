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
        public IEnumerable<Part> OnGet(string? name)
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
                return Parts;

            }
            catch (SqliteException ex)
            {
                Console.WriteLine("Sorry, maar ik krijg geen verbinding met de database.");
                return Parts = new List<Part>();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Sorry, maar er is iets misgegaan.");
                return Parts = new List<Part>();
            }
        }
    }
}
