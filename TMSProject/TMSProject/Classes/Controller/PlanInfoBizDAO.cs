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

namespace TMSProject.Classes.Controller
{
    /// \class PlanInfoBizDAO
    /// \brief This class contains the plan's information for a Order file when buyer make an order
    /// \author : <i>Nhung Luong <i>
    public class PlanInfoBizDAO
    {
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";



        /// \brief This method UpdatePlanInfo for user 
        /// \details <b>Details</b>
        /// This method will update plan info when finishing order
        /// \return  void
        public void UpdatePlanInfo(PlanInfo plan)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  UPDATE products
	                                            SET CategoryId = @CategoryId,
                                                    UnitPrice = @UnitPrice,
		                                            UnitsInStock = @UnitsInStock
	                                            WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", plan.planID);
                myCommand.Parameters.AddWithValue("@CategoryId", plan.tripID);
                myCommand.Parameters.AddWithValue("@UnitPrice", plan.startCityID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", plan.endCityID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", plan.workingTime);
                myCommand.Parameters.AddWithValue("@UnitsInStock", plan.distance);
                
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        /// \brief This method InsertPlanInfo for user 
        /// \details <b>Details</b>
        /// This method will insert plan info when finishing order
        /// \return  void
        public void InsertPlanInfo(PlanInfo plan)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  INSERT INTO products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	                                            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", plan.planID);
                myCommand.Parameters.AddWithValue("@CategoryId", plan.tripID);
                myCommand.Parameters.AddWithValue("@UnitPrice", plan.startCityID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", plan.endCityID);
                myCommand.Parameters.AddWithValue("@UnitsInStock", plan.workingTime);
                myCommand.Parameters.AddWithValue("@UnitsInStock", plan.distance);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }


        /// \brief This method DeletePlanInfo for user 
        /// \details <b>Details</b>
        /// This method will delete plan info when finishing order
        /// \return  void
        public void DeletePlanInfo(PlanInfo plan)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", plan.planID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }


        /// \brief This method GetPlanInfo for user 
        /// \details <b>Details</b>
        /// This method will get plan info when finishing order
        /// \return  void
        public List<PlanInfo> GetPlanInfo(string searchItem)
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

                var plans = DataTableToPlanInfoList(dataTable);

                return plans;
            }
        }


        /// \brief This method DataTableToPlanInfoList for user 
        /// \details <b>Details</b>
        /// This method will store plan info when finishing order
        /// \return  void
        private List<PlanInfo> DataTableToPlanInfoList(DataTable table)
        {
            var plans = new List<PlanInfo>();

            foreach (DataRow row in table.Rows)
            {
                plans.Add(new PlanInfo
                {
                    planID = row["planID"].ToString(),
                    tripID = row["tripID"].ToString(),
                    startCityID = row["startCityID"].ToString(),
                    endCityID = row["endCityID"].ToString(),
                    workingTime = Convert.ToDouble(row["workingTime"]),
                    distance = Convert.ToDouble(row["distance"])
            });
            }

            return plans;
        }
    }
}
