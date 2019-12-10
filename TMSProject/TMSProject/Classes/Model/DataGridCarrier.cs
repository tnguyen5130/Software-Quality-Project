//* FILE			: DataGridCarrier.cs
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
    public class DataGridCarrier
    {
        public string carrierID { get; set; }
        public string carrierName { get; set; }
        public string depotCityName { get; set; }
        public string ftlAvail { get; set; }
        public string ltlAvail { get; set; }
        public string ftlRate { get; set; }
        public string ltlRate { get; set; }
        public string reeferCharge { get; set; }

        public DataGridCarrier() { }
    }
}

