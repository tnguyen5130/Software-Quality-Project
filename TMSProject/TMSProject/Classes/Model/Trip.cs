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
        public string startCity { get; set; }
        public string endCity { get; set; }
        public string tripStatus { get; set; }
        public string command { get; set; }

        //order + mileage + plan information for making trips
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
        public string startCityName { get; set; }
        public string endCityName { get; set; }
        public double ftlRate { get; set; }
        public double ltlRate { get; set; }
        public int    jobType { get; set; }
        public int    quantity { get; set; }
        public int    vanType { get; set; }
        public double rate { get; set; }
        public double price { get; set; }

        public Trip() { }

        /// \brief This method Save trip 
        /// \details <b>Details</b>
        /// This method will save trip
        /// \return  void
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

        public List<Trip> GetTripsST(string startCityID, string endCityID)
        {
            var tripList = new TripBizDAO().GetTrips(startCityID, endCityID);
            return tripList;
        }

        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete trip
        /// \return  void
        public List<Trip> GetTrips(string tripID)
        {
            var tripList = new TripBizDAO().GetTrips(tripID);
            return tripList;
        }

        public List<Trip> GetShowTripsForBillings(string orderID, string carrierID)
        {
            var billingList = new TripBizDAO().GetShowTripsForBillings(orderID, carrierID);
            return billingList;
        }

        public List<Trip> GetTripBilling(string orderID)
        {
            var billingList = new TripBizDAO().GetTripBilling(orderID);
            return billingList;
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

        public List<Trip> newTripID()
        {
            var tripList = new TripBizDAO().GetTripID();
            return tripList;
        }
    }
}
