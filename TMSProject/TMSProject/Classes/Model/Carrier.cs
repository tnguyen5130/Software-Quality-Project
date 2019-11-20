using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class Carrier
    {
        public string carrierID { get; set; }
        public string depotCity { get; set; }
        public string carrierName { get; set; }
        public double ftlAvail { get; set; }
        public double ltlAvail { get; set; }
        public double ftlRate { get; set; }
        public double ltlRate { get; set; }
        public double reeferCharge { get; set; }

        public Carrier() { }

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

        public void Delete()
        {
            new CarrierBizDAO().DeleteCarrier(this);
        }

        public Carrier GetById(string contractID)
        {
            var carriers = new CarrierBizDAO().GetCarriers(contractID);
            return carriers[0];
        }

        public List<Carrier> GetCarriers(string pattern)
        {
            var contractsList = new CarrierBizDAO().GetCarriers(pattern);
            return contractsList;
        }
    }
}
