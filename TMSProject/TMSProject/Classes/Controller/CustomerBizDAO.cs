//* FILE			: CustomerBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : CustomerBizDAO for the biiling infomation

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Model;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using TMSProject.DBConnect;
using System.Windows.Controls;
using log4net;

namespace TMSProject.Classes.Controller
{
    /// \class CustomerBizDAO
    /// \brief This class contains the contract's information for a customer file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class CustomerBizDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        /// \brief This method UpdateCustomer for user 
        /// \details <b>Details</b>
        /// This method will update customer database 
        /// \return  void
        public void UpdateCustomer(Customer customer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE customer
	                                            SET 
                                                    customerCompany = @CustomerCompany,                                                    
                                                    customerCity = @CustomerCity,
                                                    customerProvince = @CustomerProvince,
		                                            telno = @CustomerTelPhone,
                                                    address = @CustomerAddress,
                                                    zipcode = @CustomerZipcode
	                                            WHERE customerName = @CustomerName; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@CustomerName", customer.customerName);
                myCommand.Parameters.AddWithValue("@CustomerCompany", customer.customerCompany);
                myCommand.Parameters.AddWithValue("@CustomerCity", customer.customerCity);
                myCommand.Parameters.AddWithValue("@CustomerProvince", customer.customerProvince);
                myCommand.Parameters.AddWithValue("@CustomerTelPhone", customer.telno);
                myCommand.Parameters.AddWithValue("@CustomerAddress", customer.address);
                myCommand.Parameters.AddWithValue("@CustomerZipcode", customer.zipcode);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        /// \brief This method InsertCustomer for user 
        /// \details <b>Details</b>
        /// This method will insert customer database 
        /// \return  void
        public void InsertCustomer(Customer customer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO customer (customerID, customerName, customerCity, customerProvince, telno, address, zipcode, customerCompany)
	                                            VALUES (@CustomerID, @CustomerName, @CustomerCity, @CustomerProvince, @CustomerTelNo, @CustomerAddress, @CustomerZipcode, @CustomerCompany); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@CustomerID", customer.customerID);
                myCommand.Parameters.AddWithValue("@CustomerName", customer.customerName);
                myCommand.Parameters.AddWithValue("@CustomerCity", customer.customerCity);
                myCommand.Parameters.AddWithValue("@CustomerProvince", customer.customerProvince);
                myCommand.Parameters.AddWithValue("@CustomerTelNo", customer.telno);
                myCommand.Parameters.AddWithValue("@CustomerAddress", customer.address);
                myCommand.Parameters.AddWithValue("@CustomerZipcode", customer.zipcode);
                myCommand.Parameters.AddWithValue("@CustomerCompany", customer.customerCompany);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        /// \brief This method DeleteCustomer for user 
        /// \details <b>Details</b>
        /// This method will delete customer database 
        /// \return  void
        public void DeleteCustomer(Customer customer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM customer WHERE customerID = @CustomerID;";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@CustomerID", customer.customerID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        /// \brief This method GetLastCustomerName for user 
        /// \details <b>Details</b>
        /// This method will get last customer's name database 
        /// \return  void
        public string GetLastCustomerName(Customer customer)
        {
            string value = "";
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  SELECT customerName FROM customer ORDER BY customerID DESC LIMIT 1; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                value = (string)myCommand.ExecuteScalar();
            }
            return value;
        }

        /// \brief This method LoadOrderList for user 
        /// \details <b>Details</b>
        /// This method will get order list that has orderID and orderDate into DataGrid table
        /// \return  void
        public void LoadCustomerList(DataGrid grid)
        {
            const string sqlStatement = @" SELECT * FROM customer;";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                DataTable dataTable = new DataTable("customer");

                myAdapter.Fill(dataTable);

                grid.ItemsSource = dataTable.DefaultView;
            }
        }

        /// \brief This method GetCustomerIDbyName for user 
        /// \details <b>Details</b>
        /// This method will get the customer ID to check the existing customer from CMP
        /// \return  void
        public string GetCustomerIDbyName(string customerName)
        {
            string value = "";
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  SELECT customer.customerID FROM customer 
                                                INNER JOIN contract_market_place ON contract_market_place.customerID = customer.customerID 
                                                WHERE customerName = @customerName; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@customerName", customerName);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                value = (string)myCommand.ExecuteScalar();
            }
            return value;
        }

        /// \brief This method GetLastCustomerID for user 
        /// \details <b>Details</b>
        /// This method will get last customer's ID database 
        /// \return  void
        public string GetLastCustomerID(Customer customer)
        {
            string value = "";
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  SELECT customerID FROM customer ORDER BY customerID DESC LIMIT 1; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                value = (string)myCommand.ExecuteScalar();
            }
            return value;
        }

        /// \brief This method GetCustomers for user 
        /// \details <b>Details</b>
        /// This method will get customer database 
        /// \return  void
        public List<Customer> GetCustomers(string searchItem)
        {
            const string sqlStatement = @" SELECT customerName, customerCompany, telno, address, customerCity, customerProvince, zipcode
                                           FROM customer 
                                           INNER JOIN ordering
                                           WHERE customer.customerID = ordering.customerID
                                           AND ordering.orderID = @SearchItem; ";


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

                var customers = DataTableToCustomerList(dataTable);

                return customers;
            }
        }

        /// \brief This method GetCustomers for user 
        /// \details <b>Details</b>
        /// This method will get customer database 
        /// \return  void
        public List<Customer> GetCustomersDetailsOnly(string searchItem)
        {
            const string sqlStatement = @" SELECT customerName, customerCompany, telno, address, customerCity, customerProvince, zipcode
                                           FROM customer
                                           WHERE customer.customerName = @SearchItem; ";


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

                var customers = DataTableToCustomerList(dataTable);

                return customers;
            }
        }

        /// \brief This method DataTableToCustomerList for user 
        /// \details <b>Details</b>
        /// This method will store customer database 
        /// \return  void
        private List<Customer> DataTableToCustomerList(DataTable table)
        {
            var customers = new List<Customer>();

            foreach (DataRow row in table.Rows)
            {
                customers.Add(new Customer
                {
                    customerName = row["customerName"].ToString(),
                    customerCompany = row["customerCompany"].ToString(),
                    telno = row["telno"].ToString(),
                    address = row["address"].ToString(),
                    customerCity = row["customerCity"].ToString(),
                    customerProvince = row["customerProvince"].ToString(),
                    zipcode = row["zipcode"].ToString()          
            });
            }

            return customers;
        }
    } // End of class
} // end of namespace
