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
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", customer.customerID);
                myCommand.Parameters.AddWithValue("@CategoryId", customer.customerName);
                myCommand.Parameters.AddWithValue("@UnitPrice", customer.customerCity);
                myCommand.Parameters.AddWithValue("@UnitsInStock", customer.telno);
                myCommand.Parameters.AddWithValue("@UnitsInStock", customer.address);
                myCommand.Parameters.AddWithValue("@UnitsInStock", customer.zipcode);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertCustomer(Customer customer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", customer.customerID);
                myCommand.Parameters.AddWithValue("@CategoryId", customer.customerName);
                myCommand.Parameters.AddWithValue("@UnitPrice", customer.customerCity);
                myCommand.Parameters.AddWithValue("@UnitsInStock", customer.telno);
                myCommand.Parameters.AddWithValue("@UnitsInStock", customer.address);
                myCommand.Parameters.AddWithValue("@UnitsInStock", customer.zipcode);

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
    }
}
