using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string OrderCode { get; set; }

        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public Order(int id, string orderCode)
        {
            Id = id;
            OrderCode = orderCode;
        }
    }
}
