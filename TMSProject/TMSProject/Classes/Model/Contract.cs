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
    /// \class PlanInfo
    /// \brief This class contains the Trip info
    /// \author : <i>Yonchul Choi <i>
    public class Contract
    {
        /// <summary>
        /// A string to get planID 
        /// A string to get tripID
        /// A string to get startCityID
        /// A string to get endCityID
        /// A string to get workingTime
        /// A string to get distance
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
        /// This method will save plan info
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



        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save plan info
        /// \return  void
        public void Delete()
        {
            new ContractBizDAO().DeleteContract(this);
        }



        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save plan info
        /// \return  void
        public Contract GetById(string contractID)
        {
            var contracts = new ContractBizDAO().GetContracts(contractID);
            return contracts[0];
        }



        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save plan info
        /// \return  void
        public List<Contract> GetContracts(string pattern)
        {
            var contractsList = new ContractBizDAO().GetContracts(pattern);
            return contractsList;
        }



        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save plan info
        /// \return  void
        public string generateContractID(int seq)
        {
            // Naming cont + date + hour + seq (1~)
            return null;
        }
    }
}
