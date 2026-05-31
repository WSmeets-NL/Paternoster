using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class Part
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string PartCode { get; set; }

        [Required]
        public PaternosterContainer AssignedContainer { get; set; }

        public int ContainerId { get; set; }

        public List<ProductPart> UsedInProducts { get; set; } = new List<ProductPart>();

        public Part(int id, string partCode, PaternosterContainer assignedContainer)
        {
            Id = id;
            PartCode = partCode;
            AssignedContainer = assignedContainer;
            ContainerId = assignedContainer.Id;
        }
    }
}
