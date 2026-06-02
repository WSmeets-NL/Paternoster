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

        public ProductPart(int id, ProductPart part, Product product)
        {
            Id = id;
            Part = part;
            PartId = Part.Id;
            Product = product;
            ProductId = product.Id;
        }
    }
}
