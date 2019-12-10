using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject.Classes.Model
{
    public class DataGridOrder
    {
        /// <summary>
        /// A string to get trip ID
        /// A string to get order ID
        /// Astring to get tripStatus
        /// </summary>
        public string orderID { get; set; }
        public string contractID { get; set; }
        public string customerName { get; set; }
        public string orderDate { get; set; }
        public string startCityName { get; set; }
        public string endCityName { get; set; }
        public string carrierName { get; set; }
        public int jobType { get; set; }
        public int quantity { get; set; }
        public int vanType { get; set; }

        public DataGridOrder() { }
    }
}
