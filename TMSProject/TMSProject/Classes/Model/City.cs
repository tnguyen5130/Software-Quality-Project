//* FILE			: City.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : City for the biiling infomation



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    /// \class Planner
    /// \brief This class contains the Planner info
    /// \author : <i>Yonchul Choi <i>
    public class City
    {
        /// A String to get city ID
        /// A string to get city name
        public string cityID { get; set; }
        public string cityName { get; set; }

        public string command { get; set; }   

        /// <summary>
        /// Astring to get city
        /// </summary>
        public City() { }


        /// \brief This method Save for user 
        /// \details <b>Details</b>
        /// This method will save city ID
        /// \return  void
        public void Save()
        {
            bool flag = false;

            if (command == "UPDATE")
            {
                flag = new CityBizDAO().UpdateCity(this);
            }
            else if (command =="INSERT")
            {
                flag = new CityBizDAO().InsertCity(this);
            }
        }

        /// \brief This method Delete city 
        /// \details <b>Details</b>
        /// This method will get planner ID
        /// \return  void
        public void Delete()
        {
            new CityBizDAO().DeleteCity(this);
        }


        /// \brief This method GetById for user 
        /// \details <b>Details</b>
        /// This method will get city by ID
        /// \return  void
        public City GetById(string cityID)
        {
            var cities = new CityBizDAO().GetCities(cityID);
            return cities[0];
        }

        /// \brief This method GetOrders for user 
        /// \details <b>Details</b>
        /// This method will get order
        /// \return  void
        public List<City> GetOrders(string pattern)
        {
            var cityList = new CityBizDAO().GetCities(pattern);
            return cityList;
        }


        //public List<City> GetCityName(string pattern)
        //{
        //    var cityList = new CityBizDAO().GetCityName(pattern);
        //    return cityList;
        //}

        public string newCityID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "C" + value;
        }

        public List<City> GetCityName(string pattern)
        {
            var cityList = new CityBizDAO().GetCityName(pattern);
            return cityList;
        }
    }
}
