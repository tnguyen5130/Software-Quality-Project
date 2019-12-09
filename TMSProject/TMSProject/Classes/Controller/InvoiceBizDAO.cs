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
                                                    invoiceDate = @invoiceDate,
                                                    completeStatus = @completeStatus, 
	                                            WHERE invoiceID = @invoiceID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@billingID", invoice.billingID);
                    myCommand.Parameters.AddWithValue("@contractID", invoice.contractID);
                    myCommand.Parameters.AddWithValue("@customerID", invoice.customerID);
                    myCommand.Parameters.AddWithValue("@invoiceDate", invoice.invoiceDate);
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
                    const string sqlStatement = @"  INSERT INTO invoice (invoiceID, billingID, contractID, customerID, invoiceDate, completeStatus)
	                                            VALUES (@invoiceID, @billingID, @contractID, @customerID, @invoiceDate, @completeStatus); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@invoiceID", invoice.invoiceID);
                    myCommand.Parameters.AddWithValue("@billingID", invoice.billingID);
                    myCommand.Parameters.AddWithValue("@contractID", invoice.contractID);
                    myCommand.Parameters.AddWithValue("@customerID", invoice.customerID);
                    myCommand.Parameters.AddWithValue("@invoiceDate", invoice.invoiceDate);
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

        public List<Invoice> GetInvoiceID(string invoiceID)
        {

            const string sqlStatement = @" SELECT 
                                                invoiceID, 
                                                invoice.billingID, 
                                                billing.orderID
                                            FROM invoice
                                            INNER JOIN billing on invoice.billingID = billing.billingID
                                            WHERE invoiceID = @invoiceID; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@invoiceID", invoiceID);
               
                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var invoices = DataTableToInvoiceIDList(dataTable);

                return invoices;
            }
        }

        public List<Invoice> ViewInvoices(string billingID, string orderID)
        {
            const string sqlStatement = @" select billing.billingID, customerName, billing.orderID, orderDate, customerCity, 
                                            telno, address, zipcode, customerCompany, customerProvince,  
                                            startCity, u1.cityName as startCityName, endCity, u2.cityName as endCityName,
                                            tripStatus, invoiceID, jobType 
                                            from ordering 
		                                    inner join billing on ordering.orderID = billing.orderID
                                            inner join customer on ordering.customerID = customer.customerID
                                            inner join trip on ordering.orderID = trip.orderID
                                            INNER JOIN city u1 ON trip.startCity = u1.cityID
                                            INNER JOIN city u2 ON trip.endCity = u2.cityID
                                            INNER JOIN invoice on billing.billingID = invoice.billingID 
                                            WHERE billing.billingID = @billingID AND ordering.orderID = @orderID; ";

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

                var invoices = DataTableToViewInvoiceList(dataTable);

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

        private List<Invoice> DataTableToInvoiceIDList(DataTable table)
        {
            var invoices = new List<Invoice>();

            foreach (DataRow row in table.Rows)
            {
                invoices.Add(new Invoice
                {
                    orderID = row["orderID"].ToString(),
                    billingID = row["billingID"].ToString(),
                    invoiceID = row["invoiceID"].ToString()
                });
            }

            return invoices;
        }

        private List<Invoice> DataTableToViewInvoiceList(DataTable table)
        {
            var invoices = new List<Invoice>();

            foreach (DataRow row in table.Rows)
            {
                invoices.Add(new Invoice
                {
                    billingID = row["billingID"].ToString(),
                    customerName = row["customerName"].ToString(),
                    orderID = row["orderID"].ToString(),
                    orderDate = row["orderDate"].ToString(),
                    customerCity = row["customerCity"].ToString(),
                    telno = row["telno"].ToString(),
                    address = row["address"].ToString(),
                    zipcode = row["zipcode"].ToString(),
                    customerCompany = row["customerCompany"].ToString(),
                    customerProvince = row["customerProvince"].ToString(),
                    startCityName = row["startCityName"].ToString(),
                    endCityName = row["endCityName"].ToString(),
                    invoiceID = row["invoiceID"].ToString(),
                    jobType = Convert.ToInt32(row["jobType"])
                });
            }

            return invoices;
        }
    }
}

