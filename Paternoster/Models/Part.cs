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


        public PaternosterContainer Container { get; set; }

        [Required]
        public int ContainerId { get; set; }

        public List<ProductPart> Products { get; set; } = new List<ProductPart>();

        public IFormFile? PartImage { get; set; }

    }
}
