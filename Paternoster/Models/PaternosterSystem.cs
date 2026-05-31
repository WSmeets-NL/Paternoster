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

        public PaternosterSystem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddPaternoster(Paternoster paternoster)
        {
            Paternosters.Add(paternoster);
        }

        public void RemovePaternoster(Paternoster paternoster)
        {
            Paternosters.Remove(paternoster);
        }
    }
}
