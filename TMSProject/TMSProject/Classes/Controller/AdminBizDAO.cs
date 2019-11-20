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
    public class AdminBizDAO
    {   
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

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;

                }
            }

        }

        public void DeleteAdmin(Admin admin)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM admin WHERE adminEmployeeID = @adminEmployeeID;
												DELETE FROM employee WHERE employeeID = @adminEmployeeID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@adminEmployeeID", admin.adminEmployeeID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        public List<Admin> GetAdmins(string adminID, string password)
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

                return admins;
            }
        }

        private List<Admin> DataTableToOrderList(DataTable table)
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

            return admins;
        }

    }
}