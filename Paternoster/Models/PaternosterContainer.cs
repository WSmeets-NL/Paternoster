using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class PaternosterContainer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ContainerCode { get; set; }

        public Part? Part { get; set; }

        public int? PartId { get; set; }

        public Paternoster Paternoster { get; set; }

        [Required]
        public int PaternosterId { get; set; }

        public int PartAmount { get; set; }


    }

}
