using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.Model
{
    public class Planner
    {
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
    }
}
