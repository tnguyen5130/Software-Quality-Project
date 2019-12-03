//* FILE			: Order.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Project Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : Order for the login page




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    /// \class Order
    /// \brief This class contains the Invoice Info
    /// \author : <i>Yonchul Choi <i>
    public class Order
    {
        /// <summary>
        /// A string to get orderID 
        /// A string to get contractID
        /// A string to get origincalCityID
        /// A string to get customerID
        /// A string to get completeStatus
        /// </summary>
        public string orderID { get; set; }
        public string contractID { get; set; }
        public string orderDate { get; set; }
        public string origincalCityID { get; set; }
        public string desCityID { get; set; }
        public string carrierID { get; set; }
        public string orderStatus { get; set; }
        public string command { get; set; }


        public Order() { }

        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save order Info
        /// \return  void
        public bool Save()
        {
            bool flag = false;
            if (command == "UPDATE")
            {
                flag = new OrderBizDAO().UpdateOrder(this);

            }
            else if(command =="INSERT")
            {
                flag = new OrderBizDAO().InsertOrder(this);
            }

            return flag;
        }

        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will Delete order Info
        /// \return  void
        public void Delete()
        {
            new OrderBizDAO().DeleteOrder(this);
        }


        /// \brief This method GetById
        /// \details <b>Details</b>
        /// This method will get  order ID
        /// \return  void
        public Order GetById(string orderID)
        {
            var orders = new OrderBizDAO().GetOrders(orderID);
            if (orders.Count == 0)
            {
                return null;
            }
            else
            {
                return orders[0];
            }
            
        }

        /// \brief This method GetLastId
        /// \details <b>Details</b>
        /// This method will get last order ID
        /// \return  string
        public string GetLastId()
        {
            var orderID = new OrderBizDAO().GetLastOrderID(this);
            return orderID;
        }

        /// \brief This method GetOrders
        /// \details <b>Details</b>
        /// This method will get  order
        /// \return  void
        public List<Order> GetOrders(string pattern)
        {
            var orderList = new OrderBizDAO().GetOrders(pattern);
            return orderList;
        }


        /// \brief This method newOrderID
        /// \details <b>Details</b>
        /// This method will generate order ID
        /// \return  string
        public string NewOrderID(int seq)
         {
            string value = String.Format("{0:D3}", seq);
            return "ORD" + DateTime.Now.ToString("MMddyyyy") + value;
        }

        /// \brief This method newOrderDate
        /// \details <b>Details</b>
        /// This method will generate order Date
        /// \return  string
        public string NewOrderDate()
         {
            return DateTime.Now.ToString("yyyy-MM-dd");
         }
    }
}
