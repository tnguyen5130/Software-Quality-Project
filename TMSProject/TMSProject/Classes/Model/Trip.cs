//* FILE			: Carrier.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Project Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : buyer for the login page



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    /// \class Planner
    /// \brief This class contains the Trip info
    /// \author : <i>Yonchul Choi <i>
    public class Trip
    {
        /// <summary>
        /// A string to get trip ID
        /// A string to get order ID
        /// Astring to get tripStatus
        /// </summary>
        public string tripID { get; set; }
        public string orderID { get; set; }
        public string tripStatus { get; set; }
        
        public Trip() { }

        /// \brief This method Save trip 
        /// \details <b>Details</b>
        /// This method will save trip
        /// \return  void
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



        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete trip
        /// \return  void
        public void Delete()
        {
            new TripBizDAO().DeleteTrip(this);
        }


        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete trip
        /// \return  void
        public Trip GetById(string tripID)
        {
            var trips = new TripBizDAO().GetTrips(tripID);
            return trips[0];
        }


        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete trip
        /// \return  void
        public List<Trip> GetTrips(string pattern)
        {
            var tripList = new TripBizDAO().GetTrips(pattern);
            return tripList;
        }



        /// \brief This method generateTripID
        /// \details <b>Details</b>
        /// This method will generate trip ID
        /// \return  void
        public string generateTripID(int seq)
        {
            // Naming currentCityID + seq
            return null;
        }
    }
}
