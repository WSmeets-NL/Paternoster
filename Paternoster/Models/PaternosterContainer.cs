using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class PaternosterContainer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ContainerCode { get; set; }

        [Required]
        public Part Part { get; set; }

        public int PartId { get; set; }

        [Required]
        public Paternoster Paternoster { get; set; }

        public int PaternosterId { get; set; }

        public int PartAmount { get; set; }

        public PaternosterContainer(int id, string containerCode, Part containedPart, Paternoster associatedPaternoster)
        {
            Id = id;
            ContainerCode = containerCode;
            ContainedPart = containedPart;
            ContainedPartId = containedPart.Id;
            AssociatedPaternoster = associatedPaternoster;
            AssociatedPaternosterId = associatedPaternoster.Id;
        }

    }

}
