//* FILE			: OrderBizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : OrderBizDAO for the biiling infomation



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Model;
using TMSProject.DBConnect;
using TMSProject.Classes.View;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Controls;

namespace TMSProject.Classes.Controller
{
    /// \class InvoiceBizDAO
    /// \brief This class contains the invoice's information for a Order file when buyer make an order
    /// \author : <i>Nhung Luong <i>
    public class MileageBizDAO
    {
        ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        /// \brief This method GetOrders for user 
        /// \details <b>Details</b>
        /// This method will get order when finishing order
        /// \return  void
        public List<Mileage> GetMileages()
        {

            const string sqlStatement = @" SELECT mileageID, 
                                                  startcityID, 
                                                  u1.cityName as startCityName, 
                                                  endCityID, 
                                                  u2.cityName as endCityName, 
                                                  distance, 
                                                  workingTime                                                
                                            FROM mileage 
                                            INNER JOIN city u1 ON mileage.startCityID = u1.cityID
                                            INNER JOIN city u2 ON mileage.endCityID = u2.cityID order by mileageID; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                
                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var mileages = DataTableToMileageList(dataTable);

                return mileages;
            }
        }

        public List<Mileage> GetCitiesMileages(string startCityName, string endCityName)
        {

            const string sqlStatement = @" SELECT mileageID, 
                                                  startcityID, 
                                                  u1.cityName as startCityName, 
                                                  endCityID, 
                                                  u2.cityName as endCityName, 
                                                  distance, 
                                                  workingTime                                                
                                            FROM mileage 
                                            INNER JOIN city u1 ON mileage.startCityID = u1.cityID
                                            INNER JOIN city u2 ON mileage.endCityID = u2.cityID 
                                            WHERE u1.cityName=@startCityName or u2.cityName=@endCityName order by mileageID; ";

            using (var myConn = new MySqlConnection(connectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@startCityName", startCityName);
                myCommand.Parameters.AddWithValue("@endCityName", endCityName);

                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var mileages = DataTableToMileageList(dataTable);

                return mileages;
            }
        }

        /// \brief This method DataTableToOrderList for user 
        /// \details <b>Details</b>
        /// This method will store order when finishing order
        /// \return  void
        private List<Mileage> DataTableToMileageList(DataTable table)
        {
            var mileages = new List<Mileage>();

            foreach (DataRow row in table.Rows)
            {
                mileages.Add(new Mileage
                {
                    mileageID = row["mileageID"].ToString(),
                    startCityID = row["startCityID"].ToString(),
                    startCityName = row["startCityName"].ToString(),
                    endCityID = row["endCityID"].ToString(),
                    endCityName = row["endCityName"].ToString(),
                    distance = Convert.ToDouble(row["distance"]),
                    workingTime = Convert.ToDouble(row["workingTime"])
                });
            }

            return mileages;
        }
    }
}
