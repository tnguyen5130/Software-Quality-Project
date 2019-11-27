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

namespace TMSProject.Classes.Controller
{

    /// \class ContractBizDAO
    /// \brief This class contains the contract's information for a billing file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class ContractBizDAO
    {   
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        /// \brief This method UpdateContract for user 
        /// \details <b>Details</b>
        /// This method will update contract database 
        /// \return  void
        public void UpdateContract(Contract contract)

        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  UPDATE contract
	                                            SET InitiateBy = @initiateBy,
                                                    startDate = @startDate,
		                                            endDate = @endDate,
                                                    completeStatus = @completeStatus
	                                            WHERE contractID = @contractID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@initiateBy", contract.initiateBy);
                    myCommand.Parameters.AddWithValue("@startDate", contract.startDate);
                    myCommand.Parameters.AddWithValue("@endDate", contract.endDate);
                    myCommand.Parameters.AddWithValue("@completeStatus", contract.completeStatus);
                    myCommand.Parameters.AddWithValue("@contractID", contract.contractID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }


        /// \brief This method InsertContract for user 
        /// \details <b>Details</b>
        /// This method will insert contract database 
        /// \return  void
        public bool InsertContract(Contract contract)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO contract (contractID, initiateBy, startDate, endDate, completeStatus)
	                                            VALUES (@contractID, @initiateBy, @startDate, @endDate, @completeStatus); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@ProductID", contract.contractID);
                    myCommand.Parameters.AddWithValue("@CategoryId", contract.initiateBy);
                    myCommand.Parameters.AddWithValue("@UnitPrice", contract.startDate);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", contract.endDate);
                    myCommand.Parameters.AddWithValue("@UnitsInStock", contract.completeStatus);

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



        /// \brief This method DeleteContract for user 
        /// \details <b>Details</b>
        /// This method will update CMP database 
        /// \return  void
        public void DeleteContract(Contract contract)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", contract.contractID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        /// \brief This method GetContracts for user 
        /// \details <b>Details</b>
        /// This method will get the contract form database
        /// \return  void
        public List<Contract> GetContracts(string contractID)
        {
            const string sqlStatement = @" SELECT 
                                                contractID, 
                                                InitiateBy, 
                                                startDate, 
                                                endDate, 
                                                completeStatus
                                            FROM contract
                                            WHERE contractID = @contractID; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@contractID", contractID);

                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var contracts = DataTableToContractList(dataTable);

                return contracts;
            }
        }


        /// \brief This method DataTableToContractList for user 
        /// \details <b>Details</b>
        /// This method will store cotract into contract list data
        /// \return  void
        private List<Contract> DataTableToContractList(DataTable table)
        {
            var contracts = new List<Contract>();

            foreach (DataRow row in table.Rows)
            {
                contracts.Add(new Contract
                {
                    contractID = row["contractID"].ToString(),
                    initiateBy = row["initiateBy"].ToString(),
                    startDate = row["startDate"].ToString(),
                    endDate = row["endDate"].ToString(),
                    completeStatus = row["completeStatus"].ToString()
            });
            }

            return contracts;
        }
    }
}
