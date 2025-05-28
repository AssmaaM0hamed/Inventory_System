using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-0DBHGF8\\SQLEXPRESS;Database=InventoryDB;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SupplyRequest> SupplyRequests { get; set; }
        public DbSet<SellRequest> SellRequests { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Warehouse_Keeper> Warehouse_Keepers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>()
                 .HasOne(t => t.FromWarehouse)
                 .WithMany(w => w.TransfersFrom)
                 .HasForeignKey(t => t.FromWH_Id)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.ToWarehouse)
                .WithMany(w => w.TransfersTo)
                .HasForeignKey(t => t.ToWH_Id)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
