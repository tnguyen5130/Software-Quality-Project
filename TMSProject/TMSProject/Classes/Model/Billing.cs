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
    /// \class Billing
    /// \brief This class contains the admin info
    /// \author : <i>Yonchul Choi <i>
    public class Billing
    {
        /// A string to store billingID
        /// A string to store orderID
        /// A string to store planId
        /// A string to store customerID
        /// A string to store totalAmountIDS
        public string billingID { get; set; }
        public string orderID { get; set; }
        public string planID { get; set; }
        public string customerID { get; set; }
        public double totalAmount { get; set; }

        /// \brief This method Billing for user 
        /// \details <b>Details</b>
        /// This method will get billing ID
        /// \return  void
        public Billing() { }


        /// \brief This method Save for user 
        /// \details <b>Details</b>
        /// This method will save the bill
        /// \return  void
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

        /// \brief This method Delete for user 
        /// \details <b>Details</b>
        /// This method will Delete the bill
        /// \return  void
        public void Delete()
        {
            new BillingBizDAO().DeleteBilling(this);
        }

        /// \brief This method GetById for user 
        /// \details <b>Details</b>
        /// This method will GetById the bill
        /// \return  void
        public Billing GetById(string billingID)
        {
            var billings = new BillingBizDAO().GetBillings(billingID);
            return billings[0];
        }

        /// \brief This method GetBillings for user 
        /// \details <b>Details</b>
        /// This method will get the bill
        /// \return  void
        public List<Billing> GetBillings(string pattern)
        {
            var billingList = new BillingBizDAO().GetBillings(pattern);
            return billingList;
        }

        /// \brief This method generateBillingID for user 
        /// \details <b>Details</b>
        /// This method will generate the bill
        /// \return  void
        public string generateBillingID(int seq)
        {
            // Naming BILL + orderID
            return null;
        }
    }
}
