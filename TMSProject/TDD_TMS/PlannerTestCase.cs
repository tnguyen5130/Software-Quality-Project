using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;
using System.Collections.Generic;

namespace TDD_TMS
{
    [TestClass]
    public class PlannerTestCase
    {
        [TestMethod]
        public void Planner_Order_Search()
        {
            bool isSameData = false;
           
            Order order = new Order();
            order.orderID = "ORD20191119001";

            Order compareOrder = new Order();
            compareOrder.orderID = "ORD20191119001";
            compareOrder.contractID = "CONT2019111908001";
            compareOrder.orderDate = "20191120";
            compareOrder.originalCityID = "C004";
            compareOrder.desCityID = "C002";
            compareOrder.carrierID = "CA20190101001";
            compareOrder.orderStatus = "A";

            var selectedOrder = order.GetOrders(order.orderID);

            if ((compareOrder.orderID == selectedOrder[0].orderID) && (compareOrder.contractID == selectedOrder[0].contractID) &&
                (compareOrder.orderDate == selectedOrder[0].orderDate) && (compareOrder.originalCityID == selectedOrder[0].originalCityID) &&
                (compareOrder.desCityID == selectedOrder[0].desCityID) && (compareOrder.carrierID == selectedOrder[0].carrierID) &&
                (compareOrder.orderStatus == selectedOrder[0].orderStatus))
            {
                isSameData = true;
            }
            else
            {
                isSameData = false;
            }

            Assert.AreEqual(1, selectedOrder.Count);
            Assert.AreEqual(true, isSameData);

        }

        [TestMethod]
        public void Planner_Select_Carrier()
        {   
            string cityName = "Toronto";

            //Order Info 
            Order order = new Order();
            order.orderID = "ORD20191119001";
            var orderResult = order.GetOrders(order.orderID);
            
            //set Order.carrierID with selected carrierID
            if (orderResult[0] == null)
            {
                order.command = "INSERT";
            }
            else
            {
                order.command = "UPDATE";
                //assign data from table
                order.contractID = orderResult[0].contractID;
                order.orderDate = orderResult[0].orderDate;
                order.originalCityID = orderResult[0].originalCityID;
                order.desCityID = orderResult[0].desCityID;
                order.orderStatus = orderResult[0].orderStatus;
            }

            //search carrierName with cityName
            Carrier carrier = new Carrier();
            //selected carrierID
            var carrierResult = carrier.GetCarriers(cityName);

            if(carrierResult[1] != null)
            {
                order.carrierID = carrierResult[1].carrierID;
            }
            
            bool flag = order.Save();
            Assert.AreEqual(true, flag);

        }

        [TestMethod]
        public void Planner_Make_Trips()
        {
            bool flag = false;

            //defind startCity, endCity
            string startCityID = "";
            string endCityID = "";

            //get the cityID 
            City city = new City();
            city.cityName = "Windsor";
            var startCityResult = city.GetCityName(city.cityName);
            startCityID = startCityResult[0].cityID;

            city.cityName = "Hamilton";
            var endCityResult = city.GetCityName(city.cityName);
            endCityID = endCityResult[0].cityID;

            //retreive order data
            //Order order = new Order();
            Trip trip = new Trip();
            //order.orderID = "ORD20191120001";
            
            var viewTrips = trip.GetTrips(startCityID, endCityID);
            
            String[] tripID = new String[4];
            tripID[0] = "C00120191120001";
            tripID[1] = "C00120191120002";
            tripID[2] = "C00120191120003";
            tripID[3] = "C00120191120004";

            for (int i=0; i < viewTrips.Count - 1; i++)
            {
                trip.tripID = tripID[i];
                trip.orderID = viewTrips[i].orderID;
                trip.startCity = viewTrips[i].startCityID;
                trip.endCity = viewTrips[i].endCityID;

                if(viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].startCityID == viewTrips[i].originalCityID)
                {
                    trip.tripStatus = "loading";
                }
                else if ((viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].endCityID == viewTrips[i].desCityID))
                {
                    trip.tripStatus = "unloading";
                }
                else
                {
                    trip.tripStatus = "driving";
                }

                trip.command = "INSERT";
                flag = trip.Save();
                Assert.AreEqual(true, flag);
            }
        }

        [TestMethod]
        public void Planner_Make_Plan()
        {
            bool flag = false;

            //Order Info 
            Order order = new Order();
            order.orderID = "ORD20191119001";

            //Planinfo 
            PlanInfo plan = new PlanInfo();
            var planResult = plan.GetPlanInfos(order.orderID);

            //set Order.carrierID with selected carrierID
            if(planResult[0] != null)
            {
                plan.command = "INSERT";

                //assign data from table
                plan.planID = "PLAN" + planResult[0].orderID;
                plan.orderID = planResult[0].orderID;
                plan.startCityID = planResult[0].startCityID;
                plan.endCityID = planResult[0].endCityID;
                plan.distance = planResult[0].distance;
                plan.workingTime = planResult[0].workingTime;

                flag = plan.Save();
            }
            else
            {
                flag = false;
            }
            
            Assert.AreEqual(true, flag);

        }
    }
}
