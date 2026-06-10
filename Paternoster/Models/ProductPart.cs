using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class ProductPart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Part Part { get; set; }

        public int PartId { get; set; }

        [Required]
        public Product Product { get; set; }

        public int ProductId { get; set; }

    }
}
