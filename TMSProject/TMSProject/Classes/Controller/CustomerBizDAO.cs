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

namespace TMSProject.Classes.Controller
{
    public class CustomerBizDAO
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        public void UpdateCustomer(Customer customer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE customer
	                                            SET customerID = @CustomerID,
                                                    customerName = @CustomerName
                                                    customerCity = @CustomerCity,
		                                            telno = @CustomerTelPhone,
                                                    address = @CustomerAddress,
                                                    zipcode = @CustomerZipcode
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@CustomerID", customer.customerID);
                myCommand.Parameters.AddWithValue("@CustomerName", customer.customerName);
                myCommand.Parameters.AddWithValue("@CustomerCity", customer.customerCity);
                myCommand.Parameters.AddWithValue("@CustomerTelPhone", customer.telno);
                myCommand.Parameters.AddWithValue("@CustomerAddress", customer.address);
                myCommand.Parameters.AddWithValue("@CustomerZipcode", customer.zipcode);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertCustomer(Customer customer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO customer (customerID, customerName, customerCity, telno, address, zipcode)
	                                            VALUES (@CustomerID, @Cust@CustomerName, @CustomerCity, @CustomerTelNo, @CustomerAddress, @CustomerZipcode); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@CustomerID", customer.customerID);
                myCommand.Parameters.AddWithValue("@CustomerName", customer.customerName);
                myCommand.Parameters.AddWithValue("@CustomerCity", customer.customerCity);
                myCommand.Parameters.AddWithValue("@CustomerTelNo", customer.telno);
                myCommand.Parameters.AddWithValue("@CustomerAddress", customer.address);
                myCommand.Parameters.AddWithValue("@CustomerZipcode", customer.zipcode);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        public void DeleteCustomer(Customer customer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", customer.customerID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<Customer> GetCustomers(string searchItem)
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

                var customers = DataTableToCustomerList(dataTable);

                return customers;
            }
        }

        private List<Customer> DataTableToCustomerList(DataTable table)
        {
            var customers = new List<Customer>();

            foreach (DataRow row in table.Rows)
            {
                customers.Add(new Customer
                {
                    customerID = row["customerID"].ToString(),
                    customerName = row["customerName"].ToString(),
                    customerCity = row["customerCity"].ToString(),
                    telno = row["telno"].ToString(),
                    address = row["address"].ToString(),
                    zipcode = row["zipcode"].ToString()
            });
            }

            return customers;
        }
    }
}
