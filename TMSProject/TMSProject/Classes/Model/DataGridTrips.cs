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
    public class DataGridTrips
    {
        /// <summary>
        /// A string to get trip ID
        /// A string to get order ID
        /// Astring to get tripStatus
        /// </summary>
        public string TranNo { get; set; }
        public string tripStatus { get; set; }
        public double distance { get; set; }
        public double workingTime { get; set; }
        public string startCityName { get; set; }
        public string endCityName { get; set; }

        public DataGridTrips() { }
    }
}
