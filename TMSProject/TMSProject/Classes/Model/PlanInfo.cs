using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class PlanInfo
    {
        public string planID { get; set; }
        public string orderID { get; set; }
        public string startCityID { get; set; }
        public string endCityID { get; set; }
        public double workingTime { get; set; }
        public double distance { get; set; }
        public string command { get; set; }

        public PlanInfo() { }

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

        public void Delete()
        {
            new PlanInfoBizDAO().DeletePlanInfo(this);
        }

        public PlanInfo GetById(string planID)
        {
            var plans = new PlanInfoBizDAO().GetPlanInfos(planID);
            return plans[0];
        }

        public List<PlanInfo> GetPlanInfos(string pattern)
        {
            var planList = new PlanInfoBizDAO().GetPlanInfos(pattern);
            return planList;
        }

        public string generatePlanID(int seq)
        {
            // Naming PLAN + tripID
            return null;
        }
    }
}
