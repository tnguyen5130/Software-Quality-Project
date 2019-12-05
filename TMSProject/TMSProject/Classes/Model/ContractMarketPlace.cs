//* FILE			: Carrier.cs
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
    /// \class ContractMarketPlace
    /// \brief This class contains the Contract Market Place info
    /// \author : <i>Yonchul Choi <i>
    public class ContractMarketPlace
    {
        /// <summary>
        /// A string to get customerID 
        /// A string to get contractID
        /// A string to get jobType
        /// A string to get quantity
        /// A string to get origin
        /// A string to get destination
        /// A string to get vanType
        /// </summary>
        public string customerID { get; set; }
        public string contractID { get; set; }
        public string jobType { get; set; }
        public int quantity { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string vanType { get; set; }
        public string command { get; set; }

        public ContractMarketPlace() { }

        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save CMP info
        /// \return  void
        public void Save()
        {
            if (command == "UPDATE")
            {
                new CMPBizDAO().UpdateCMP(this);
            }
            else if (command == "INSERT")
            {
                new CMPBizDAO().InsertCMP(this);
            }
        }

        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete CMP info
        /// \return  void
        public void Delete()
        {
            new CMPBizDAO().DeleteCMP(this);
        }

        /// \brief This method GetById
        /// \details <b>Details</b>
        /// This method will get ID
        /// \return  void
        public ContractMarketPlace GetById(string customerID)
        {
            var cmps = new CMPBizDAO().GetCMPs(customerID);
            return cmps[0];
        }

        /// \brief This method GetLastCusID
        /// \details <b>Details</b>
        /// This method will get the last customerID
        /// \return string
        public string GetLastCusID()
        {
            var customer = new CMPBizDAO().GetLastCustomerId(this);
            return customer;
        }

        /// \brief This method NewCustomerID
        /// \details <b>Details</b>
        /// This method will create new customerID
        /// \return  void
        public string NewCustomerID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "CUS" + DateTime.Now.ToString("MMddyyyy") + value;
        }

        /// \brief This method GetCMPs
        /// \details <b>Details</b>
        /// This method will store CMP
        /// \return  void
        public List<ContractMarketPlace> GetCMPs(string pattern)
        {
            var cmpsList = new CMPBizDAO().GetCMPs(pattern);
            return cmpsList;
        }
    }
}
