//* FILE			: EmployeeBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : EmployeeBizDAO for the biiling infomation




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Model;
using TMSProject.DBConnect;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace TMSProject.Classes.Controller
{
    /// \class InvoiceBizDAO
    /// \brief This class contains the invoice's information for a Invoice file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class InvoiceBizDAO
    {
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        /// \brief This method UpdateInvoice for user 
        /// \details <b>Details</b>
        /// This method will update invoice when finishing order
        /// \return  void
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

        /// \brief This method InsertInvoice for user 
        /// \details <b>Details</b>
        /// This method will insert invoice when finishing order
        /// \return  void
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


        /// \brief This method DeleteInvoice for user 
        /// \details <b>Details</b>
        /// This method will delete invoice when finishing order
        /// \return  void
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

        /// \brief This method GetInvoices for user 
        /// \details <b>Details</b>
        /// This method will get invoice when finishing order
        /// \return  void
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


        /// \brief This method DataTableToInvoiceList for user 
        /// \details <b>Details</b>
        /// This method will store invoice when finishing order
        /// \return  void
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
