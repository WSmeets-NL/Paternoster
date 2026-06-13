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

        public IEnumerable<Part> OnGet(int? paternosterId)
        {
            try
            {
                if (paternosterId != null)
                {
                    Parts = _context.Parts.ToList().Where(p => p.Container.PaternosterId == paternosterId);
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
