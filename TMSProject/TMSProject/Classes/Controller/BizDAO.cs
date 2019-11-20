//* FILE			: BizDAO.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : BillingBizDAO for the biiling infomation 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TMSProject.DBConnect;

namespace TMSProject.Classes.Controller
{
    ///private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    ///private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";
    ///
    /// <br></br>
    /// \class BizDAO
    /// \brief This class contains the billing's information for a billing file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class BizDAO
    {
        /// \brief This method getOrderID for user 
        /// \details <b>Details</b>
        /// This method will get order ID
        /// \return  void
        public string getOrderID()
        {
            return null;
        }


        /// \brief This method getContractID for user 
        /// \details <b>Details</b>
        /// This method will get contract ID
        /// \return  void
        public string getContractID()
        {
            return null;
        }



        /// \brief This method getCityID for user 
        /// \details <b>Details</b>
        /// This method will get city ID for city table
        /// \return  void
        public string getCityID(string id)
        {
            return null;
        }



        /// \brief This method getCarrierID for user 
        /// \details <b>Details</b>
        /// This method will get carrier ID
        /// \return  void
        public string getCarrierID(string id)
        {
            return null;
        }


        /// \brief This method getOrderStatus for user 
        /// \details <b>Details</b>
        /// This method will get order status to check is order finish or not
        /// \return  void
        public bool getOrderStatus()
        {
            return false;
        }


        /// \brief This method getInitiatedBy for user 
        /// \details <b>Details</b>
        /// This method will get initiate by 
        /// \return  void
        public string getInitiatedBy()
        {
            return null;
        }


        /// \brief This method getCustomerID for user 
        /// \details <b>Details</b>
        /// This method will get customer ID
        /// \return  void
        public string getCustomerID()
        {
            return null;
        }


        /// \brief This method getCustomerID for user 
        /// \details <b>Details</b>
        /// This method will get customer ID
        /// \return  void
        public string C()
        {
            return null;
        }



        /// \brief This method newEmployeeID for user 
        /// \details <b>Details</b>
        /// This method will get employee ID
        /// \return  void
        public string newEmployeeID()
        {
            return null;
        }



        /// \brief This method newEmployeeType for user 
        /// \details <b>Details</b>
        /// This method will get employee type
        /// \return  void
        public string newEmployeeType()
        {
            return null;
        }

    }
}
