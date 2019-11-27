using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;
using System.Collections.Generic;

namespace TDD_TMS
{
    [TestClass]
    public class LoginTestCase
    {

        [TestMethod]
        public void AdminLoginTest()
        {
            bool loginResult = false;
            Admin admin = new Admin();
            admin.adminEmployeeID = "admin01";
            admin.adminPassword = "123";

            var adminResult = admin.GetAdmins(admin.adminEmployeeID, admin.adminPassword);
            if (adminResult[0].employeeType == "ADMIN")
            {
                loginResult = true;
            }
            else
            {
                loginResult = false;
            }

            Assert.AreEqual(true, loginResult);
        }

        [TestMethod]
        public void BuyerLoginTest()
        {
            bool loginResult = false;
            Buyer buyer = new Buyer();
            buyer.buyerEmployeeID = "buyer01";
            buyer.buyerPassword = "123";

            var buyerResult = buyer.GetBuyers(buyer.buyerEmployeeID, buyer.buyerPassword);
            if (buyerResult[0].employeeType == "BUYER")
            {
                loginResult = true;
            }
            else
            {
                loginResult = false;
            }

            Assert.AreEqual(true, loginResult);
        }

        [TestMethod]
        public void PlannerLoginTest()
        {
            bool loginResult = false;
            Planner planner = new Planner();
            planner.plannerEmployeeID = "planner01";
            planner.plannerPassword = "123";

            var plannerResult = planner.GetPlanners(planner.plannerEmployeeID, planner.plannerPassword);
            if (plannerResult[0].employeeType == "PLANNER")
            {
                loginResult = true;
            }
            else
            {
                loginResult = false;
            }

            Assert.AreEqual(true, loginResult);
        }



    }
}