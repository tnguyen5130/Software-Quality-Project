using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject.Classes.Model
{
    /// \class Planner
    /// \brief This class contains the Trip info
    /// \author : <i>Yonchul Choi <i>
    public class DataGridRoute
    {
        /// <summary>
        /// A string to get trip ID
        /// A string to get order ID
        /// Astring to get tripStatus
        /// </summary>
        public string tripID { get; set; }
        public string startCityName { get; set; }
        public string endCityName { get; set; }
        public double distance { get; set; }
        public double workingTime { get; set; }
        public string rate { get; set; }
        public string price { get; set; }
        public string tripStatus { get; set; }

        public DataGridRoute() { }
    }
}

