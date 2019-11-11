using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class ContractMarketPlace
    {   
        public string customerID { get; set; }
        public string contractID { get; set; }
        public string jobType { get; set; }
        public int quantity { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string vanType { get; set; }

        public ContractMarketPlace() { }

        public void Save()
        {
            if (customerID != "")
            {
                new CMPBizDAO().UpdateCMP(this);
            }
            else
            {
                new CMPBizDAO().InsertCMP(this);
            }
        }

        public void Delete()
        {
            new CMPBizDAO().DeleteCMP(this);
        }

        public ContractMarketPlace GetById(string customerID)
        {
            var cmps = new CMPBizDAO().GetCMPs(customerID);
            return cmps[0];
        }

        public List<ContractMarketPlace> GetCMPs(string pattern)
        {
            var cmpsList = new CMPBizDAO().GetCMPs(pattern);
            return cmpsList;
        }
    }
}
