using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class City
    {
        public string cityID { get; set; }
        public string cityName { get; set; }
        public string command { get; set; }
       

        public City() { }

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

        public void Delete()
        {
            new CityBizDAO().DeleteCity(this);
        }

        public City GetById(string cityID)
        {
            var cities = new CityBizDAO().GetCities(cityID);
            return cities[0];
        }

        public List<City> GetCities(string pattern)
        {
            var cityList = new CityBizDAO().GetCities(pattern);
            return cityList;
        }

<<<<<<< HEAD
        public List<City> GetCityName(string pattern)
        {
            var cityList = new CityBizDAO().GetCityName(pattern);
            return cityList;
=======
        public string newCityID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "C" + value;
>>>>>>> 3c765d5050898e05342c0eb00a7a51612041337b
        }
    }
}
