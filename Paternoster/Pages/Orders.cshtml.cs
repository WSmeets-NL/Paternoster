using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paternoster.Models;
using Paternoster.DAL;

namespace Paternoster.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly PaternosterDbContext _context;

        public IEnumerable<Order> Orders { get; set; }
        public void OnGet(bool? sorted)
        {

        }
    }
}
