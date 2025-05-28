using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Models
{
    public class SupplyRequest
    {
        public int Id { get; set; }
        //----------------------------
        [Required]
        [ForeignKey("Warehouse")]
        public int Warehouse_Id { get; set; }
        public Warehouse Warehouse { get; set; }
        //----------------------------
        [Required]
        [ForeignKey("Supplier")]
        public int Supplier_Id { get; set; }
        public Supplier Supplier { get; set; }
        //----------------------------
        [Required]
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public Product Product { get; set; }
        //----------------------------
        public int Quantity { get; set; }
        //----------------------------
        public DateTime Request_Date { get; set; }
        //----------------------------
        public DateTime Production_Date { get; set; }
        //----------------------------
        public DateTime Expire_Date {  get; set; }

    }
}
