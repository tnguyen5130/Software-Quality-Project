//* FILE			: Buyer.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : buyer for the login page 




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.Model
{
    /// \class Billing
    /// \brief This class contains the Buyer info
    /// \author : <i>Yonchul Choi <i>
    public class Buyer
    {
        /// A String to get employee ID
        public string buyerEmployeeID { get; set; }


        /// \brief This method Buyer for user 
        /// \details <b>Details</b>
        /// This method will get Buyer ID
        /// \return  void
        public Buyer() { }

        public string buyerEmployeeID { get; set; }
        public string buyerPassword { get; set; }
        public string employeeType { get; set; }
        public string command { get; set; }

        public void Save()
        {
            bool flag = false;

            if (command == "UPDATE")
            {
                flag = new BuyerBizDAO().UpdateBuyer(this);
            }
            else if (command == "INSERT")
            {
                flag = new BuyerBizDAO().InsertBuyer(this);
            }
        }

        public void Delete()
        {
            new BuyerBizDAO().DeleteBuyer(this);
        }

        public Buyer GetById(string buyerEmployeeID, string buyerPassword)
        {
            var buyers = new BuyerBizDAO().GetBuyers(buyerEmployeeID, buyerPassword);
            return buyers[0];
        }

        public List<Buyer> GetBuyers(string buyerEmployeeID, string buyerPassword)
        {
            var buyerList = new BuyerBizDAO().GetBuyers(buyerEmployeeID, buyerPassword);
            return buyerList;
        }
    }
}