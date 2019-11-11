using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class Customer
    {
        public string customerID { get; set; }
        public string customerName { get; set; }
        public string customerCity { get; set; }
        public string telno { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }

        public Customer() { }

        public void Save()
        {
            if (customerID != "")
            {
                new CustomerBizDAO().UpdateCustomer(this);
            }
            else
            {
                new CustomerBizDAO().InsertCustomer(this);
            }
        }

        public void Delete()
        {
            new CustomerBizDAO().DeleteCustomer(this);
        }

        public Customer GetById(string customerID)
        {
            var customers = new CustomerBizDAO().GetCustomers(customerID);
            return customers[0];
        }

        public List<Customer> GetCustomers(string pattern)
        {
            var customerList = new CustomerBizDAO().GetCustomers(pattern);
            return customerList;
        }
    }
}
