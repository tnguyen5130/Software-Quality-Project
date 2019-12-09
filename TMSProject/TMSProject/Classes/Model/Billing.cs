//* FILE			: Billing.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : Billing for the biiling infomation 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class Billing
    {
        public string billingID { get; set; }
        public string orderID { get; set; }
        public string planID { get; set; }
        public string customerID { get; set; }
        public double totalAmount { get; set; }
        public string command { get; set; }

        public double workingTime { get; set; }
        public double distance { get; set; }
        public int jobType { get; set; }
        public int quantity { get; set; }
        public int vanType { get; set; }
        public double ftlRate { get; set; }
        public double ltlRate { get; set; }
        public double reeferCharge { get; set; }

        public Billing() { }

        public bool Save()
        {
            bool flag = false;

            if (command == "UPDATE")
            {
                flag = new BillingBizDAO().UpdateBilling(this);
            }
            else if (command == "INSERT")
            {
                flag = new BillingBizDAO().InsertBilling(this);
            }

            return flag;
        }

        public void Delete()
        {
            new BillingBizDAO().DeleteBilling(this);
        }

        public Billing GetById(string billingID, string orderID)
        {
            var billings = new BillingBizDAO().GetBillings(billingID, orderID);
            return billings[0];
        }

        public List<Billing> GetBillings(string planID, string orderID)
        {
            var billingList = new BillingBizDAO().GetBillings(planID, orderID);
            return billingList;
        }

        public List<Billing> GetBillingID(string planID, string orderID)
        {
            var billingList = new BillingBizDAO().GetBillingID(planID, orderID);

            return billingList;
        }

        public string generateBillingID(int seq)
        {
            // Naming BILL + orderID
            return null;
        }
    }
}
