﻿//* FILE			: BillingBizDAO.cs
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

namespace TMSProject.Classes.Controller
{
    /*==========================================================================================================================*/
    // Name         : BillingBizDAO
    // Purpose      : contain all the name definitions and relative path of files
    // Description  : this class defines name of BillingBizDAO connect to database and update, insert and delete the billing
    /*==========================================================================================================================*/
    public class BillingBizDAO
    {
        // Connect with the database
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        /* =========================================================================================================================
        * Name		: UpdateBilling																						
        * Purpose	: to UPDATE the billing infor whenever create an new order 	
        * Inputs	: Billing billing		    : an object called billing																												
        * Outputs	: None																											
        * Returns	: None																										
        ===========================================================================================================================*/
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
