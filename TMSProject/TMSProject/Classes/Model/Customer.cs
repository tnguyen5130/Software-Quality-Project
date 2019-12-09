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
        /// A string to get customerCompany
        /// A string to get customerProvince
        /// A string to get customerCity
        /// A string to get telno
        /// A string to get address
        /// A string to get zipcode
        /// A string to get command
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
        /// \return object
        public Customer GetById(string orderID)
        {
            var customers = new CustomerBizDAO().GetCustomers(orderID);
            return customers[0];
        }

        /// \brief This method GetLastCustomerName for user 
        /// \details <b>Details</b>
        /// This method will get last customer's name database 
        /// \return  void
        public string GetCustomerIDbyName(string customerName)
        {
            var id = new CustomerBizDAO().GetCustomerIDbyName(customerName);
            return id;
        }

        /// \brief This method GetLastCusName
        /// \details <b>Details</b>
        /// This method will get the last customer's name
        /// \return string
        public string GetLastCusName()
        {
            var customer = new CustomerBizDAO().GetLastCustomerName(this);
            return customer;
        }

        /// \brief This method GetLastCusID
        /// \details <b>Details</b>
        /// This method will get the last customer's ID
        /// \return string
        public string GetLastCusID()
        {
            var customer = new CustomerBizDAO().GetLastCustomerID(this);
            return customer;
        }

        /// \brief This method GetCustomers
        /// \details <b>Details</b>
        /// This method will get customer info
        /// \return List<Customer>
        public List<Customer> GetCustomers(string pattern)
        {
            var customerList = new CustomerBizDAO().GetCustomers(pattern);
            return customerList;
        }

        /// \brief This method GetCustomers
        /// \details <b>Details</b>
        /// This method will get customer info
        /// \return List<Customer>
        public Customer GetCustomerDetailsOnly(string customerName)
        {
            var customerList = new CustomerBizDAO().GetCustomersDetailsOnly(customerName);
            return customerList[0];
        }

        /// \brief This method NewCustomerID
        /// \details <b>Details</b>
        /// This method will generate new customerID based on sequence number
        /// \return List<Customer>
        public string NewCustomerID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "CUS" + DateTime.Now.ToString("yyyyMMdd") + value;
        }
    }
}
