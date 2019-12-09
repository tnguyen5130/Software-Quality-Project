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
    public class AdminBizDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        public bool UpdateAdmin(Admin admin)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  UPDATE Admin
	                                            SET adminPassword = @adminPassword
	                                            WHERE adminEmployeeID = @adminEmployeeID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@adminPassword", admin.adminPassword);
                    myCommand.Parameters.AddWithValue("@adminEmployeeID", admin.adminEmployeeID);

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


        public bool InsertAdmin(Admin admin)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO admin (adminEmployeeID, adminPassword)
	                                                VALUES (@adminEmployeeID, @adminPassword); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@adminEmployeeID", admin.adminEmployeeID);
                    myCommand.Parameters.AddWithValue("@adminPassword", admin.adminPassword);
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

        public void DeleteAdmin(Admin admin)
        {
            try
            {

                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  DELETE FROM admin WHERE adminEmployeeID = @adminEmployeeID;
												DELETE FROM employee WHERE employeeID = @adminEmployeeID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@adminEmployeeID", admin.adminEmployeeID);

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

        public List<Admin> GetAdmins(string adminID, string password)
        {
            try
            {
                const string sqlStatement = @" SELECT 
                                                employeeID, 
                                                employeeType, 
                                                adminPassword
                                            FROM employee
                                            INNER JOIN admin ON
                                            employee.employeeID = admin.adminEmployeeID
                                            WHERE employee.employeeID = @adminID 
                                            and admin.adminPassword = @password ";

                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@adminID", adminID);
                    myCommand.Parameters.AddWithValue("@password", password);

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var dataTable = new DataTable();

                    myAdapter.Fill(dataTable);

                    var admins = DataTableToOrderList(dataTable);
                    Log.Info("SQL Execute: " + sqlStatement);
                    return admins;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
            
        }

        private List<Admin> DataTableToOrderList(DataTable table)
        {
            try
            {
                var admins = new List<Admin>();

                foreach (DataRow row in table.Rows)
                {
                    admins.Add(new Admin
                    {
                        adminEmployeeID = row["employeeID"].ToString(),
                        adminPassword = row["adminPassword"].ToString(),
                        employeeType = row["employeeType"].ToString()
                    });
                }
                Log.Info("ResultSet Execute!!!");
                return admins;
                
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }
            
        }
    }
}