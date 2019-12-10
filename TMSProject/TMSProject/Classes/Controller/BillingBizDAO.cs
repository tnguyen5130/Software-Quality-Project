//* FILE			: BillingBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : BillingBizDAO for the biiling infomation 


using System;
using System.Collections.Generic;
using System.Configuration;
using TMSProject.Classes.Model;
using MySql.Data.MySqlClient;
using System.Data;
using TMSProject.DBConnect;
using log4net;

/// \namespace TMSProject.Classes.Controller
/// \brief The purpose of this namespace is to create a handle billing menu option. 
/// \details This namespace has 13 classes <b>Admin</b>, <b>Billing</b>, <b>Buyer</b>, <b>Carrier</b>, <b>City</b>, <b>Contract</b>
/// <b>ContractMarketPlace</b>, <b>Customer</b>,<b>Employee</b>, <b>Invoice</b>, <b>Order</b>, <b>PlanInfo</b>, <b>Trip</b>, <b>Planner</b>
/// \author : <i>Nhung Luong<i>
namespace TMSProject.Classes.Controller
{
    /*==========================================================================================================================*/
    // Name         : BillingBizDAO
    // Purpose      : contain all the name definitions and relative path of files
    // Description  : this class defines name of BillingBizDAO connect to database and update, insert and delete the billing
    /*==========================================================================================================================*/

    /// \class BillingBizDAO
    /// \brief This class contains the billing's information for a billing file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class BillingBizDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";


        /* =========================================================================================================================
        * Name		: UpdateBilling																						
        * Purpose	: to UPDATE the billing infor whenever create an new order 	
        * Inputs	: Billing billing		    : an object called billing																												
        * Outputs	: None																											
        * Returns	: None																										
        ===========================================================================================================================*/
        /// \brief This method UpdateBilling for user 
        /// \details <b>Details</b>
        /// This method will update billing infomation when finishing an order
        /// \return  void
        public bool UpdateBilling(Billing billing)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  UPDATE billing
	                                            SET orderID = @orderID,
                                                    planID = @planID,
		                                            customerID = @customerID, 
		                                            totalAmount = @totalAmount
	                                            WHERE billingID = @billingID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@CategoryId", billing.orderID);
                    myCommand.Parameters.AddWithValue("@UnitPrice", billing.planID);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", billing.customerID);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", billing.totalAmount);
                    myCommand.Parameters.AddWithValue("@ProductID", billing.billingID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    Log.Info(sqlStatement);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return false;
            }
        }

        /* =========================================================================================================================
        * Name		: InsertBilling																						
        * Purpose	: to INSERT the billing infor whenever create an new order 	
        * Inputs	: Billing billing		    : an object called billing																												
        * Outputs	: None																											
        * Returns	: None																										
        ===========================================================================================================================*/

        /// \brief This method InsertBilling for user
        /// \details <b>Details</b>
        /// This method will insert billing infomation when into order details and invoice
        /// \return  void
        public bool InsertBilling(Billing billing)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO billing (billingID, orderID, planID, customerID, totalAmount)
	                                            VALUES (@billingID, @orderID, @planID, @customerID, @totalAmount); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@billingID", billing.billingID);
                    myCommand.Parameters.AddWithValue("@orderID", billing.orderID);
                    myCommand.Parameters.AddWithValue("@planID", billing.planID);
                    myCommand.Parameters.AddWithValue("@customerID", billing.customerID);
                    myCommand.Parameters.AddWithValue("@totalAmount", billing.totalAmount);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    Log.Info(sqlStatement);
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

        /* =========================================================================================================================
        * Name		: DeleteBilling																						
        * Purpose	: to DELETE the billing infor whenever create an new order 	
        * Inputs	: Billing billing		    : an object called billing																												
        * Outputs	: None																											
        * Returns	: None																										
        ===========================================================================================================================*/
        /// \brief This method DeleteBilling for user
        /// \details <b>Details</b>
        /// This method will delete billing infomation when into order details and invoice
        /// \return  void
        public void DeleteBilling(Billing billing)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@ProductID", billing.billingID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    Log.Info(sqlStatement);
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
            }

        }

        public List<Billing> GetBillingID(string planID, string orderID)
        {
            try
            {
                const string sqlStatement = @" SELECT 
                                                billingID
                                            FROM billing 
                                            WHERE orderID = @orderID
                                            AND planID = @planID; ";

                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@planID", planID);
                    myCommand.Parameters.AddWithValue("@orderID", orderID);

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var dataTable = new DataTable();

                    myAdapter.Fill(dataTable);

                    var billings = DataTableToBillingIDList(dataTable);

                    Log.Info(sqlStatement);
                    return billings;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }

        }

        /* =========================================================================================================================
        * Name		: GetBillings																						
        * Purpose	: to GET the billing infor whenever create an new order 	
        * Inputs	: Billing billing		    : an object called billing																												
        * Outputs	: None																											
        * Returns	: None																										
        ===========================================================================================================================*/
        /// \brief This method GetBillings for user
        /// \details <b>Details</b>
        /// This method will get billing infomation when into order details and invoice
        /// \return  void
        public List<Billing> GetBillings(string planID, string orderID)
        {
            try
            {
                const string sqlStatement = @" SELECT 
                                                planID, 
                                                planinfo.orderID, 
                                                customerID,
                                                workingTime, 
                                                distance, 
                                                jobType, 
                                                quantity, 
                                                vanType, 
                                                ftlRate, 
                                                ltlRate, 
                                                reeferCharge
                                            FROM planinfo 
	                                        INNER JOIN 
                                                ordering on planinfo.orderID = ordering.orderID
                                            INNER JOIN 
                                                carrier on ordering.carrierID = carrier.carrierID 
                                            WHERE ordering.orderID = @orderID
                                            AND planID = @planID; ";

                using (var myConn = new MySqlConnection(connectionString))
                {

                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@planID", planID);
                    myCommand.Parameters.AddWithValue("@orderID", orderID);

                    //For offline connection we weill use  MySqlDataAdapter class.  
                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var dataTable = new DataTable();

                    myAdapter.Fill(dataTable);

                    var billings = DataTableToBillingList(dataTable);

                    Log.Info(sqlStatement);
                    return billings;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }

        }


        /* =========================================================================================================================
        * Name		: DataTableToBillingList																						
        * Purpose	: to STORE the billing infor whenever create an new order 	
        * Inputs	: Billing billing		    : an object called billing																												
        * Outputs	: None																											
        * Returns	: None																										
        ===========================================================================================================================*/
        /// \brief This method GetBillings for user
        /// \details <b>Details</b>
        /// This method will get billing infomation when into order details and invoice
        /// \return  void
        private List<Billing> DataTableToBillingList(DataTable table)
        {
            try
            {
                var billings = new List<Billing>();

                foreach (DataRow row in table.Rows)
                {
                    billings.Add(new Billing
                    {
                        //billingID = row["billingID"].ToString(),
                        orderID = row["orderID"].ToString(),
                        planID = row["planID"].ToString(),
                        customerID = row["customerID"].ToString(),
                        workingTime = Convert.ToDouble(row["workingTime"]),
                        distance = Convert.ToDouble(row["distance"]),
                        jobType = Convert.ToInt32(row["jobType"]),
                        quantity = Convert.ToInt32(row["quantity"]),
                        vanType = Convert.ToInt32(row["vanType"]),
                        ftlRate = Convert.ToDouble(row["ftlRate"]),
                        ltlRate = Convert.ToDouble(row["ltlRate"]),
                        reeferCharge = Convert.ToDouble(row["reeferCharge"]),

                    });
                }

                Log.Info("ResultSet Execute!!!");
                return billings;
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }
        }

        private List<Billing> DataTableToBillingIDList(DataTable table)
        {
            try
            {
                var billings = new List<Billing>();

                foreach (DataRow row in table.Rows)
                {
                    billings.Add(new Billing
                    {
                        billingID = row["billingID"].ToString()
                    });
                }

                Log.Info("ResultSet Execute!!!");
                return billings;
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }
        }
    }
}
