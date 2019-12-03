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
    /// \class Contract
    /// \brief This class contains the Trip info
    /// \author : <i>Yonchul Choi <i>
    public class Contract
    {
        /// <summary>
        /// A string to get contractID 
        /// A string to get initiateBy
        /// A string to get startDate
        /// A string to get endDate
        /// A string to get completeStatus
        /// A string to get command
        /// </summary>
        public string contractID { get; set; }
        public string initiateBy { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string completeStatus { get; set; }
        public string command { get; set; }

        public Contract() { }

        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save contract info
        /// \return  void
        public void Save()
        {
            bool flag = false;
            if (command == "UPDATE")
            {
                flag = new ContractBizDAO().UpdateContract(this);
            }
            else if(command == "INSERT")
            {
                flag = new ContractBizDAO().InsertContract(this);
            }
        }

        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete contract info
        /// \return  void
        public void Delete()
        {
            new ContractBizDAO().DeleteContract(this);
        }

        /// \brief This method NewContractID
        /// \details <b>Details</b>
        /// This method will generate contractID
        /// \return  string
        public string NewContractID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "CON" + DateTime.Now.ToString("MMddyyyy") + value;
        }

        /// \brief This method NewContractDate
        /// \details <b>Details</b>
        /// This method will generate contract Date
        /// \return  string
        public string NewContractDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// \brief This method getLastId
        /// \details <b>Details</b>
        /// This method will get last contract ID
        /// \return  string
        public string GetLastId()
        {
            var contractID = new ContractBizDAO().GetLastContractID(this);
            return contractID;
        }
    }
}
