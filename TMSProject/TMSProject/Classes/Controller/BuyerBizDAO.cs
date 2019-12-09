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
    public class BuyerBizDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        public bool UpdateBuyer(Buyer buyer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  UPDATE Buyer
	                                            SET buyerPassword = @buyerPassword
	                                            WHERE buyerEmployeeID = @buyerEmployeeID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@buyerPassword", buyer.buyerPassword);
                    myCommand.Parameters.AddWithValue("@adminEmployeeID", buyer.buyerEmployeeID);

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

        public bool InsertBuyer(Buyer buyer)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO buyer (buyerEmployeeID, buyerPassword)
	                                                VALUES (@buyerEmployeeID, @buyerPassword); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@adminEmployeeID", buyer.buyerEmployeeID);
                    myCommand.Parameters.AddWithValue("@adminPassword", buyer.buyerPassword);
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

        public void DeleteBuyer(Buyer buyer)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  DELETE FROM buyer WHERE buyerEmployeeID = @buyerEmployeeID;
												DELETE FROM employee WHERE employeeID = @buyerEmployeeID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@adminEmployeeID", buyer.buyerEmployeeID);

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

        public List<Buyer> GetBuyers(string buyerID, string password)
        {
            const string sqlStatement = @" SELECT 
                                                employeeID, 
                                                employeeType, 
                                                buyerPassword
                                            FROM employee
                                            INNER JOIN buyer ON
                                            employee.employeeID = buyer.buyerEmployeeID
                                            WHERE employee.employeeID = @buyerID 
                                            AND buyer.buyerPassword = @password ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@buyerID", buyerID);
                myCommand.Parameters.AddWithValue("@password", password);

                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var buyers = DataTableToOrderList(dataTable);
                Log.Info("SQL Execute: " + sqlStatement);
                return buyers;
            }
        }

        private List<Buyer> DataTableToOrderList(DataTable table)
        {
            var buyers = new List<Buyer>();

            foreach (DataRow row in table.Rows)
            {
                buyers.Add(new Buyer
                {
                    buyerEmployeeID = row["employeeID"].ToString(),
                    buyerPassword = row["buyerPassword"].ToString(),
                    employeeType = row["employeeType"].ToString()
                });
            }

            return buyers;
        }

    }
}
