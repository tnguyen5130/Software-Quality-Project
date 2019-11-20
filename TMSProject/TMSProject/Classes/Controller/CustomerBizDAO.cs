//* FILE			: CMPBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : CityBizDAO for the biiling infomation

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
    /// \class CustomerBizDAO
    /// \brief This class contains the contract's information for a customer file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class CustomerBizDAO
    {
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";


        /// \brief This method UpdateContract for user 
        /// \details <b>Details</b>
        /// This method will update contract database 
        /// \return  void
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

        /// \brief This method InsertCustomer for user 
        /// \details <b>Details</b>
        /// This method will insert customer database 
        /// \return  void
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


        /// \brief This method DeleteCustomer for user 
        /// \details <b>Details</b>
        /// This method will delete customer database 
        /// \return  void
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


        /// \brief This method GetCustomers for user 
        /// \details <b>Details</b>
        /// This method will get customer database 
        /// \return  void
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
