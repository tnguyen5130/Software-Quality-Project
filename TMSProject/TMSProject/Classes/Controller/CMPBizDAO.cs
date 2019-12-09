//* FILE			: CMPBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : CityBizDAO for the biiling infomation



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
    /// \class CMPBizDAO
    /// \brief This class contains the CMP's information for a billing file when buyer make an order
    /// \author : <i>nhung Luong<i>
    public class CMPBizDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";



        /// \brief This method UpdateCMP for user 
        /// \details <b>Details</b>
        /// This method will update CMP database 
        /// \return  void
        public void UpdateCMP(ContractMarketPlace cmp)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@ProductID", cmp.customerID);
                    myCommand.Parameters.AddWithValue("@CategoryId", cmp.contractID);
                    myCommand.Parameters.AddWithValue("@UnitPrice", cmp.jobType);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.quantity);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.origin);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.destination);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.vanType);

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



        /// \brief This method UpdateCity for user 
        /// \details <b>Details</b>
        /// This method will update city from CMP
        /// \return  void
        public void InsertCMP(ContractMarketPlace cmp)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@ProductID", cmp.customerID);
                    myCommand.Parameters.AddWithValue("@CategoryId", cmp.contractID);
                    myCommand.Parameters.AddWithValue("@UnitPrice", cmp.jobType);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.quantity);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.origin);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.destination);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", cmp.vanType);

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



        /// \brief This method DeleteCMP for user 
        /// \details <b>Details</b>
        /// This method will delete CMP for selecting start and end city
        /// \return  void
        public void DeleteCMP(ContractMarketPlace cmp)
        {
            try
            {
                using (var myConn = new MySqlConnection(connectionString))
                {
                    const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@ProductID", cmp.customerID);

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

        public List<ContractMarketPlace> GetCMPs(string searchItem)
        {
            try
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

                    var cmps = DataTableToCMPList(dataTable);
                    Log.Info("SQL Execute: " + sqlStatement);
                    return cmps;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SQL Error" + ex.Message);
                return null;
            }
            
        }


        /// \brief This method DataTableToCMPList for user 
        /// \details <b>Details</b>
        /// This method will store CMP database
        /// \return  void
        private List<ContractMarketPlace> DataTableToCMPList(DataTable table)
        {
            try
            {
                var orders = new List<ContractMarketPlace>();

                foreach (DataRow row in table.Rows)
                {
                    orders.Add(new ContractMarketPlace
                    {
                        customerID = row["customerID"].ToString(),
                        contractID = row["contractID"].ToString(),
                        jobType = row["jobType"].ToString(),
                        quantity = Convert.ToInt32(row["quantity"]),
                        origin = row["origin"].ToString(),
                        destination = row["destination"].ToString(),
                        vanType = row["vanType"].ToString()
                    });
                }
                Log.Info("ResultSet Execute!!!");
                return orders;
                
            }
            catch (Exception ex)
            {
                Log.Error("ResultSet Error: " + ex.Message);
                return null;
            }
            
        }
    }
}
