using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class PaternosterSystem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Paternoster> Paternosters { get; set; } = new List<Paternoster>();


    }
}
