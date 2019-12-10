//* FILE			: PlanInfoBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : PlanInfoBizDAO for the biiling infomation



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
using log4net;

namespace TMSProject.Classes.Controller
{
    /// \class PlanInfoBizDAO
    /// \brief This class contains the plan's information for a Order file when buyer make an order
    /// \author : <i>Nhung Luong <i>
    public class PlanInfoBizDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        /// \brief This method UpdatePlanInfo for user 
        /// \details <b>Details</b>
        /// This method will update plan info when finishing order
        /// \return  void
        public bool UpdatePlanInfo(PlanInfo plan)
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

                    myCommand.Parameters.AddWithValue("@ProductID", plan.planID);
                    //myCommand.Parameters.AddWithValue("@CategoryId", plan.tripID);
                    myCommand.Parameters.AddWithValue("@UnitPrice", plan.startCityID);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", plan.endCityID);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", plan.workingTime);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", plan.distance);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    Log.Info("SQL Execute: " + sqlStatement);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Log.Error("SQL Error" + ex.Message);
                    return false;
                }
            }
        }

        /// \brief This method InsertPlanInfo for user 
        /// \details <b>Details</b>
        /// This method will insert plan info when finishing order
        /// \return  void
        public bool InsertPlanInfo(PlanInfo plan)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @" INSERT INTO planinfo (planID, orderID, startCityID, endCityID, workingTime, distance)
	                                            VALUES (@planID, @orderID, @startCityID, @endCityID, @workingTime, @distance); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@planID", plan.planID);
                    myCommand.Parameters.AddWithValue("@orderID", plan.orderID);
                    myCommand.Parameters.AddWithValue("@startCityID", plan.startCityID);
                    myCommand.Parameters.AddWithValue("@endCityID", plan.endCityID);
                    myCommand.Parameters.AddWithValue("@workingTime", plan.workingTime);
                    myCommand.Parameters.AddWithValue("@distance", plan.distance);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    Log.Info("SQL Execute: " + sqlStatement);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Log.Error("SQL Error" + ex.Message);
                    return false;
                }
            }

        }


        /// \brief This method DeletePlanInfo for user 
        /// \details <b>Details</b>
        /// This method will delete plan info when finishing order
        /// \return  void
        public void DeletePlanInfo(PlanInfo plan)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@ProductID", plan.planID);

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

        /// \brief This method GetPlanInfo for user 
        /// \details <b>Details</b>
        /// This method will get plan info when finishing order
        /// \return  void
        public List<PlanInfo> GetPlanInfos(string orderID)
        {
            try
            {
                const string sqlStatement = @" SELECT 
                                                trip.orderID, 
                                                sum(distance) as distance, 
                                                sum(workingTime) as workingTime, 
                                                originalCityID, 
                                                desCityID 
                                            FROM trip
                                            INNER JOIN 
                                                mileage on trip.startCity = mileage.startCityID and trip.endCity = mileage.endCityID
                                            INNER JOIN 
                                                ordering on trip.orderID = ordering.orderID
                                            WHERE trip.orderID = @orderID
                                            GROUP BY trip.orderID; ";

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

                    var plans = DataTableToPlanInfoList(dataTable);

                    Log.Info("SQL Execute: " + sqlStatement);
                    return plans;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
        }

        public List<PlanInfo> GetPlanID(string orderID)
        {
            try
            {
                const string sqlStatement = @" SELECT 
                                                planID, 
                                                orderID, 
                                                distance, 
                                                workingTime, 
                                                startCityID, 
                                                endCityID 
                                            FROM planinfo
                                            WHERE orderID = @orderID; ";

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

                    var plans = DataTableToPlanIDList(dataTable);

                    Log.Info("SQL Execute: " + sqlStatement);
                    return plans;
                }

            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }

        }

        /// \brief This method DataTableToPlanInfoList for user 
        /// \details <b>Details</b>
        /// This method will store plan info when finishing order
        /// \return  void
        private List<PlanInfo> DataTableToPlanInfoList(DataTable table)
        {
            try
            {
                var plans = new List<PlanInfo>();

                foreach (DataRow row in table.Rows)
                {
                    plans.Add(new PlanInfo
                    {
                        orderID = row["orderID"].ToString(),
                        distance = Convert.ToDouble(row["distance"]),
                        workingTime = Convert.ToDouble(row["workingTime"]),
                        startCityID = row["originalCityID"].ToString(),
                        endCityID = row["desCityID"].ToString(),
                    });
                }

                Log.Info("ResultSet Execute!!!");
                return plans;
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }

        }

        /// \brief This method DataTableToPlanInfoList for user 
        /// \details <b>Details</b>
        /// This method will store plan info when finishing order
        /// \return  void
        private List<PlanInfo> DataTableToPlanIDList(DataTable table)
        {
            try
            {
                var plans = new List<PlanInfo>();

                foreach (DataRow row in table.Rows)
                {
                    plans.Add(new PlanInfo
                    {
                        planID = row["planID"].ToString(),
                        orderID = row["orderID"].ToString(),
                        distance = Convert.ToDouble(row["distance"]),
                        workingTime = Convert.ToDouble(row["workingTime"]),
                        startCityID = row["startCityID"].ToString(),
                        endCityID = row["endCityID"].ToString()
                    });
                }

                Log.Info("ResultSet Execute!!!");
                return plans;
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }


        }
    }
}
