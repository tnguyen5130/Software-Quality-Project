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
    public class TripBizDAO
    {
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

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
                    myCommand.Parameters.AddWithValue("@startCity", trip.startCity);
                    myCommand.Parameters.AddWithValue("@endCity", trip.endCity);
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
                                                endCityID,
                                                distance, 
                                                workingTime
                                            FROM ordering
		                                        INNER JOIN mileage ON ordering.originalCityID = mileage.startCityID
                                            OR ordering.desCityID = mileage.endCityID
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
                });
            }

            return trips;
        }
    }
}
