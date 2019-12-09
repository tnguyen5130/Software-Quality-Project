using System;
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
        public string command { get; set; }

        public bool Save()
        {
            bool flag = false;
            if (command == "UPDATE")
            {
                flag = new MileageBizDAO().UpdateMileage(this);
            }
            else
            {
                flag = new MileageBizDAO().InsertMileage(this);
            }

            return flag;
        }

        /// \brief This method Delete carrier 
        /// \details <b>Details</b>
        /// This method will Delete carrier
        /// \return  void
        public void Delete()
        {
            new MileageBizDAO().DeleteMileage(this);
        }

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

        /// \brief This method NewMileageID
        /// \details <b>Details</b>
        /// This method will generate mileage ID
        /// \return  string
        public string NewMileageID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "MILE" + value;
        }

        /// \brief This method GetLastCarrierID  
        /// \details <b>Details</b>
        /// This method will get the last carrier by ID
        /// \return  void
        public string GetLastMileageID()
        {
            var mileage = new MileageBizDAO().GetLastMileageID();
            return mileage;
        }

        /// \brief This method Admin 
        /// \details <b>Details</b>
        /// This method will get admin ID
        /// \return  void
        public Mileage() { }
    }
}
