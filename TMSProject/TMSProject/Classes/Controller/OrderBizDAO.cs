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
using log4net;

namespace TMSProject.Classes.Controller
{
    /// \class InvoiceBizDAO
    /// \brief This class contains the invoice's information for a Order file when buyer make an order
    /// \author : <i>Nhung Luong <i>
    public class OrderBizDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
	                                            SET carrierID = @carrierID
	                                            WHERE orderID = @orderID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@carrierID", order.carrierID);
                myCommand.Parameters.AddWithValue("@orderID", order.orderID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                Log.Info("SQL Execute: " + sqlStatement);
                
                return true;
                }
                catch(Exception ex)
                {
                    Log.Error("SQL Execute Error: " + ex.Message);
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
                    myCommand.Parameters.AddWithValue("@originalCityID", order.originalCityID);
                    myCommand.Parameters.AddWithValue("@desCityID", order.desCityID);
                    myCommand.Parameters.AddWithValue("@carrierID", order.carrierID);
                    myCommand.Parameters.AddWithValue("@orderStatus", order.orderStatus);
                
                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    Log.Info("SQL Execute: " + sqlStatement);
                    return true;
                }
                catch(Exception ex)
                {
                    Log.Error("SQL Execute Error: " + ex.Message);
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        }


        /// \brief This method DeleteOrder for user 
        /// \details <b>Details</b>
        /// This method will delete order when finishing order
        /// \return  void
        public void DeleteOrder(Order order)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@ProductID", order.orderID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    Log.Info("SQL Execute: " + sqlStatement);
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
            }
        }



        /// \brief This method GetOrders for user 
        /// \details <b>Details</b>
        /// This method will get order when finishing order
        /// \return  void
        public List<Order> GetOrders(string searchItem)
        {
            try
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
                    Log.Info("SQL Execute: " + sqlStatement);
                    return orders;
                }
                
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
            
        }

        public List<Order> GetOrderWithID(string orderID)
        {
            try
            {
                const string sqlStatement = @" select u1.cityName as startCityName, u2.cityName as endCityName from ordering                                              
                                            INNER JOIN city u1 ON ordering.originalCityID = u1.cityID
                                            INNER JOIN city u2 ON ordering.desCityID = u2.cityID
                                            where orderID = @orderID; ";
                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@orderID", orderID);

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var dataTable = new DataTable();

                    myAdapter.Fill(dataTable);

                    var orders = DataTableToCityNamesList(dataTable);
                    Log.Info("SQL Execute: " + sqlStatement);
                    return orders;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
            
        }

        private List<Order> DataTableToCityNamesList(DataTable table)
        {
            try
            {
                var orders = new List<Order>();

                foreach (DataRow row in table.Rows)
                {
                    orders.Add(new Order
                    {
                        startCityName = row["startCityName"].ToString(),
                        endCityName = row["endCityName"].ToString()

                    });
                }
                Log.Info("SQL ResultSet Execute!!!");
                return orders;
                
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
            
        }

        public List<Order> GetOrderDetailWithID(string orderID)
        {
            try
            {
                const string sqlStatement = @" SELECT orderID, contractID, customerName, orderDate, jobType, quantity, vanType, carrierID,                                              
                                            originalCityID, u1.cityName as startCityName, desCityID, u2.cityName as endCityName from ordering
                                            INNER JOIN customer on ordering.customerID = customer.customerID
                                            INNER JOIN city u1 ON ordering.originalCityID = u1.cityID
                                            INNER JOIN city u2 ON ordering.desCityID = u2.cityID
                                            WHERE orderID = @orderID ";

                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@orderID", orderID);

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var dataTable = new DataTable();

                    myAdapter.Fill(dataTable);

                    var orders = DataTableToOrderDetailList(dataTable);
                    Log.Info("SQL Execute: " + sqlStatement);
                    return orders;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
            
        }

        public List<Order> GetOrderDetail(string startCity, string endCity)
        {
            try
            {
                const string sqlStatement = @" SELECT orderID, contractID, customerName, orderDate, jobType, quantity, vanType,                                              
                                            originalCityID, u1.cityName as startCityName, desCityID, u2.cityName as endCityName, 
                                            carrierID from ordering
                                            INNER JOIN customer on ordering.customerID = customer.customerID
                                            INNER JOIN city u1 ON ordering.originalCityID = u1.cityID
                                            INNER JOIN city u2 ON ordering.desCityID = u2.cityID
                                            WHERE u1.cityName = @startCityName or u2.cityName = @endCityName ";

                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@startCityName", startCity);
                    myCommand.Parameters.AddWithValue("@endCityName", endCity);

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var dataTable = new DataTable();

                    myAdapter.Fill(dataTable);

                    var orders = DataTableToOrderDetailList(dataTable);
                    Log.Info("SQL Execute: " + sqlStatement);
                    return orders;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
            
        }

        private List<Order> DataTableToOrderDetailList(DataTable table)
        {
            try
            {
                var orders = new List<Order>();

                foreach (DataRow row in table.Rows)
                {
                    orders.Add(new Order
                    {
                        orderID = row["orderID"].ToString(),
                        contractID = row["contractID"].ToString(),
                        originalCityID = row["originalCityID"].ToString(),
                        startCityName = row["startCityName"].ToString(),
                        desCityID = row["desCityID"].ToString(),
                        customerName = row["customerName"].ToString(),
                        endCityName = row["endCityName"].ToString(),
                        orderDate = row["orderDate"].ToString(),
                        jobType = Convert.ToInt32(row["jobType"]),
                        quantity = Convert.ToInt32(row["quantity"]),
                        vanType = Convert.ToInt32((row["vanType"])),
                        carrierID = row["carrierID"].ToString()
                    });
                }

                Log.Info("ResultSet Execute!!!");
                return orders;
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }
            
        }

        /// \brief This method DataTableToOrderList for user 
        /// \details <b>Details</b>
        /// This method will store order when finishing order
        /// \return  void
        private List<Order> DataTableToOrderList(DataTable table)
        {
            try
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

                Log.Info("ResultSet Execute!!!");
                return orders;
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }
            
        }

        public void loadOrderList(DataGrid grid)
        {
            try
            {
                const string sqlStatement = @" SELECT orderID, orderDate FROM ordering;";

                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    //myCommand.Parameters.AddWithValue("@OrderID", order.orderID);
                    //myCommand.Parameters.AddWithValue("@OrderDate", order.orderDate);

                    //myConn.Open();

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    DataTable dataTable = new DataTable("ordering");

                    myAdapter.Fill(dataTable);

                    //var orders = DataTableToOrderList(dataTable);

                    grid.ItemsSource = dataTable.DefaultView;
                }

                Log.Info("SQL Execute:" + sqlStatement);
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
            }
        }
    }
}
