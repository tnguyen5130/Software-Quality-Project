using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.DataLayer;

namespace TMSProject.Classes.BusinessLayer
{
    public class Billing
    {
        public string billingID { get; set; }
        public string orderID { get; set; }
        public string planID { get; set; }
        public string customerID { get; set; }
        public double totalAmount { get; set; }
        
        public Billing() { }

        public void Save()
        {
            if (planID != "")
            {
                new BillingBizDAO().UpdateBilling(this);
            }
            else
            {
                new BillingBizDAO().InsertBilling(this);
            }
        }

        public void Delete()
        {
            new BillingBizDAO().DeleteBilling(this);
        }

        public Billing GetById(string billingID)
        {
            var billings = new BillingBizDAO().GetBillings(billingID);
            return billings[0];
        }

        public List<Billing> GetBillings(string pattern)
        {
            var billingList = new BillingBizDAO().GetBillings(pattern);
            return billingList;
        }

        public string generateBillingID(int seq)
        {
            // Naming BILL + orderID
            return null;
        }
    }
}
