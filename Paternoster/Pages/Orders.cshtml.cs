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

        public List<Order> Orders { get; set; } = new List<Order>();
         
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Product> Products { get; set; } = new List<Product>();

        public OrdersModel(PaternosterDbContext context)
        {
            _context = context;
        }   
        public async void OnGetAsync(string? orderedBy, bool? inverted, int? customerId)
        {
            try
            {
                if (customerId != null)
                {
                    Orders.AddRange(_context.Orders.ToList().Where(o => o.CustomerId == customerId && o.IsFinished == false));
                }
                else 
                {
                    Orders.AddRange(_context.Orders.ToList().Where(o => o.IsFinished == false));
                }

                foreach(Order order in Orders)
                {
                    var LinesInOrder = _context.OrderLines.ToList().Where(ol => ol.OrderId == order.Id);
                    OrderLines.AddRange(LinesInOrder);

                    Customers.AddRange(_context.Customers.ToList().Where(c => c.Id == order.CustomerId));
                }

                foreach(OrderLine orderLine in OrderLines)
                {
                    Products.AddRange(_context.Products.ToList().Where(p => p.Id == orderLine.ProductId));
                }

                Customers = Customers.DistinctBy(c => c.Name).ToList();
                Products = Products.DistinctBy(p => p.Name).ToList();
            
                switch (orderedBy)
                {
                    case "Customer":
                        Orders = Orders.OrderBy(o => o.Customer.Name).ToList();
                        break;

                    case "OrderNumber":
                        Orders = Orders.OrderBy(o => o.OrderCode).ToList();
                        break;

                    case "OrderLines":
                        Orders = Orders.OrderBy(o => o.OrderLines.Count).ToList();
                        break;
                }
               
                if(inverted == true)
                {
                    Orders.Reverse();
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
