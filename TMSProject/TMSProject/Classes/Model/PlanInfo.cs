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


/// \class PlanInfo
/// \brief This class contains the Trip info
/// \author : <i>Yonchul Choi <i>
namespace TMSProject.Classes.Model
{
    public class PlanInfo
    {

        /// <summary>
        /// A string to get planID 
        /// A string to get tripID
        /// A string to get startCityID
        /// A string to get endCityID
        /// A string to get workingTime
        /// A string to get distance
        /// </summary>
        public string planID { get; set; }
        public string orderID { get; set; }
        public string startCityID { get; set; }
        public string endCityID { get; set; }
        public double workingTime { get; set; }
        public double distance { get; set; }
        public string command { get; set; }

        public PlanInfo() { }

        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save plan info
        /// \return  void
        public bool Save()
        {
            bool flag = false;
            if (command == "UPDATE")
            {
                flag = new PlanInfoBizDAO().UpdatePlanInfo(this);
            }
            else if (command == "INSERT")
            {
                flag = new PlanInfoBizDAO().InsertPlanInfo(this);
            }

            return flag;
        }


        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete plan info
        /// \return  void
        public void Delete()
        {
            new PlanInfoBizDAO().DeletePlanInfo(this);
        }


        /// \brief This method GetById
        /// \details <b>Details</b>
        /// This method will get ID by element
        /// \return  void
        public PlanInfo GetById(string planID)
        {
            var plans = new PlanInfoBizDAO().GetPlanInfo(planID);
            return plans[0];
        }


        /// \brief This method GetPlanInfo
        /// \details <b>Details</b>
        /// This method will get plan info
        /// \return  void
        public List<PlanInfo> GetPlanInfo(string pattern)
        {
            var planList = new PlanInfoBizDAO().GetPlanInfo(pattern);
            return planList;
        }


        /// \brief This method generatePlanID
        /// \details <b>Details</b>
        /// This method will generate plan ID
        /// \return  void
        public string generatePlanID(int seq)
        {
            // Naming PLAN + tripID
            return null;
        }
    }
}
