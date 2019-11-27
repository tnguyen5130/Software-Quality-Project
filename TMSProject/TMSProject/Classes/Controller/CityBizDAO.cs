//* FILE			: CityBizDAO.cs
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
    /// \class CarrierBizDAO
    /// \brief This class contains the City's information for a billing file when buyer make an order
    /// \author : <i>nhung Luong<i>
    public class CityBizDAO
    {       
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        /// \brief This method UpdateCity for user 
        /// \details <b>Details</b>
        /// This method will update city for select ting start and end city
        /// \return  void
        public bool UpdateCity(City city)

        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  UPDATE city
	                                            SET cityName = @cityName
	                                            WHERE cityID = @cityID; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@cityName", city.cityName);
                    myCommand.Parameters.AddWithValue("@cityID", city.cityID);

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

        /// \brief This method InsertCarrier for user 
        /// \details <b>Details</b>
        /// This method will insert city for selecting start and end city
        /// \return  void
        public bool InsertCity(City city)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                try
                {
                    const string sqlStatement = @"  INSERT INTO city (cityID, cityName)
	                                            VALUES (@cityID, @cityName); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@cityID", city.cityID);
                    myCommand.Parameters.AddWithValue("@cityName", city.cityName);

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


        /// \brief This method DeleteCity for user 
        /// \details <b>Details</b>
        /// This method will delete city for selecting start and end city
        /// \return  void
        public void DeleteCity(City city)
        {
            using (var myConn = new MySqlConnection(connectionString))
            {
                const string sqlStatement = @"  DELETE FROM orderdetails WHERE ProductID = @ProductID;
												DELETE FROM products WHERE ProductID = @ProductID; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@ProductID", city.cityID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
        }

        /// \brief This method GetCities for user 
        /// \details <b>Details</b>
        /// This method will get city for select ting start and end city
        /// \return  void
        public List<City> GetCities(string searchItem)
        {
            const string sqlStatement = @" SELECT 
                                                cityId, 
                                                cityName 
                                            FROM city
                                            WHERE cityName = @SearchItem; ";

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

                var cities = DataTableToCityList(dataTable);

                return cities;
            }
        }

        /// \brief This method DataTableToCityList for user 
        /// \details <b>Details</b>
        /// This method will store city for select ting start and end city
        /// \return  void
        private List<City> DataTableToCityList(DataTable table)
        {
            var cities = new List<City>();

            foreach (DataRow row in table.Rows)
            {
                cities.Add(new City
                {
                    cityID = row["cityID"].ToString(),
                    cityName = row["cityName"].ToString(),
                    
                });
            }

            return cities;
        }

        public void getCityNameList(ComboBox box)
        {

            const string sqlStatement = @" SELECT 
                                                cityName 
                                            FROM city; ";

            using (var myConn = new MySqlConnection(connectionString))
            {
                myConn.Open();
                var myCommand = new MySqlCommand(sqlStatement, myConn);

                //For offline connection we weill use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                MySqlDataReader dr = myCommand.ExecuteReader();             

                while (dr.Read())
                {
                    City city = new City();
                    city.cityName = dr.GetString("cityName");
                    box.Items.Add(city.cityName);
                }
            }
        }

    }
}
