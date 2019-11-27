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
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
        public void UpdateBilling(Billing billing)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", billing.billingID);
                myCommand.Parameters.AddWithValue("@CategoryId", billing.orderID);
                myCommand.Parameters.AddWithValue("@UnitPrice", billing.planID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", billing.customerID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", billing.totalAmount);
                
                myConn.Open();

                myCommand.ExecuteNonQuery();
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
        public void InsertBilling(Billing billing)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", billing.billingID);
                myCommand.Parameters.AddWithValue("@CategoryId", billing.orderID);
                myCommand.Parameters.AddWithValue("@UnitPrice", billing.planID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", billing.customerID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", billing.totalAmount);

                myConn.Open();

                myCommand.ExecuteNonQuery();
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
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", billing.billingID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
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
        public List<Billing> GetBillings(string searchItem)
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

                var billings = DataTableToBillingList(dataTable);

                return billings;
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
            var billings = new List<Billing>();

            foreach (DataRow row in table.Rows)
            {
                billings.Add(new Billing
                {
                    billingID = row["billingID"].ToString(),
                    orderID = row["orderID"].ToString(),
                    planID = row["planID"].ToString(),
                    customerID = row["customerID"].ToString(),
                    totalAmount = Convert.ToDouble(row["totalAmount"])
                   
            });
            }

            return billings;
        }
    }
}
