using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.DataLayer;

namespace TMSProject.Classes.BusinessLayer
{
    public class Trip
    {
        public string tripID { get; set; }
        public string orderID { get; set; }
        public string tripStatus { get; set; }
        
        public Trip() { }

        public void Save()
        {
            if (tripID != "")
            {
                new TripBizDAO().UpdateTrip(this);
            }
            else
            {
                new TripBizDAO().InsertTrip(this);
            }
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

        public List<Trip> GetTrips(string pattern)
        {
            var tripList = new TripBizDAO().GetTrips(pattern);
            return tripList;
        }

        public string generateTripID(int seq)
        {
            // Naming currentCityID + seq
            return null;
        }
    }
}
