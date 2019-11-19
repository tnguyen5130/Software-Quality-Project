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
       

        public City() { }

        public void Save()
        {
            if (cityID != "")
            {
                new CityBizDAO().UpdateCity(this);
            }
            else
            {
                //new CityBizDAO().InsertCity(this);
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

        public List<City> GetOrders(string pattern)
        {
            var cityList = new CityBizDAO().GetCities(pattern);
            return cityList;
        }

        public string newCityID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "C" + value;
        }
    }
}
