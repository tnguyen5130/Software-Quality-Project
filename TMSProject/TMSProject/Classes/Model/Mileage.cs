﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.Model
{
    public class Mileage
    {
        public string mileageID { get; set; }
        public string startCityID { get; set; }
        public string startCityName { get; set; }
        public string endCityID { get; set; }
        public string endCityName { get; set; }
        public double distance { get; set; }
        public double workingTime { get; set; }
        public string status { get; set; }

        public List<Mileage> GetMileages()
        {
            var mileageList = new MileageBizDAO().GetMileages();
            return mileageList;
        }

        public List<Mileage> GetCitiesMileages(string startCityName, string endCityName)
        {
            var mileageList = new MileageBizDAO().GetCitiesMileages(startCityName, endCityName);
            return mileageList;
        }

        public List<Mileage> GetMileageID()
        {
            var mileageList = new MileageBizDAO().GetMileageID();
            return mileageList;
        }


        /// \brief This method Admin 
        /// \details <b>Details</b>
        /// This method will get admin ID
        /// \return  void
        public Mileage() { }
    }
}
