using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Paternoster.DAL;
using Paternoster.Models;
using System.IO;

namespace Paternoster.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly PaternosterDbContext _context;

        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Order> OnGet(string? orderedBy, bool? inverted)
        {
            try
            {
                Orders = _context.Orders.ToList().Where(o => o.IsFinished == false);
                switch(orderedBy)
                {
                    case "customer":
                        Orders.OrderBy(o => o.Customer.Name);
                        break;

                    case "orderNumber":
                        Orders.OrderBy(o => o.OrderCode);
                        break;

                    case "NumberOfLines":
                        Orders.OrderBy(o => o.OrderLines.Count);
                        break;
                }
               
                if(inverted == true)
                {
                    Orders.Reverse();
                }
               return Orders;
            }

            catch (SqliteException ex)
            {
                Console.WriteLine("Sorry, maar ik krijg geen verbinding met de database.");
                return Orders = new List<Order>();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Sorry, maar er is iets misgegaan.");
                return Orders = new List<Order>();
            }

        }
    }
}
