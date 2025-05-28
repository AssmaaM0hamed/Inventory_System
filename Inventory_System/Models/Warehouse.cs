using InventorySystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Models
{
    public class Warehouse
    {
        public int Id {  get; set; }
        //-----------------------------------
        [Required]
        public string Name { get; set; }
        //-----------------------------------
        [Required]
        public string Location { get; set; }
        //-----------------------------------
        [Required]
        [ForeignKey("Keeper")]
        public int Mgr_Id { get; set; }
        public Warehouse_Keeper Keeper { get; set; }
        //-----------------------------------

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<SupplyRequest> SupplyRequests { get; set; }
        public ICollection<SellRequest> SellRequests {get; set;}
        public ICollection<Transfer> TransfersFrom { get; set; }
        public ICollection<Transfer> TransfersTo { get; set; }
        //-----------------------------------
    }
}
