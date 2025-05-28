using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public required string Email { get; set; }
        public string Social_Account  { get; set; }
        public ICollection<SellRequest> SellRequests { get; set; }
    }
}
