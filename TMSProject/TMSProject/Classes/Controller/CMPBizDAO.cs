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
using System.Windows.Controls;

namespace TMSProject.Classes.Controller
{
    /// \class CMPBizDAO
    /// \brief This class contains the CMP's information for a billing file when buyer make an order
    /// \author : <i>nhung Luong<i>
    public class CMPBizDAO
    {
        /// <summary>
        /// Remote DB connectionString
        /// </summary>
        private string connectionString = "server=" + CMPConfigs.dbServer + ";user id=" + CMPConfigs.dbUID + ";password=" + CMPConfigs.dbPassword + ";database=" + CMPConfigs.dbDatabase + ";SslMode=none";
        /// <summary>
        /// Local DB connectionString
        /// </summary>
        private string connectionStringLocal = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";


        /// \brief This method UpdateCMP for user 
        /// \details <b>Details</b>
        /// This method will update CMP database 
        /// \return  void
        public void UpdateCMP(ContractMarketPlace cmp)
        {
            using (var myConn = new MySqlConnection(connectionStringLocal))
            {
                const string sqlStatement = @"  UPDATE contract_market_place
	                                            SET jobType = @jobType,
                                                    quantity = @quantity,
		                                            origin = @origin,
                                                    destination = @destination,
                                                    vanType = @vanType
	                                            WHERE customerID = @customerID,
                                                      contractID = @contractID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@customerID", cmp.customerID);
                myCommand.Parameters.AddWithValue("@contractID", cmp.contractID);
                myCommand.Parameters.AddWithValue("@jobType", cmp.jobType);
                myCommand.Parameters.AddWithValue("@quantity", cmp.quantity);
                myCommand.Parameters.AddWithValue("@origin", cmp.origin);
                myCommand.Parameters.AddWithValue("@destination", cmp.destination);
                myCommand.Parameters.AddWithValue("@vanType", cmp.vanType);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        /// \brief This method GetLastCustomerId for user 
        /// \details <b>Details</b>
        /// This method will get GetLastCustomerId from database 
        /// \return  void
        public string GetLastCustomerId(ContractMarketPlace cmp)
        {
            string value = "";
            using (var myConn = new MySqlConnection(connectionStringLocal))
            {
                const string sqlStatement = @"SELECT customerID FROM contract_market_place ORDER BY customerID DESC LIMIT 1; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myConn.Open();

                myCommand.ExecuteNonQuery();
                value = (string)myCommand.ExecuteScalar();
            }
            return value;
        }

        /// \brief This method InsertCMP for user 
        /// \details <b>Details</b>
        /// This method will insert data into CMP
        /// \return  void
        public void InsertCMP(ContractMarketPlace cmp)
        {
            using (var myConn = new MySqlConnection(connectionStringLocal))
            {
                const string sqlStatement = @"  INSERT INTO contract_market_place (customerID, contractID, jobType, quantity, origin, destination, vanType)
	                                            VALUES (@customerID, @contractID, @jobType, @quantity, @origin, @destination, @vanType); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@customerID", cmp.customerID);
                myCommand.Parameters.AddWithValue("@contractID", cmp.contractID);
                myCommand.Parameters.AddWithValue("@jobType", cmp.jobType);
                myCommand.Parameters.AddWithValue("@quantity", cmp.quantity);
                myCommand.Parameters.AddWithValue("@origin", cmp.origin);
                myCommand.Parameters.AddWithValue("@destination", cmp.destination);
                myCommand.Parameters.AddWithValue("@vanType", cmp.vanType);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

        }

        /// \brief This method DeleteCMP for user 
        /// \details <b>Details</b>
        /// This method will delete CMP for selecting customerID and contractID
        /// \return  void
        public void DeleteCMP(ContractMarketPlace cmp)
        {
            using (var myConn = new MySqlConnection(connectionStringLocal))
            {
                const string sqlStatement = @"  DELETE FROM contract_market_place WHERE customerID = @customerID AND contractID = @contractID;";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@customerID", cmp.customerID);
                myCommand.Parameters.AddWithValue("@contractID", cmp.contractID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        /// \brief This method LoadOrderList for user 
        /// \details <b>Details</b>
        /// This method will get order list that has orderID and orderDate into DataGrid table
        /// \return  void
        public void LoadCMPList(DataGrid grid)
        {
            const string sqlStatement = @" SELECT Client_Name, Job_Type, Quantity, Origin, Destination, Van_Type FROM Contract;";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                DataTable dataTable = new DataTable("Contract");

                myAdapter.Fill(dataTable);

                grid.ItemsSource = dataTable.DefaultView;
            }
        }

        /// \brief This method GetCMPs for user 
        /// \details <b>Details</b>
        /// This method will get CMP list that is based on searchItem
        /// \return List<ContractMarketPlace>
        public List<ContractMarketPlace> GetCMPs(string searchItem)
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


            using (var myConn = new MySqlConnection(connectionStringLocal))
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

                return cmps;
            }
        }

        /// \brief This method DataTableToCMPList for user 
        /// \details <b>Details</b>
        /// This method will store CMP database
        /// \return  void
        private List<ContractMarketPlace> DataTableToCMPList(DataTable table)
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

            return orders;
        }
    } // end of class
} // end of namespace
