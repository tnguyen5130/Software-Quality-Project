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
    public class OrderBizDAO
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

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

        public void DeleteOrder(Order order)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", order.orderID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<Order> GetOrders(string searchItem)
        {
            const string sqlStatement = @" SELECT 
                                                orderID, 
                                                contractID, 
                                                orderDate, 
                                                originalCityID, 
                                                desCityID, 
                                                carrierID,
                                                orderStatus
                                            FROM ordering
		                                        
                                            WHERE orderID = @SearchItem ";


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

                var orders = DataTableToOrderList(dataTable);

                return orders;
            }
        }

        private List<Order> DataTableToOrderList(DataTable table)
        {
            var orders = new List<Order>();

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new Order
                {
                    orderID = row["orderID"].ToString(),
                    contractID = row["contractID"].ToString(),
                    orderDate = row["orderDate"].ToString(),
                    origincalCityID = row["originalCityID"].ToString(),
                    desCityID = row["desCityID"].ToString(),
                    carrierID = row["carrierID"].ToString(),
                    orderStatus = row["orderStatus"].ToString()
                });
            }

            return orders;
        }

    }
}
