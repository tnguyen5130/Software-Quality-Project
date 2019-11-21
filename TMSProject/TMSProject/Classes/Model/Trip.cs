using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class Trip
    {
        public string tripID { get; set; }
        public string orderID { get; set; }
        public string startCity { get; set; }
        public string endCity { get; set; }
        public string tripStatus { get; set; }
        public string command { get; set; }

        //order + mileage information for making trips
        public string contractID { get; set; }
        public string orderDate { get; set; }
        public string originalCityID { get; set; }
        public string desCityID { get; set; }
        public string carrierID { get; set; }
        public string orderStatus { get; set; }
        public string mileageID { get; set; }
        public string startCityID { get; set; }
        public string endCityID { get; set; }
        public double distance { get; set; }
        public double workingTime { get; set; }

        public Trip() { }

        public bool Save()
        {
            bool flag = false;
            if (command == "UPDATE")
            {
                flag = new TripBizDAO().UpdateTrip(this);
            }
            else if (command == "INSERT")
            {
                flag = new TripBizDAO().InsertTrip(this);
            }

            return flag;
        }

        public void Delete()
        {
            new TripBizDAO().DeleteTrip(this);
        }

        public Trip GetById(string tripID)
        {
            var trips = new TripBizDAO().GetTrips(tripID);
            return trips[0];
        }

        public List<Trip> GetTripsST(string startCityID, string endCityID)
        {
            var tripList = new TripBizDAO().GetTrips(startCityID, endCityID);
            return tripList;
        }

        public List<Trip> GetTrips(string tripID)
        {
            var tripList = new TripBizDAO().GetTrips(tripID);
            return tripList;
        }

        public string generateTripID(int seq)
        {
            // Naming currentCityID + seq
            return null;
        }
    }
}
