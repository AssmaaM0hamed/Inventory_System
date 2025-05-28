using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string Measurement_Unit { get; set; }

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<SellRequest> SellRequests { get; set; }
        public ICollection<SupplyRequest> SupplyRequests { get; set; }
        public ICollection<Transfer> ItemsTransfers { get; set; }
    }
}
