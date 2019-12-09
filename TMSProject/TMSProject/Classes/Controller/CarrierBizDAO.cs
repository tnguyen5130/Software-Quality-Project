//* FILE			: CarrierBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : CarrierBizDAO for the biiling infomation



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
    /// \class CarrierBizDAO
    /// \brief This class contains the carrier's information for a billing file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class CarrierBizDAO
    {
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";


        /// \brief This method UpdateCarrier for user 
        /// \details <b>Details</b>
        /// This method will update carrier when finishing order
        /// \return  void
        public bool UpdateCarrier(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  UPDATE carrier
	                                            SET depotCity = @depotCity,
                                                    carrierName = @carrierName,
		                                            ftlAvail = @ftlAvail, 
                                                    ltlAvail = @ltlAvail, 
                                                    ftlRate = @ftlRate, 
                                                    ltlRate = @ltlRate,
                                                    carrierID = @carrierID 
	                                            WHERE carrierID = @carrierID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@depotCity", carrier.depotCity);
                    myCommand.Parameters.AddWithValue("@carrierName", carrier.carrierName);
                    myCommand.Parameters.AddWithValue("@ftlAvail", carrier.ftlAvail);
                    myCommand.Parameters.AddWithValue("@ltlAvail", carrier.ltlAvail);
                    myCommand.Parameters.AddWithValue("@ftlRate", carrier.ftlRate);
                    myCommand.Parameters.AddWithValue("@ltlRate", carrier.ltlRate);
                    myCommand.Parameters.AddWithValue("@reeferCharge", carrier.carrierID);
                    myCommand.Parameters.AddWithValue("@carrierID", carrier.carrierID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        }

        /// \brief This method InsertCarrier for user 
        /// \details <b>Details</b>
        /// This method will insert carrier for making order
        /// \return  void
        public bool InsertCarrier(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO carrier (carrierID, depotCity, carrierName, ftlAvail, ltlAvail, ftlRate, ltlRate, reeferCharge)
	                                            VALUES (@carrierID, @depotCity, @carrierName, @ftlAvail, @ltlAvail, @ftlRate, @ltlRate, @reeferCharge); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@carrierID", carrier.carrierID);
                    myCommand.Parameters.AddWithValue("@depotCity", carrier.depotCity);
                    myCommand.Parameters.AddWithValue("@carrierName", carrier.carrierName);
                    myCommand.Parameters.AddWithValue("@ftlAvail", carrier.ftlAvail);
                    myCommand.Parameters.AddWithValue("@ltlAvail", carrier.ltlAvail);
                    myCommand.Parameters.AddWithValue("@ftlRate", carrier.ftlRate);
                    myCommand.Parameters.AddWithValue("@ltlRate", carrier.ltlRate);
                    myCommand.Parameters.AddWithValue("@reeferCharge", carrier.reeferCharge);


                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        }

        /// \brief This method DeleteCarrier for user 
        /// \details <b>Details</b>
        /// This method will delete an carrier
        /// \return  void
        public void DeleteCarrier(Carrier carrier)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM carrier WHERE carrierID = @carrierID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@carrierID", carrier.carrierID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        /// \brief This method GetCustomers for user 
        /// \details <b>Details</b>
        /// This method will get customer database 
        /// \return  void
        public string GetLastCarrierID()
        {
            string value = "";
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  SELECT carrierID FROM carrier ORDER BY carrierID DESC LIMIT 1; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                value = (string)myCommand.ExecuteScalar();
            }
            return value;
        }

        /// \brief This method GetCarrierIDbyDepotCity for user 
        /// \details <b>Details</b>
        /// This method will get GetCarrierIDbyDepotCity
        /// \return  void
        public string GetCarrierIDbyDepotCity(string depotCity)
        {
            string value = "";
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  SELECT carrierID FROM carrier WHERE depotCity = @depotCity; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@depotCity", depotCity);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                value = (string)myCommand.ExecuteScalar();
            }
            return value;
        }

        /// \brief This method GetCarriers for user 
        /// \details <b>Details</b>
        /// This method will get a nee carrier
        /// \return  void
        public List<Carrier> GetAvailabilty(string orderID, string carrierID)
        {
            const string sqlStatement = @" SELECT 
                                                planinfo.orderID, 
                                                ordering.carrierID, 
                                                jobType, 
                                                quantity, 
                                                vanType, 
                                                ftlAvail, 
                                                ltlAvail, 
                                                reeferCharge,
                                                depotCity,
                                                ftlRate,
                                                ltlRate,
                                                carrierName
                                            FROM planinfo 
			                                INNER JOIN 
                                                ordering on planinfo.orderID = ordering.orderID
                                            INNER JOIN 
                                                carrier on ordering.carrierID = carrier.carrierID
                                            WHERE planinfo.orderID = @orderID 
                                            AND carrier.carrierID = @carrierID  ; ";

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

                var carriers = DataTableToAvailabilityList(dataTable);

                return carriers;
            }
        }

        public List<Carrier> GetCarriers(string searchItem)
        {
            const string sqlStatement = @" SELECT 
                                                carrierID, 
                                                depotCity, 
                                                carrierName, 
                                                ftlAvail, 
                                                ltlAvail, 
                                                ftlRate,
                                                ltlRate, 
                                                reeferCharge,
                                                cityName
                                            FROM carrier
                                            INNER JOIN city on carrier.depotCity = city.cityID
                                            WHERE city.cityName = @SearchItem ; ";

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

                var carriers = DataTableToCarrierList(dataTable);

                return carriers;
            }
        }

        /// \brief This method DataTableToCarrierList for user 
        /// \details <b>Details</b>
        /// This method will store the database to the carrier list
        /// \return  void
        private List<Carrier> DataTableToCarrierList(DataTable table)
        {
            var carriers = new List<Carrier>();

            foreach (DataRow row in table.Rows)
            {
                carriers.Add(new Carrier
                {
                    carrierID = row["carrierID"].ToString(),
                    depotCity = row["depotCity"].ToString(),
                    carrierName = row["carrierName"].ToString(),
                    ftlAvail = Convert.ToDouble(row["ftlAvail"]),
                    ltlAvail = Convert.ToDouble(row["ltlAvail"]),
                    ftlRate = Convert.ToDouble(row["ftlRate"]),
                    ltlRate = Convert.ToDouble(row["ltlRate"]),
                    reeferCharge = Convert.ToDouble(row["reeferCharge"])
                });
            }

            return carriers;
        }

        private List<Carrier> DataTableToAvailabilityList(DataTable table)
        {
            var carriers = new List<Carrier>();

            foreach (DataRow row in table.Rows)
            {
                carriers.Add(new Carrier
                {
                    orderID = row["orderID"].ToString(),
                    carrierID = row["carrierID"].ToString(),
                    jobType = Convert.ToInt32(row["jobType"]),
                    quantity = Convert.ToInt32(row["quantity"]),
                    vanType = Convert.ToInt32(row["vanType"]),
                    ftlAvail = Convert.ToDouble(row["ftlAvail"]),
                    ltlAvail = Convert.ToDouble(row["ltlAvail"]),

                    reeferCharge = Convert.ToDouble(row["reeferCharge"]),
                    depotCity = row["depotCity"].ToString(),
                    ftlRate = Convert.ToDouble(row["ftlRate"]),
                    ltlRate = Convert.ToDouble(row["ltlRate"]),
                    carrierName = row["carrierName"].ToString()
                });
            }

            return carriers;
        }
    }
}
