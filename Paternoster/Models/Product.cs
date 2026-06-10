using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Paternoster.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductCode { get; set; }

        public List<OrderLine> Orders { get; set; } = new List<OrderLine>();

        public List<ProductPart> ProductParts { get; set; } = new List<ProductPart>();

    }
}
