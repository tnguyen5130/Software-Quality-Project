using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.DataLayer;

namespace TMSProject.Classes.BusinessLayer
{
    public class Contract
    {  
        public string contractID { get; set; }
        public string initiateBy { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string completeStatus { get; set; }

        public Contract() { }

        public void Save()
        {
            if (contractID != "")
            {
                new ContractBizDAO().UpdateContract(this);
            }
            else
            {
                new ContractBizDAO().InsertContract(this);
            }
        }

        public void Delete()
        {
            new ContractBizDAO().DeleteContract(this);
        }

        public Contract GetById(string contractID)
        {
            var contracts = new ContractBizDAO().GetContracts(contractID);
            return contracts[0];
        }

        public List<Contract> GetContracts(string pattern)
        {
            var contractsList = new ContractBizDAO().GetContracts(pattern);
            return contractsList;
        }

        public string generateContractID(int seq)
        {
            // Naming cont + date + hour + seq (1~)
            return null;
        }
    }
}
