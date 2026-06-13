using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class Part
    {
        [Required]
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        [Required]
        public string PartCode { get; set; }

        [Required]
        public PaternosterContainer Container { get; set; }

        public int ContainerId { get; set; }

        public List<ProductPart> Products { get; set; } = new List<ProductPart>();

    }
}
