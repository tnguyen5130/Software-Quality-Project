//* FILE			: BizCommon.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Projetc Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : BillingBizDAO for the biiling infomation 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject.Classes.Controller
{
    /// \class BizCommon
    /// \brief This class contains the billing's information for a billing file when buyer make an order
    /// \author : <i>Nhung Luong<i>
    public class BizCommon
    {
        //public string newOrderID()
        /// \brief This method newOrder for user 
        /// \details <b>Details</b>
        /// This method will generate an new order
        /// \return  void
        public string newOrder()
        {
            string value = String.Format("{0:D3}", GlobalSeq.orderSeq);
            GlobalSeq.orderSeq = GlobalSeq.orderSeq + 1;
            return "ORD" + DateTime.Now.ToString("yyyyMMdd") + value;
            
        }


        /// \brief This method fieldsValidation for user 
        /// \details <b>Details</b>
        /// This method will validate order input
        /// \return  void
        public bool fieldsValidation()
        {
            return false;
        }


        /// \brief This method getInitiated_By for user 
        /// \details <b>Details</b>
        /// This method will inititial by buyer or planner
        /// \return  void
        public string getInitiated_By()
        {
            return null;
        }


        /// \brief This method newTripID for user 
        /// \details <b>Details</b>
        /// This method will generate an new trip ID
        /// \return  void
        public string newTripID()
        {
            return null;
        }


        /// \brief This method newPlanID for user 
        /// \details <b>Details</b>
        /// This method will generate an new plan ID
        /// \return  void
        public string newPlanID()
        {
            return null;
        }


        /// \brief This method newBillingID for user 
        /// \details <b>Details</b>
        /// This method will generate an new billing ID
        /// \return  void
        public string newBillingID()
        {
            return null;
        }

        public string newInvoiceID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "INV" + value;
        }
    }
}
