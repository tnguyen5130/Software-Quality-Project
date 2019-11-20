using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;
using System.Collections.Generic;

namespace TDD_TMS
{
    [TestClass]
    public class BuyerTestCase
    {
        [TestMethod]
        public void checkCityID()
        {
            bool isExistingData = false;
            
            City city = new City();
            city.cityID = "C004";

            var cityResult = city.GetCities(city.cityID);

            if (cityResult[0].cityName == "Toronto")
            {
                isExistingData = true;
            }
            else
            {
                isExistingData = false;
            }

            Assert.AreEqual(true, isExistingData);
        }

        [TestMethod]
        public void checkContractID()
        {
            bool isExistingData = false;
            Contract contract = new Contract();
            contract.contractID = "CONT2019111908001";

            var contractResult = contract.GetContracts(contract.contractID);

            if (contractResult[0].contractID != null)
            {
                isExistingData = true;
            }
            else
            {
                isExistingData = false;
            }

            Assert.AreEqual(true, isExistingData);
        }

        [TestMethod]
        public void addOrder()
        {
            Order order = new Order();
            order.orderID = "ORD20191119004";

            //check new Order->Insert, exiting order->Update
            Order checkOrder = new Order();
            checkOrder = checkOrder.GetById(order.orderID);

            if (checkOrder == null)
            {
                order.command = "INSERT";
            }
            else
            {
                order.orderID = "UPDATE";
            }

            //check contractID
            order.contractID = "CONT2019111908001";
            order.orderDate = "20191120";

            //check city
            order.origincalCityID = "C004";
            //check city
            order.desCityID = "C002";
            //buyer should set the value(carrierID) --> default CA00000000000
            order.carrierID = "CA00000000000";
            order.orderStatus = "A";

            bool flag = order.Save();

            Assert.AreEqual(true, flag);
        }
    }
}
