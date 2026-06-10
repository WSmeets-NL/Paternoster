using System.ComponentModel.DataAnnotations;

namespace Paternoster.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Password { get; set; }

        public Customer()
        {

        }
    }
}
