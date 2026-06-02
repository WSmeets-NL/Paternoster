using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class Paternoster
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PaternosterCode { get; set; }

        [Required]
        public PaternosterSystem PaternosterSystem { get; set; }
        [Required]
        public int NumberOfContainers { get; set;  }

        public int PaternosterSystemId { get; set; }

        public bool IsFull { get; set; } = false;

        public List<PaternosterContainer> Containers { get; set; } = new List<PaternosterContainer>();

        public string? Location { get; set; }

        public Paternoster(int id, string paternosterCode, PaternosterSystem associatedPaternosterSystem)
        {
            Id = id;
            PaternosterCode = paternosterCode;
            AssociatedPaternosterSystem = associatedPaternosterSystem;
            PaternosterSystemId = associatedPaternosterSystem.Id;
        }

        public void AddContainer(PaternosterContainer container)
            {
                if (Containers.Count < NumberOfContainers)
                {
                    Containers.Add(container);
                    if (Containers.Count == NumberOfContainers)
                    {
                        IsFull = true;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Sorry, deze paternoster zit vol. Kies alstublieft een andere paternoster.");
                }
        }

        public void RemoveContainer(PaternosterContainer container)
        {
            if (Containers.Contains(container))
            {
                Containers.Remove(container);
                IsFull = false;
            }
            else
            {
                throw new InvalidOperationException("Sorry, deze container zit niet in deze paternoster.");
            }
        }

    }
}
