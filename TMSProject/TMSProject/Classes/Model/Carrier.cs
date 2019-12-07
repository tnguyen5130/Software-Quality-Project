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
    /// \brief This class contains the Planner info
    /// \author : <i>Yonchul Choi <i>
    public class Carrier
    {
        /// <summary>
        /// A string to get carrier ID
        /// A string to get depot city
        /// A string to get carrier Name
        /// A string to get ftlAvail
        /// A string to get ltlAvail
        /// A string to get fltRate
        /// A string to get ltlRate
        /// Astring to get feeferCharge
        /// </summary>
        public string carrierID { get; set; }
        public string depotCity { get; set; }
        public string carrierName { get; set; }
        public double ftlAvail { get; set; }
        public double ltlAvail { get; set; }
        public double ftlRate { get; set; }
        public double ltlRate { get; set; }
        public double reeferCharge { get; set; }

        public string command { get; set; }
        public string orderID { get; set; }
        public int jobType { get; set; }
        public int quantity { get; set; }
        public int vanType { get; set; }

        public Carrier() { }

        /// \brief This method Save carrier 
        /// \details <b>Details</b>
        /// This method will save carrier
        /// \return  void
        public void Save()
        {
            if (carrierID != "")
            {
                new CarrierBizDAO().UpdateCarrier(this);
            }
            else
            {
                new CarrierBizDAO().InsertCarrier(this);
            }
        }


        /// \brief This method Save carrier 
        /// \details <b>Details</b>
        /// This method will save carrier
        /// \return  void
        public void Delete()
        {
            new CarrierBizDAO().DeleteCarrier(this);
        }

        public string GetLastCarrierID()
        {
            var carrier = new CarrierBizDAO().GetLastCarrierID();
            return carrier;
        }

        /// \brief This method NewCarrierID
        /// \details <b>Details</b>
        /// This method will generate carrier ID
        /// \return  string
        public string NewCarrierID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "CAR" + value;
        }
    }
}
