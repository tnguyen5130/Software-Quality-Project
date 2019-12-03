//* FILE			: OrderBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : OrderBizDAO for the biiling infomation



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Model;
using TMSProject.DBConnect;
using TMSProject.Classes.View;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Controls;

namespace TMSProject.Classes.Controller
{
    /// \class InvoiceBizDAO
    /// \brief This class contains the invoice's information for a Order file when buyer make an order
    /// \author : <i>Nhung Luong <i>
    public class OrderBizDAO
    {
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        /// \brief This method UpdateOrder for user 
        /// \details <b>Details</b>
        /// This method will update order when finishing order
        /// \return  void
        public bool UpdateOrder(Order order)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {   
                    const string sqlStatement = @"  UPDATE ordering
	                                            SET contractID = @contractID,
                                                    orderDate = @orderDate,
		                                            originalCityID = @originalCityID,
                                                    desCityID = @desCityID,
                                                    carrierID = @carrierID,
                                                    orderStatus = @orderStatus
	                                            WHERE orderID = @orderID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@contractID", order.contractID);
                myCommand.Parameters.AddWithValue("@orderDate", order.orderDate);
                myCommand.Parameters.AddWithValue("@originalCityID", order.origincalCityID);
                myCommand.Parameters.AddWithValue("@desCityID", order.desCityID);
                myCommand.Parameters.AddWithValue("@carrierID", order.carrierID);
                myCommand.Parameters.AddWithValue("@orderStatus", order.orderStatus);
                myCommand.Parameters.AddWithValue("@orderID", order.orderID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                
                return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        }

        /// \brief This method InsertOrder for user 
        /// \details <b>Details</b>
        /// This method will insert order when finishing order
        /// \return  void
        public bool InsertOrder(Order order)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO ordering (orderID, contractID, orderDate, originalCityID, desCityID, carrierID, orderStatus)
	                                                VALUES (@orderID, @contractID, @orderDate, @originalCityID, @desCityID, @carrierID, @orderStatus); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    
                    myCommand.Parameters.AddWithValue("@orderID", order.orderID);
                    myCommand.Parameters.AddWithValue("@contractID", order.contractID);
                    myCommand.Parameters.AddWithValue("@orderDate", order.orderDate);
                    myCommand.Parameters.AddWithValue("@originalCityID", order.origincalCityID);
                    myCommand.Parameters.AddWithValue("@desCityID", order.desCityID);
                    myCommand.Parameters.AddWithValue("@carrierID", order.carrierID);
                    myCommand.Parameters.AddWithValue("@orderStatus", order.orderStatus);
                
                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        }

        /// \brief This method GetLastOrderID for user 
        /// \details <b>Details</b>
        /// This method will get the last order id to check whether DB contains it
        /// \return  void
        public string GetLastOrderID(Order order)
        {
            string value = "";
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  SELECT orderID FROM ordering ORDER BY orderID DESC LIMIT 1; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                value = (string)myCommand.ExecuteScalar();
            }
            return value;
        }

        /// \brief This method DeleteOrder for user 
        /// \details <b>Details</b>
        /// This method will delete order when finishing order
        /// \return  void
        public void DeleteOrder(Order order)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }



        /// \brief This method GetOrders for user 
        /// \details <b>Details</b>
        /// This method will get order when finishing order
        /// \return  void
        public List<Order> GetOrders(string searchItem)
        {
            const string sqlStatement = @" SELECT orderID, orderDate                                                
                                            FROM ordering
                                            WHERE orderID = @OrderID, 
                                                  orderDate = @OrderDate ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@OrderID", searchItem);

                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var orders = DataTableToOrderList(dataTable);

                return orders;
            }
        }

        /// \brief This method DataTableToOrderList for user 
        /// \details <b>Details</b>
        /// This method will store order when finishing order
        /// \return  void
        private List<Order> DataTableToOrderList(DataTable table)
        {
            var orders = new List<Order>();

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new Order
                {
                    orderID = row["orderID"].ToString(),
                    orderDate = row["orderDate"].ToString(),
                });
            }

            return orders;
        }

        /// \brief This method loadOrderList for user 
        /// \details <b>Details</b>
        /// This method will get order list that has orderID and orderDate into DataGrid table
        /// \return  void
        public void loadOrderList(DataGrid grid)
        {
            const string sqlStatement = @" SELECT orderID, orderDate FROM ordering;";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                DataTable dataTable = new DataTable("ordering");

                myAdapter.Fill(dataTable);

                grid.ItemsSource = dataTable.DefaultView;             
            }
        }

    }
}
