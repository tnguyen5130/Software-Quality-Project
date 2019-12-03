//* FILE			: Customer.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Project Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : get customer info




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    /// \class Customer
    /// \brief This class contains the Customer Info
    /// \author : <i>Yonchul Choi <i>
    public class Customer
    {
        /// <summary>
        /// A string to get customerID 
        /// A string to get customerName
        /// A string to get customerCity
        /// A string to get telno
        /// A string to get address
        /// A string to get zipcode
        /// </summary>
        public string customerID { get; set; }
        public string customerName { get; set; }
        public string customerCompany { get; set; }
        public string customerProvince { get; set; }
        public string customerCity { get; set; }
        public string telno { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string command { get; set; }



        public Customer() { }


        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save customer Info
        /// \return  void
        public void Save()
        {
            if (command == "UPDATE")
            {
                new CustomerBizDAO().UpdateCustomer(this);
            }
            else if (command == "INSERT")
            {
                new CustomerBizDAO().InsertCustomer(this);
            }
        }


        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will delete customer
        /// \return  void
        public void Delete()
        {
            new CustomerBizDAO().DeleteCustomer(this);
        }
        /// \brief This method GetById
        /// \details <b>Details</b>
        /// This method will get emelent by ID
        /// \return  void
        public Customer GetById(string orderID)
        {
            var customers = new CustomerBizDAO().GetCustomers(orderID);
            return customers[0];
        }

        public string GetLastCusName()
        {
            var customer = new CustomerBizDAO().GetLastCustomerName(this);
            return customer;
        }

        /// \brief This method GetCustomers
        /// \details <b>Details</b>
        /// This method will get customer info
        /// \return  void
        public List<Customer> GetCustomers(string pattern)
        {
            var customerList = new CustomerBizDAO().GetCustomers(pattern);
            return customerList;
        }

        public string NewCustomerID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "cus" + DateTime.Now.ToString("yyyyMMddTHH:mm:ssZ") + value;
        }
    }
}
