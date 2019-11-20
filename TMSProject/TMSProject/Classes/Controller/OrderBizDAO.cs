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
    public class OrderBizDAO
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        public void UpdateOrder(Order order)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", order.orderID);
                myCommand.Parameters.AddWithValue("@CategoryId", order.contractID);
                myCommand.Parameters.AddWithValue("@UnitPrice", order.orderDate);
                myCommand.Parameters.AddWithValue("@UnitsInStock", order.origincalCityID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", order.desCityID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", order.carrierID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", order.orderStatus);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        public void InsertOrder(Order order)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductName", order.orderID);
                myCommand.Parameters.AddWithValue("@SupplierID", order.contractID);
                myCommand.Parameters.AddWithValue("@CategoryID", order.orderDate);
                myCommand.Parameters.AddWithValue("@QuantityPerUnit", order.origincalCityID);
                myCommand.Parameters.AddWithValue("@UnitPrice", order.desCityID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", order.orderStatus);
                
                myConn.Open();

                myCommand.ExecuteNonQuery();
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

        public List<Order> DataTableToOrderList(DataTable table)
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

        public void loadOrderList(DataGrid grid)
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
        }

    }
}
