using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        //-----------------------------------
        [Required]
        public int FromWH_Id { get; set; }
        public Warehouse FromWarehouse { get; set; }
        //-----------------------------------
        [Required]
        public int ToWH_Id { get; set; }
        public Warehouse ToWarehouse { get; set; }
        //-----------------------------------
        [Required]
        [ForeignKey("Supplier")]
        public int Supplier_Id { get; set; }
        public Supplier Supplier { get; set; }
        //----------------------------------
        [Required]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public Product Product { get; set; }
        //----------------------------------
        public int Quantity { get; set; }
        //----------------------------
        public DateTime Production_Date { get; set; }
        //----------------------------
        public DateTime Expire_Date { get; set; }
        public DateTime Transfer_Date { get; set; }
    }
}
