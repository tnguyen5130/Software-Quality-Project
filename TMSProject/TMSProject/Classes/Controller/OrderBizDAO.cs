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
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

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

        /// \brief This method InsertOrder for user 
        /// \details <b>Details</b>
        /// This method will insert order when finishing order
        /// \return  void
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

                myCommand.Parameters.AddWithValue("@ProductID", order.orderID);

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
