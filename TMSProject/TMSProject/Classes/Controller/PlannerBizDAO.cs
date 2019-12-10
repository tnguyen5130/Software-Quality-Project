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
    public class PlannerBizDAO
    {
        private static readonly log4net.ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        public bool UpdatePlanner(Planner planner)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  UPDATE Planner
	                                            SET plannerPassword = @plannerPassword
	                                            WHERE plannerEmployeeID = @plannerEmployeeID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@plannerPassword", planner.plannerPassword);
                    myCommand.Parameters.AddWithValue("@plannerEmployeeID", planner.plannerEmployeeID);

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


        public bool InsertPlanner(Planner planner)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO planner (plannerEmployeeID, plannerPassword)
	                                                VALUES (@plannerEmployeeID, @plannerPassword); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@plannerEmployeeID", planner.plannerEmployeeID);
                    myCommand.Parameters.AddWithValue("@plannerPassword", planner.plannerPassword);
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

        public void DeletePlanner(Planner planner)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  DELETE FROM planner WHERE plannerEmployeeID = @plannerEmployeeID;
												DELETE FROM employee WHERE employeeID = @plannerEmployeeID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@plannerEmployeeID", planner.plannerEmployeeID);

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

        public List<Planner> GetPlanners(string plannerID, string password)
        {
            try
            {
                const string sqlStatement = @" SELECT 
                                                employeeID, 
                                                employeeType, 
                                                plannerPassword
                                            FROM employee
                                            INNER JOIN planner ON
                                            employee.employeeID = planner.plannerEmployeeID
                                            WHERE employee.employeeID = @plannerID 
                                            AND planner.plannerPassword = @password ";

                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@plannerID", plannerID);
                    myCommand.Parameters.AddWithValue("@password", password);

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var dataTable = new DataTable();

                    myAdapter.Fill(dataTable);

                    var planners = DataTableToOrderList(dataTable);
                    Log.Info("SQL Execute: " + sqlStatement);
                    return planners;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }

        }

        private List<Planner> DataTableToOrderList(DataTable table)
        {
            try
            {
                var planners = new List<Planner>();

                foreach (DataRow row in table.Rows)
                {
                    planners.Add(new Planner
                    {
                        plannerEmployeeID = row["employeeID"].ToString(),
                        plannerPassword = row["plannerPassword"].ToString(),
                        employeeType = row["employeeType"].ToString()
                    });
                }

                Log.Info("ResultSet Execute!!!");
                return planners;
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }

        }

    }
}