using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class OrderLine
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Product Product { get; set; }

        public int ProductId { get; set; }

        [Required]
        public Order Order { get; set; }

        public int OrderId { get; set; }

        [Required]
        public int ProductAmount { get; set; }

        public OrderLine(int id, Product productInOrder, int productAmount, Order inOrder)
        {
            Id = id;
            ProductInOrder = productInOrder;
            ProductId = ProductInOrder.Id;
            ProductAmount = productAmount;
            InOrder = inOrder;
            OrderId = inOrder.Id;

        }
    }
}
