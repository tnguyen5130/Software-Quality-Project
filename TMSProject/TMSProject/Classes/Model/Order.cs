﻿//* FILE			: Order.cs
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

        public Order() { }

        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save order Info
        /// \return  void
        public void Save()
        {
            if (orderID != "")
            {
                new OrderBizDAO().UpdateOrder(this);
            }
            else
            {
                new OrderBizDAO().InsertOrder(this);
            }
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
            return orders[0];
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


        /// \brief This method generateOrderID
        /// \details <b>Details</b>
        /// This method will generate order ID
        /// \return  void
        public string generateOrderID(int seq)
        {
            // Naming ord + date + seq
            return null;
        }

        public bool fieldsValidation()
        {
            return false;
        }
    }
}
