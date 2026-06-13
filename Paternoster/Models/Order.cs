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

        [Required]
        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public bool IsFinished { get; set; } = false;

        public Order()
        {

        }
    }
}
