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
        
    }
}
