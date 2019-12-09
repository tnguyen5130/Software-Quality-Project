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
    /// \class Planner
    /// \brief This class contains the Planner info
    /// \author : <i>Yonchul Choi <i>
    public class Planner
    {
        /// A string to get planner ID
        public string plannerEmployeeID { get; set; }
        public string plannerPassword { get; set; }
        public string employeeType { get; set; }
        public string command { get; set; }

        public void Save()
        {
            bool flag = false;

            if (command == "UPDATE")
            {
                flag = new PlannerBizDAO().UpdatePlanner(this);
            }
            else if (command == "INSERT")
            {
                flag = new PlannerBizDAO().InsertPlanner(this);
            }
        }

        public void Delete()
        {
            new PlannerBizDAO().DeletePlanner(this);
        }

        public Planner GetById(string plannerEmployeeID, string plannerPassword)
        {
            var planners = new PlannerBizDAO().GetPlanners(plannerEmployeeID, plannerPassword);
            return planners[0];
        }

        public List<Planner> GetPlanners(string plannerEmployeeID, string plannerPassword)
        {
            var plannerList = new PlannerBizDAO().GetPlanners(plannerEmployeeID, plannerPassword);
            return plannerList;
        }

        /// \brief This method Planner for user 
        /// \details <b>Details</b>
        /// This method will get planner ID
        /// \return  void
        public Planner() { }
    }
}
