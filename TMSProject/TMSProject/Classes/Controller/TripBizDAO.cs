//* FILE			: TripBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : TripBizDAO for the biiling infomation


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
    /// \class TripBizDAO
    /// \brief This class contains the trip's information for a Order file when buyer make an order
    /// \author : <i>Nhung Luong <i>
    public class TripBizDAO
    {
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";


        /// \brief This method UpdateTrip for user 
        /// \details <b>Details</b>
        /// This method will update trip info when finishing order
        /// \return  void
        public void UpdateTrip(Trip trip)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", trip.tripID);
                myCommand.Parameters.AddWithValue("@CategoryId", trip.orderID);
                myCommand.Parameters.AddWithValue("@CategoryId", trip.tripStatus);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }



        /// \brief This method InsertTrip for user 
        /// \details <b>Details</b>
        /// This method will insert trip info when finishing order
        /// \return  void
        public void InsertTrip(Trip trip)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", trip.tripID);
                myCommand.Parameters.AddWithValue("@CategoryId", trip.orderID);
                myCommand.Parameters.AddWithValue("@CategoryId", trip.tripStatus);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        /// \brief This method DeleteTrip for user 
        /// \details <b>Details</b>
        /// This method will delete trip info when finishing order
        /// \return  void
        public void DeleteTrip(Trip trip)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", trip.tripID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }



        /// \brief This method GetTrip for user 
        /// \details <b>Details</b>
        /// This method will get trip info when finishing order
        /// \return  void
        public List<Trip> GetTrips(string searchItem)
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

                var trips = DataTableToTripList(dataTable);

                return trips;
            }
        }


        /// \brief This method DataTableToTripList for user 
        /// \details <b>Details</b>
        /// This method will delete trip list info when finishing order
        /// \return  void
        private List<Trip> DataTableToTripList(DataTable table)
        {
            var trips = new List<Trip>();

            foreach (DataRow row in table.Rows)
            {
                trips.Add(new Trip
                {
                    tripID = row["tripID"].ToString(),
                    orderID = row["orderID"].ToString(),
                    tripStatus = row["tripStatus"].ToString(),

                });
            }

            return trips;
        }
    }
}
