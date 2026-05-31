using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class ProductPart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ProductPart PartUsed { get; set; }

        public int PartId { get; set; }

        [Required]
        public Product Product { get; set; }

        public int ProductId { get; set; }

        public ProductPart(int id, ProductPart partUsed, Product product)
        {
            Id = id;
            PartUsed = partUsed;
            PartId = PartUsed.Id;
            Product = product;
            ProductId = product.Id;
        }
    }
}
