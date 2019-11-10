using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.BusinessLayer;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace TMSProject.Classes.DataLayer
{
    public class InvoiceBizDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void UpdateInvoice(Invoice invoice)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", invoice.invoiceID);
                myCommand.Parameters.AddWithValue("@CategoryId", invoice.billingID);
                myCommand.Parameters.AddWithValue("@UnitPrice", invoice.contractID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", invoice.customerID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", invoice.completeStatus);
          
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertInvoice(Invoice invoice)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", invoice.invoiceID);
                myCommand.Parameters.AddWithValue("@CategoryId", invoice.billingID);
                myCommand.Parameters.AddWithValue("@UnitPrice", invoice.contractID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", invoice.customerID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", invoice.completeStatus);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        public void DeleteInvoice(Invoice invoice)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", invoice.invoiceID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<Invoice> GetInvoices(string searchItem)
        {
            const string sqlStatement = @" SELECT 
                                                ProductId, 
                                                ProductName, 
                                                QuantityPerUnit, 
                                                UnitPrice, 
                                                UnitsInStock, 
                                                QuantityPerUnit,
                                                UnitsOnOrder, 
                                                ReorderLevel,
                                                categories.CategoryId,
                                                CategoryName,
                                                Description, 
                                                suppliers.SupplierId,
                                                CompanyName
                                            FROM products
		                                        INNER JOIN categories ON products.CategoryId = categories.CategoryID 
                                                INNER JOIN suppliers ON products.SupplierId = suppliers.SupplierId
                                            WHERE Discontinued <> 1 
                                                AND ( ProductId = @SearchItem 
                                                        OR ProductName = @SearchItem
		                                                OR CategoryName = @SearchItem 
                                                        OR CompanyName = @SearchItem
		                                                OR @SearchItem = '')
                                            ORDER BY ProductName; ";


            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@SearchItem", searchItem);

                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var invoices = DataTableToInvoiceList(dataTable);

                return invoices;
            }
        }

        private List<Invoice> DataTableToInvoiceList(DataTable table)
        {
            var invoices = new List<Invoice>();

            foreach (DataRow row in table.Rows)
            {
                invoices.Add(new Invoice
                {
                    invoiceID = row["invoiceID"].ToString(),
                    billingID = row["billingID"].ToString(),
                    contractID = row["contractID"].ToString(),
                    customerID = row["customerID"].ToString(),
                    completeStatus = row["completeStatus"].ToString()
            });
            }

            return invoices;
        }
    }
}
