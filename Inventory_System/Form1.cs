using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{

    public partial class Form1 : Form
    {
        AppDbContext context;
        Button activeButton;
        public Form1()
        {
            InitializeComponent();
            context = new AppDbContext();
        }
        private void LoadWarehouses()
        {
            var warehouses = context.Warehouses
                .Join(context.Warehouse_Keepers,
                    warehouse => warehouse.Mgr_Id,
                    keeper => keeper.Id,
                    (warehouse, keeper) => new
                    {
                        Id = warehouse.Id,
                        Name = warehouse.Name,
                        Location = warehouse.Location,
                        ManagerId = warehouse.Mgr_Id,
                        Manager = keeper.Name
                    })
                .ToList();

            whData.DataSource = null;
            whData.DataSource = warehouses;
            whData.Columns["ManagerId"].Visible = false;
        }
        private void LoadSuppliers()
        {
            var suppliers = context.Suppliers.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                Fax = x.Fax,
                Account = x.Social_Account
            }).ToList();

            supplierData.DataSource = null;
            supplierData.DataSource = suppliers;
        }
        private void LoadCustomers()
        {
            var customers = context.Customers.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                Fax = x.Fax,
                Account = x.Social_Account
            }).ToList();

            customerData.DataSource = null;
            customerData.DataSource = customers;
        }
        private void LoadProducts()
        {
            var products = context.products.Select(x => new
            {
                Id = x.Id,
                Product = x.Name,
                Unit = x.Measurement_Unit
            }).ToList();

            productData.DataSource = null;
            productData.DataSource = products;
        }
        private void LoadSupplyRequests()
        {
            var Requests = context.SupplyRequests
                  .Select(r => new
                  {
                      Id = r.Id,
                      Date = r.Request_Date,
                      WhId = r.Warehouse_Id,
                      WareHouse = r.Warehouse.Name,
                      SId = r.Supplier_Id,
                      Supplier = r.Supplier.Name,
                      PId = r.Product_Id,
                      Product = r.Product.Name,
                      Quantity = r.Quantity,
                      Pro_Date = r.Production_Date,
                      Exp_Date = r.Expire_Date
                  })
                  .ToList();

            entriesData.DataSource = null;
            entriesData.DataSource = Requests;
            entriesData.Columns["WhId"].Visible = false;
            entriesData.Columns["SId"].Visible = false;
            entriesData.Columns["PId"].Visible = false;
        }
        private void LoadSellRequests()
        {
            var Requests = context.SellRequests
                  .Select(r => new
                  {
                      Id = r.Id,
                      Date = r.Release_Date,
                      WhId = r.Warehouse_Id,
                      WareHouse = r.Warehouse.Name,
                      CId = r.Customer_Id,
                      Customer = r.Customer_Id != null ? r.Customer.Name : "N/A",
                      PId = r.Product_Id,
                      Product = r.Product.Name,
                      Quantity = r.Quantity,
                  })
                  .ToList();

           
            releaseData.DataSource = null;
            releaseData.DataSource = Requests;
            releaseData.Columns["WhId"].Visible = false;
            releaseData.Columns["CId"].Visible = false;
            releaseData.Columns["PId"].Visible = false;
        }
        private void LoadTransfers()
        {
            var transfers = context.Transfers
                    .Select(t => new
                    {
                        Id = t.Id,
                        FId = t.FromWarehouse.Id,
                        From = t.FromWarehouse.Name,
                        TId = t.ToWarehouse.Id,
                        To = t.ToWarehouse.Name,
                        SId = t.Supplier.Id,
                        Suuplier = t.Supplier_Id,
                        PId = t.Product.Id,
                        Product = t.Product.Name,
                        Quantity = t.Quantity,
                        Pro_Date = t.Production_Date,
                        Exp_Date = t.Expire_Date
                    }).ToList();

            transferData.DataSource = null;
            transferData.DataSource = transfers;

            transferData.Columns["FId"].Visible = false;
            transferData.Columns["TId"].Visible = false;
            transferData.Columns["SId"].Visible = false;
            transferData.Columns["PId"].Visible = false;
        }
        private void LoadWarehousesNames()
        {
            var WhNames = context.Warehouses.Select(x => x.Name).ToList();
            foreach (var Wh in WhNames)
            {
                stockWh.Items.Add(Wh);
                whNamesList.Items.Add(Wh);
                PTWhList.Items.Add(Wh);
            }
        }
        private void LoadProductNames()
        {
            var productName = context.products.Select(x => x.Name).ToList();
            foreach (var name in productName)
            {
                productNames.Items.Add(name);
                PTProductNames.Items.Add(name);
            }
        }
        //=======================================================================================
        private void ShowPanel(Panel panelToShow)
        {
            warehousePanel.Visible = false;
            productPanel.Visible = false;
            customerPanel.Visible = false;
            supplierPanel.Visible = false;
            stockEntriesPanel.Visible = false;
            stockReleasePanel.Visible = false;
            transferPanel.Visible = false;
            reportPanel.Visible = false;

            panelToShow.Visible = true;
        }

        private void ClickButton(Button clickedButton)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = Color.Black;
                activeButton.ForeColor = Color.White;
            }
            clickedButton.BackColor = Color.White;
            clickedButton.ForeColor = Color.Black;

            activeButton = clickedButton;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void warehouseBtn_Click(object sender, EventArgs e)
        {
            ClickButton(warehouseBtn);
            ShowPanel(warehousePanel);
        }
        private void stockEntryBtn_Click(object sender, EventArgs e)
        {
            ClickButton(stockEntryBtn);
            ShowPanel(stockEntriesPanel);
        }

        private void stockReleaseBtn_Click(object sender, EventArgs e)
        {
            ClickButton(stockReleaseBtn);
            ShowPanel(stockReleasePanel);
        }

        private void productBtn_Click(object sender, EventArgs e)
        {
            ClickButton(productBtn);
            ShowPanel(productPanel);
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            ClickButton(SuppliersBtn);
            ShowPanel(supplierPanel);
        }

        private void customerBtn_Click(object sender, EventArgs e)
        {
            ClickButton(customerBtn);
            ShowPanel(customerPanel);
        }

        private void TransferBtn_Click(object sender, EventArgs e)
        {
            ClickButton(TransferBtn);
            ShowPanel(transferPanel);
        }
        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            ClickButton(ReportsBtn);
            ShowPanel(reportPanel);
        }
        //=======================================================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadWarehouses();
            LoadSuppliers();
            LoadCustomers();
            LoadProducts();
            LoadSupplyRequests();
            LoadSellRequests();
            LoadTransfers();
            LoadWarehousesNames();
            LoadProductNames();
        }
        //=======================================================================================
        //==========================================WAREHOUSE======================================
        private void whAddBtn_Click(object sender, EventArgs e)
        {
            var newWh = new Warehouse()
            {
                Name = whNameTxt.Text,
                Location = whAddressTxt.Text,
                Mgr_Id = int.Parse(whMngTxt.Text)
            };
            context.Warehouses.Add(newWh);
            context.SaveChanges();
            LoadWarehouses();
        }
        private void whData_SelectionChanged(object sender, EventArgs e)
        {
            if (whData.SelectedRows.Count > 0)
            {
                whIdTxt.Enabled = false;
                DataGridViewRow row = whData.SelectedRows[0];
                whIdTxt.Text = row.Cells["Id"].Value?.ToString() ?? "";
                whNameTxt.Text = row.Cells["Name"].Value?.ToString() ?? "";
                whAddressTxt.Text = row.Cells["Location"].Value?.ToString() ?? "";
                whMngTxt.Text = row.Cells["ManagerId"].Value?.ToString() ?? "";
            }
        }
        private void whUpdateBtn_Click(object sender, EventArgs e)
        {
            var id = int.Parse(whIdTxt.Text);
            var updatedWh = context.Warehouses.Find(id);
            if (updatedWh != null)
            {
                updatedWh.Name = whNameTxt.Text;
                updatedWh.Location = whAddressTxt.Text;
                updatedWh.Mgr_Id = int.Parse(whMngTxt.Text);

                context.SaveChanges();
                LoadWarehouses();
            }
            else
            {
                MessageBox.Show("Warehouse not found!");
            }
        }
        //=======================================================================================
        //==========================================SUPPLIERS====================================
        private void supplierData_SelectionChanged(object sender, EventArgs e)
        {
            if (supplierData.SelectedRows.Count > 0)
            {
                supplierIdTxt.Enabled = false;
                DataGridViewRow row = supplierData.SelectedRows[0];
                supplierIdTxt.Text = row.Cells["Id"].Value?.ToString() ?? "";
                supplierNameTxt.Text = row.Cells["Name"].Value?.ToString() ?? "";
                supplierPhoneTxt.Text = row.Cells["Phone"].Value?.ToString() ?? "";
                supplierEmailTxt.Text = row.Cells["Email"].Value?.ToString() ?? "";
                supplierFaxTxt.Text = row.Cells["Fax"].Value?.ToString() ?? "";
                supplierWebsiteTxt.Text = row.Cells["Account"].Value?.ToString() ?? "";
            }
        }
        private void supplierAdd_Click(object sender, EventArgs e)
        {
            var newSupplier = new Supplier()
            {
                Name = supplierNameTxt.Text,
                Phone = supplierPhoneTxt.Text,
                Email = supplierEmailTxt.Text,
                Fax = supplierFaxTxt.Text,
                Social_Account = supplierWebsiteTxt.Text,
            };

            context.Suppliers.Add(newSupplier);
            context.SaveChanges();
            LoadSuppliers();
        }
        private void supplierUpdate_Click(object sender, EventArgs e)
        {
            var id = int.Parse(supplierIdTxt.Text);
            var updatedSupplier = context.Suppliers.Find(id);
            if (updatedSupplier != null)
            {
                updatedSupplier.Name = supplierNameTxt.Text;
                updatedSupplier.Phone = supplierPhoneTxt.Text;
                updatedSupplier.Email = supplierEmailTxt.Text;
                updatedSupplier.Fax = supplierFaxTxt.Text;
                updatedSupplier.Social_Account = supplierWebsiteTxt.Text;

                context.SaveChanges();
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Supplier not found!");
            }
        }
        //=======================================================================================
        //==========================================CUSTOMERS====================================
        private void customerData_SelectionChanged(object sender, EventArgs e)
        {
            if (customerData.SelectedRows.Count > 0)
            {
                customerIdTxt.Enabled = false;
                DataGridViewRow row = customerData.SelectedRows[0];
                customerIdTxt.Text = row.Cells["Id"].Value?.ToString() ?? "";
                customerNameTxt.Text = row.Cells["Name"].Value?.ToString() ?? "";
                customerPhoneTxt.Text = row.Cells["Phone"].Value?.ToString() ?? "";
                customerEmailTxt.Text = row.Cells["Email"].Value?.ToString() ?? "";
                customerFaxTxt.Text = row.Cells["Fax"].Value?.ToString() ?? "";
                customerWebsiteTxt.Text = row.Cells["Account"].Value?.ToString() ?? "";
            }
        }
        private void customerAdd_Click(object sender, EventArgs e)
        {
            var newCustomer = new Customer()
            {
                Name = customerNameTxt.Text,
                Phone = customerPhoneTxt.Text,
                Email = customerEmailTxt.Text,
                Fax = customerFaxTxt.Text,
                Social_Account = customerWebsiteTxt.Text,
            };
            context.Customers.Add(newCustomer);
            context.SaveChanges();
            LoadCustomers();
        }
        private void customerUpdate_Click(object sender, EventArgs e)
        {
            var id = int.Parse(customerIdTxt.Text);
            var updatedCustomer = context.Customers.Find(id);
            if (updatedCustomer != null)
            {
                updatedCustomer.Name = customerNameTxt.Text;
                updatedCustomer.Phone = customerPhoneTxt.Text;
                updatedCustomer.Email = customerEmailTxt.Text;
                updatedCustomer.Fax = customerFaxTxt.Text;
                updatedCustomer.Social_Account = customerWebsiteTxt.Text;

                context.SaveChanges();
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Customer not found!");
            }
        }
        //=======================================================================================
        //==========================================PRODUCTS====================================
        private void productData_SelectionChanged(object sender, EventArgs e)
        {
            if (productData.SelectedRows.Count > 0)
            {
                productIdTxt.Enabled = false;
                DataGridViewRow row = productData.SelectedRows[0];
                productIdTxt.Text = row.Cells["Id"].Value?.ToString() ?? "";
                productNameTxt.Text = row.Cells["Product"].Value?.ToString() ?? "";
                productMUTxt.Text = row.Cells["Unit"].Value?.ToString() ?? "";
            }
        }
        private void productAdd_Click(object sender, EventArgs e)
        {
            var newProduct = new Product()
            {
                Name = productNameTxt.Text,
                Measurement_Unit = productMUTxt.Text,
            };

            context.products.Add(newProduct);
            context.SaveChanges();
            LoadProducts();
        }
        private void productUpdate_Click(object sender, EventArgs e)
        {
            var id = int.Parse(productIdTxt.Text);
            var updatedProducts = context.products.Find(id);
            if (updatedProducts != null)
            {
                updatedProducts.Name = productNameTxt.Text;
                updatedProducts.Measurement_Unit = productMUTxt.Text;
            }
            context.SaveChanges();
            LoadProducts();
            productIdTxt.Enabled = true;
        }
        //=======================================================================================
        //==========================================SupplyRequest================================
        private void entriesData_SelectionChanged(object sender, EventArgs e)
        {
            if (entriesData.SelectedRows.Count > 0)
            {
                entryIdTxt.Enabled = false;
                DataGridViewRow row = entriesData.SelectedRows[0];
                entryIdTxt.Text = row.Cells["Id"].Value?.ToString() ?? "";
                entryDateTxt.Text = row.Cells["Date"].Value?.ToString() ?? "";
                EWhIdTxt.Text = row.Cells["WhId"].Value?.ToString() ?? "";
                ESupplierIdTxt.Text = row.Cells["SId"].Value?.ToString() ?? "";
                EProductIdTxt.Text = row.Cells["PId"].Value?.ToString() ?? "";
                EProductQuantityTxt.Text = row.Cells["Quantity"].Value?.ToString() ?? "";
                EproDateTxt.Text = row.Cells["Pro_Date"].Value?.ToString() ?? "";
                EexDateTxt.Text = row.Cells["Exp_Date"].Value?.ToString() ?? "";
            }
        }
        private void entriesAdd_Click(object sender, EventArgs e)
        {
            var newSupplyRequest = new SupplyRequest()
            {
                Request_Date = DateTime.Parse(entryDateTxt.Text),
                Warehouse_Id = int.Parse(EWhIdTxt.Text),
                Supplier_Id = int.Parse(ESupplierIdTxt.Text),
                Product_Id = int.Parse(EProductIdTxt.Text),
                Quantity = int.Parse(EProductQuantityTxt.Text),
                Production_Date = DateTime.Parse(EproDateTxt.Text),
                Expire_Date = DateTime.Parse(EexDateTxt.Text),
            };
            var existingStock = context.Stocks
                .FirstOrDefault(s => s.WarehouseId == newSupplyRequest.Warehouse_Id && s.ProductId == newSupplyRequest.Product_Id);

            if (existingStock != null)
            {
                existingStock.Quantity += newSupplyRequest.Quantity;
            }
            else
            {
                var newStock = new Stock()
                {
                    WarehouseId = newSupplyRequest.Warehouse_Id,
                    ProductId = newSupplyRequest.Product_Id,
                    Quantity = newSupplyRequest.Quantity,
                    ProductionDate = newSupplyRequest.Production_Date,
                    ExpiryDate = newSupplyRequest.Expire_Date
                };
                context.Stocks.Add(newStock);
            }

            context.SupplyRequests.Add(newSupplyRequest);
            context.SaveChanges();
            LoadSupplyRequests();
        }
        private void entriesUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(entryIdTxt.Text);
                var updatedRequest = context.SupplyRequests.Find(id);

                if (updatedRequest != null)
                {
                    updatedRequest.Request_Date = DateTime.Parse(entryDateTxt.Text);
                    updatedRequest.Warehouse_Id = int.Parse(EWhIdTxt.Text);
                    updatedRequest.Supplier_Id = int.Parse(ESupplierIdTxt.Text);
                    updatedRequest.Product_Id = int.Parse(EProductIdTxt.Text);
                    updatedRequest.Quantity = int.Parse(EProductQuantityTxt.Text);
                    updatedRequest.Production_Date = DateTime.Parse(EproDateTxt.Text);
                    updatedRequest.Expire_Date = DateTime.Parse(EexDateTxt.Text);

                    context.SaveChanges();
                    LoadSupplyRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input. Please check your values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //=======================================================================================
        //==========================================SellRequest==================================
        private void releaseData_SelectionChanged(object sender, EventArgs e)
        {
            if (releaseData.SelectedRows.Count > 0)
            {
                releaseIdTxt.Enabled = false;
                DataGridViewRow row = releaseData.SelectedRows[0];
                releaseIdTxt.Text = row.Cells["Id"].Value?.ToString() ?? "";
                releaseDateTxt.Text = row.Cells["Date"].Value?.ToString() ?? "";
                RWhIdTxt.Text = row.Cells["WhId"].Value?.ToString() ?? "";
                RCustIdTxt.Text = row.Cells["CId"].Value?.ToString() ?? "";
                RproductIdTxt.Text = row.Cells["PId"].Value?.ToString() ?? "";
                RproductQuantityTxt.Text = row.Cells["Quantity"].Value?.ToString() ?? "";
            }
        }
        private void releaseAdd_Click(object sender, EventArgs e)
        {
            var newSellRequest = new SellRequest()
            {
                Release_Date = DateTime.Parse(releaseDateTxt.Text),
                Warehouse_Id = int.Parse(RWhIdTxt.Text),
                Customer_Id = int.Parse(RCustIdTxt.Text),
                Product_Id = int.Parse(RproductIdTxt.Text),
                Quantity = int.Parse(RproductQuantityTxt.Text),
            };

            var existingStock = context.Stocks
                .FirstOrDefault(s => s.WarehouseId == newSellRequest.Warehouse_Id && s.ProductId == newSellRequest.Product_Id);

            if (existingStock != null && existingStock.Quantity >= newSellRequest.Quantity)
            {
                existingStock.Quantity -= newSellRequest.Quantity;

                context.SellRequests.Add(newSellRequest);
                context.SaveChanges();
                LoadSellRequests();
            }
            else
            {
                MessageBox.Show("Not enough stock available!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void releaseUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(releaseIdTxt.Text);
                var updatedRequest = context.SellRequests.Find(id);

                if (updatedRequest != null)
                {
                    updatedRequest.Warehouse_Id = int.Parse(RWhIdTxt.Text);
                    updatedRequest.Customer_Id = int.Parse(RCustIdTxt.Text);
                    updatedRequest.Product_Id = int.Parse(RproductIdTxt.Text);
                    updatedRequest.Quantity = int.Parse(RproductQuantityTxt.Text);

                    context.SaveChanges();
                    LoadSellRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input. Please check your values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //=======================================================================================
        //==========================================Transfer==================================
        private void transferAdd_Click(object sender, EventArgs e)
        {
            var fromWH = int.Parse(fromTxt.Text);
            var toWH = int.Parse(toTxt.Text);
            var supplier = int.Parse(TsupplierIdTxt.Text);
            var productId = int.Parse(TproudectIdTxt.Text);
            var quantity = int.Parse(TproductQuantityTxt.Text);
            var productionDate = DateTime.Parse(ProDateTxt.Text);
            var expiryDate = DateTime.Parse(ExDateTxt.Text);

            var sourceStock = context.Stocks
                .FirstOrDefault(s => s.WarehouseId == fromWH && s.ProductId == productId);

            if (sourceStock == null || sourceStock.Quantity < quantity)
            {
                MessageBox.Show("Not enough stock in the source warehouse!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Deduct from source warehouse
            sourceStock.Quantity -= quantity;

            // Add to destination warehouse
            var destinationStock = context.Stocks
                .FirstOrDefault(s => s.WarehouseId == toWH && s.ProductId == productId);

            if (destinationStock != null)
            {
                destinationStock.Quantity += quantity;
            }
            else
            {
                var newStock = new Stock()
                {
                    WarehouseId = toWH,
                    ProductId = productId,
                    Quantity = quantity,
                    ProductionDate = productionDate,
                    ExpiryDate = expiryDate
                };
                context.Stocks.Add(newStock);
            }

            // Create a Transfer Record
            var newTransfer = new Transfer()
            {
                FromWH_Id = fromWH,
                ToWH_Id = toWH,
                Supplier_Id = supplier,
                Product_Id = productId,
                Quantity = quantity,
                Production_Date = productionDate,
                Expire_Date = expiryDate,
            };
            context.Transfers.Add(newTransfer);

            // 📌 Create a SellRequest for Source Warehouse
            var newSellRequest = new SellRequest()
            {
                Release_Date = DateTime.Now, // Current date
                Warehouse_Id = fromWH,
                Customer_Id = null, // No specific customer, it's a warehouse transfer
                Product_Id = productId,
                Quantity = quantity
            };
            context.SellRequests.Add(newSellRequest);

            // 📌 Create a SupplyRequest for Destination Warehouse
            var newSupplyRequest = new SupplyRequest()
            {
                Request_Date = DateTime.Now, // Current date
                Warehouse_Id = toWH,
                Supplier_Id = supplier, // Supplier of the product
                Product_Id = productId,
                Quantity = quantity,
                Production_Date = productionDate,
                Expire_Date = expiryDate
            };
            context.SupplyRequests.Add(newSupplyRequest);

            context.SaveChanges();
            LoadTransfers();
        }
        //=======================================================================================
        //==========================================REPORTS==================================
        private void productPeriodBtn_Click(object sender, EventArgs e)
        {
            int periodValue = (int)numeric.Value;
            string periodType = DMY.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(periodType))
            {
                MessageBox.Show("Please select a period type (Days, Months, Years)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime strictDate = DateTime.Now;

            switch (periodType)
            {
                case "Days":
                    strictDate = DateTime.Now.AddDays(-periodValue);
                    break;
                case "Months":
                    strictDate = DateTime.Now.AddMonths(-periodValue);
                    break;
                case "Years":
                    strictDate = DateTime.Now.AddYears(-periodValue);
                    break;
            }

            var reportData = context.Stocks
                .Join(context.SupplyRequests,
                    stock => new { stock.WarehouseId, stock.ProductId },
                    request => new { WarehouseId = request.Warehouse_Id, ProductId = request.Product_Id },
                    (stock, request) => new
                    {
                        request.Product_Id,
                        ProductName = request.Product.Name,
                        WarehouseName = stock.Warehouse.Name,
                        stock.Quantity,
                        EntryDate = request.Request_Date
                    })
                .Where(s => s.EntryDate.Date == strictDate.Date)
                .ToList();

            productPeriodInStock.DataSource = reportData;

        }
        private void ExpireReport_Click(object sender, EventArgs e)
        {
            int periodValue = (int)Enumeric.Value;
            string periodType = eDMY.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(periodType))
            {
                MessageBox.Show("Please select a period type (Days, Months, Years)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime strictDate = DateTime.Now;

            switch (periodType)
            {
                case "Days":
                    strictDate = DateTime.Now.AddDays(periodValue);
                    break;
                case "Months":
                    strictDate = DateTime.Now.AddMonths(periodValue);
                    break;
                case "Years":
                    strictDate = DateTime.Now.AddYears(periodValue);
                    break;
            }

            var reportData = context.Stocks
                .Where(s => s.ExpiryDate.Date == strictDate.Date)
                .Select(
                    stock => new
                    {
                        stock.ProductId,
                        ProductName = stock.Product.Name,
                        WarehouseName = stock.Warehouse.Name,
                        stock.Quantity,
                        stock.ExpiryDate
                    })
                .ToList();

            ExpireDataReport.DataSource = reportData;

        }
        private void stockInPeriodData_Click(object sender, EventArgs e)
        {
            int periodValue = (int)Snumeric.Value;
            string periodType = sDMY.SelectedItem?.ToString();
            string whName = stockWh.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(periodType) || string.IsNullOrEmpty(whName))
            {
                MessageBox.Show("Please select a period type and warehouse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime strictDate = DateTime.Now;

            switch (periodType)
            {
                case "Days":
                    strictDate = DateTime.Now.AddDays(-periodValue);
                    break;
                case "Months":
                    strictDate = DateTime.Now.AddMonths(-periodValue);
                    break;
                case "Years":
                    strictDate = DateTime.Now.AddYears(-periodValue);
                    break;
            }

            var reportData = context.Stocks
                .Join(context.SupplyRequests,
                    stock => new { stock.WarehouseId, stock.ProductId },
                    request => new { WarehouseId = request.Warehouse_Id, ProductId = request.Product_Id },
                    (stock, request) => new
                    {
                        request.Product_Id,
                        ProductName = request.Product.Name,
                        WarehouseName = stock.Warehouse.Name,
                        stock.Quantity,
                        EntryDate = request.Request_Date
                    })
                .Where(s => s.EntryDate.Date == strictDate.Date && s.WarehouseName == whName)
                .ToList();

            stockDetialsdata.DataSource = reportData;

        }
        private void productReport_Click(object sender, EventArgs e)
        {
            int periodValue = (int)Pnumeric.Value;
            string periodType = pDMY.SelectedItem?.ToString();
            string selectedProduct = productNames.SelectedItem?.ToString();
            List<string> selectedWarehouses = whNamesList.CheckedItems.Cast<string>().ToList();

            if (string.IsNullOrEmpty(periodType) || string.IsNullOrEmpty(selectedProduct) || selectedWarehouses.Count == 0)
            {
                MessageBox.Show("Please select a period type and warehouse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime strictDate = DateTime.Now;

            switch (periodType)
            {
                case "Days":
                    strictDate = DateTime.Now.AddDays(-periodValue);
                    break;
                case "Months":
                    strictDate = DateTime.Now.AddMonths(-periodValue);
                    break;
                case "Years":
                    strictDate = DateTime.Now.AddYears(-periodValue);
                    break;
            }

            var reportData = context.Stocks
            .Join(context.SupplyRequests,
                stock => new { stock.WarehouseId, stock.ProductId },
                request => new { WarehouseId = request.Warehouse_Id, ProductId = request.Product_Id },
                (stock, request) => new
                {
                    request.Product_Id,
                    ProductName = request.Product.Name,
                    WarehouseName = stock.Warehouse.Name,
                    stock.Quantity,
                    EntryDate = request.Request_Date
                })
            .Where(s => s.ProductName == selectedProduct &&
                        selectedWarehouses.Contains(s.WarehouseName) &&
                        s.EntryDate.Date == strictDate.Date)
            .ToList();


            productInWhData.DataSource = reportData;
        }
        private void productTransferReport_Click(object sender, EventArgs e)
        {
            int periodValue = (int)PTnumeric.Value;
            string periodType = ptDMY.SelectedItem?.ToString();
            string selectedProduct = PTProductNames.SelectedItem?.ToString();
            List<string> selectedWarehouses = PTWhList.CheckedItems.Cast<string>().ToList();

            if (string.IsNullOrEmpty(periodType) || string.IsNullOrEmpty(selectedProduct) || selectedWarehouses.Count == 0)
            {
                MessageBox.Show("Please select a period type and warehouse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime strictDate = DateTime.Now;

            switch (periodType)
            {
                case "Days":
                    strictDate = DateTime.Now.AddDays(-periodValue);
                    break;
                case "Months":
                    strictDate = DateTime.Now.AddMonths(-periodValue);
                    break;
                case "Years":
                    strictDate = DateTime.Now.AddYears(-periodValue);
                    break;
            }

            var supplyData = context.Stocks
            .Join(context.SupplyRequests,
                stock => new { stock.WarehouseId, stock.ProductId },
                request => new { WarehouseId = request.Warehouse_Id, ProductId = request.Product_Id },
                (stock, request) => new
                {
                    ProductName = request.Product.Name,
                    FromWarehouse = request.Warehouse.Name,
                    ToWarehouse = "",
                    Quantity = stock.Quantity,
                    EntryDate = request.Request_Date
                })
            .Where(s => s.ProductName == selectedProduct &&
                       selectedWarehouses.Contains(s.FromWarehouse) &&
                       s.EntryDate >= strictDate);

            var transferData = context.Transfers
                .Where(t => t.Product.Name == selectedProduct &&
                           (selectedWarehouses.Contains(t.FromWarehouse.Name) || selectedWarehouses.Contains(t.ToWarehouse.Name)) &&
                           t.Production_Date >= strictDate)
                .Select(transfer => new
                {
                    ProductName = transfer.Product.Name,
                    FromWarehouse = transfer.FromWarehouse.Name,
                    ToWarehouse = transfer.ToWarehouse.Name,
                    Quantity = transfer.Quantity,
                    EntryDate = transfer.Production_Date
                });

            var reportData = supplyData.Concat(transferData).ToList();
            productInWhData.DataSource = reportData;
        }
    }
}
