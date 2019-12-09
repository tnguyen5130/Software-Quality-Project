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
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";


        /// \brief This method UpdateTrip for user 
        /// \details <b>Details</b>
        /// This method will update trip info when finishing order
        /// \return  void
        public bool UpdateTrip(Trip trip)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
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
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        /// \brief This method InsertTrip for user 
        /// \details <b>Details</b>
        /// This method will insert trip info when finishing order
        /// \return  void
        public bool InsertTrip(Trip trip)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO trip (tripID, orderID, startCity, endCity, tripStatus)
	                                            VALUES (@tripID, @orderID, @startCity, @endCity, @tripStatus); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@tripID", trip.tripID);
                    myCommand.Parameters.AddWithValue("@orderID", trip.orderID);
                    myCommand.Parameters.AddWithValue("@startCity", trip.startCityID);
                    myCommand.Parameters.AddWithValue("@endCity", trip.endCityID);
                    myCommand.Parameters.AddWithValue("@tripStatus", trip.tripStatus);

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
        
        public List<Trip> GetTrips(string tripID)
        {
            const string sqlStatement = @" SELECT 
                                                orderID, 
                                                contractID, 
                                                orderDate, 
                                                originalCityID, 
                                                desCityID, 
                                                carrierID,
                                                orderStatus, 
                                                mileageID,
                                                destinationCityID,
                                                targetCityID,
                                                distance, 
                                                workingTime
                                            FROM ordering
		                                        INNER JOIN mileage ON ordering.originalCityID = mileage.destinationCityID
                                            OR ordering.desCityID = mileage.destinationCityID
                                            WHERE ordering.originalCityID = @startCityID 
                                            AND ordering.desCityID = @endCityID; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@tripID", tripID);
                
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
      

        public List<Trip> GetTrips(string startCityID, string endCityID)
        {
            const string sqlStatement = @" SELECT 
                                                orderID, 
                                                contractID, 
                                                orderDate, 
                                                originalCityID, 
                                                desCityID, 
                                                carrierID,
                                                orderStatus, 
                                                mileageID,
                                                startCityID,
                                                u1.cityName as startCityName, 
                                                endCityID,
                                                u2.cityName as endCityName, 
                                                distance, 
                                                workingTime
                                            FROM ordering
		                                        INNER JOIN mileage ON ordering.originalCityID = mileage.startCityID
                                            OR ordering.desCityID = mileage.endCityID
                                            INNER JOIN city u1 ON mileage.startCityID = u1.cityID 
                                            INNER JOIN city u2 ON mileage.endCityID = u2.cityID 
                                            WHERE ordering.originalCityID = @startCityID 
                                            AND ordering.desCityID = @endCityID; ";
  
            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@startCityID", startCityID);
                myCommand.Parameters.AddWithValue("@endCityID", endCityID);

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

        public List<Trip> GetTripBilling(string orderID)
        {
            const string sqlStatement = @" SELECT 
                                                startCity, endCity, carrierID from trip
                                           INNER JOIN ordering on trip.orderID = ordering.orderID where trip.orderID = @orderID; ";

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

                var trips = DataTableToTripBillingList(dataTable);

                return trips;
            }
        }

        public List<Trip> GetTripID()
        {
            const string sqlStatement = @" SELECT CONCAT_WS('', 'TRIP', '', DATE_FORMAT(curdate(), '%Y%m%d'), LPAD(COUNT(*) + 1, 4, '0')) AS tripID
                                            FROM trip; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                
                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var trips = DataTableToTripIDList(dataTable);

                return trips;
            }
        }

        public List<Trip> GetShowTripsForBillings(string orderID, string carrierID)
        {
            const string sqlStatement = @" SELECT 
                                                tripID, startCity, u1.cityName as startCityName, endCity, u2.cityName as endCityName, 
                                                tripStatus, distance, workingTime, jobtype, quantity, vantype, ftlRate, ltlRate from trip 
                                           inner join mileage on trip.startCity = mileage.startCityID and trip.endCity = mileage.endCityID 
                                           inner join ordering on trip.orderID = ordering.orderID 
                                           inner join carrier on ordering.carrierID = carrier.carrierID 
                                           INNER JOIN city u1 ON trip.startCity = u1.cityID 
                                           INNER JOIN city u2 ON trip.endCity = u2.cityID 
                                           WHERE trip.orderID = @orderID and ordering.carrierID = @carrierID; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@orderID", orderID);
                myCommand.Parameters.AddWithValue("@carrierID", carrierID);
                
                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var trips = DataTableToBillingList(dataTable);

                return trips;
            }
        }

        private List<Trip> DataTableToTripIDList(DataTable table)
        {
            var trips = new List<Trip>();

            foreach (DataRow row in table.Rows)
            {
                trips.Add(new Trip
                {
                    tripID = row["tripID"].ToString()
                });
            }

            return trips;
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
                    orderID = row["orderID"].ToString(),
                    contractID = row["contractID"].ToString(),
                    orderDate = row["orderDate"].ToString(),
                    originalCityID = row["originalCityID"].ToString(),
                    desCityID = row["desCityID"].ToString(),
                    carrierID = row["carrierID"].ToString(),
                    orderStatus = row["orderStatus"].ToString(),
                    mileageID = row["mileageID"].ToString(),
                    startCityID = row["startCityID"].ToString(),
                    endCityID = row["endCityID"].ToString(),
                    distance = Convert.ToDouble(row["distance"]),
                    workingTime = Convert.ToDouble(row["workingTime"]),
                    startCityName = row["startCityName"].ToString(),
                    endCityName = row["endCityName"].ToString()
                });
            }

            return trips;
        }

        private List<Trip> DataTableToBillingList(DataTable table)
        {
            var trips = new List<Trip>();

            foreach (DataRow row in table.Rows)
            {
                trips.Add(new Trip
                {
                    tripID = row["tripID"].ToString(),
                    startCityID = row["startCity"].ToString(),
                    startCityName = row["startCityName"].ToString(),
                    endCity = row["endCity"].ToString(),
                    endCityName = row["endCityName"].ToString(),
                    tripStatus = row["tripStatus"].ToString(),
                    distance = Convert.ToDouble(row["distance"]),
                    workingTime = Convert.ToDouble(row["workingTime"]),
                    ftlRate = Convert.ToDouble(row["ftlRate"]),
                    ltlRate = Convert.ToDouble(row["ltlRate"]),
                    jobType = Convert.ToInt16(row["jobtype"]),
                    quantity = Convert.ToInt16(row["quantity"]),
                    vanType = Convert.ToInt16(row["vantype"])
                });
            }

            return trips;
        }

        private List<Trip> DataTableToTripBillingList(DataTable table)
        {
            var trips = new List<Trip>();

            foreach (DataRow row in table.Rows)
            {
                trips.Add(new Trip
                {   
                    startCity = row["startCity"].ToString(),
                    endCity = row["endCity"].ToString(),
                    carrierID = row["carrierID"].ToString()
                  
                });
            }

            return trips;
        }
    }
}
