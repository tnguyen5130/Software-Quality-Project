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
    public class InvoiceBizDAO
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        public bool UpdateInvoice(Invoice invoice)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  UPDATE invoice
	                                            SET billingID = @billingID,
                                                    contractID = @contractID,
		                                            customerID = @customerID, 
                                                    completeStatus = @completeStatus, 
	                                            WHERE invoiceID = @invoiceID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@billingID", invoice.billingID);
                    myCommand.Parameters.AddWithValue("@contractID", invoice.contractID);
                    myCommand.Parameters.AddWithValue("@customerID", invoice.customerID);
                    myCommand.Parameters.AddWithValue("@completeStatus", invoice.completeStatus);
                    myCommand.Parameters.AddWithValue("@invoiceID", invoice.invoiceID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


        }


        public bool InsertInvoice(Invoice invoice)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO invoice (invoiceID, billingID, contractID, customerID, completeStatus)
	                                            VALUES (@invoiceID, @billingID, @contractID, @customerID, @completeStatus); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@invoiceID", invoice.invoiceID);
                    myCommand.Parameters.AddWithValue("@billingID", invoice.billingID);
                    myCommand.Parameters.AddWithValue("@contractID", invoice.contractID);
                    myCommand.Parameters.AddWithValue("@customerID", invoice.customerID);
                    myCommand.Parameters.AddWithValue("@completeStatus", invoice.completeStatus);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

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

        public List<Invoice> GetInvoices(string billingID, string orderID)
        {
            const string sqlStatement = @" SELECT 
                                                billingID, 
                                                contractID, 
                                                ordering.customerID,
                                                orderStatus 
                                            FROM billing 
		                                    INNER JOIN 
                                                ordering on billing.orderID = ordering.orderID
                                            WHERE billingID = @billingID AND ordering.orderID = @orderID; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@billingID", billingID);
                myCommand.Parameters.AddWithValue("@orderID", orderID);

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
                    billingID = row["billingID"].ToString(),
                    contractID = row["contractID"].ToString(),
                    customerID = row["customerID"].ToString(),
                    completeStatus = row["orderStatus"].ToString()
                });
            }

            return invoices;
        }
    }
}

