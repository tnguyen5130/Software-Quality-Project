using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    public class Order
    {
        public string orderID { get; set; }
        public string contractID { get; set; }
        public string orderDate { get; set; }
        public string origincalCityID { get; set; }
        public string desCityID { get; set; }
        public string carrierID { get; set; }
        public string orderStatus { get; set; }
        public string command { get; set; }


        public Order() { }

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

        public void Delete()
        {
            new OrderBizDAO().DeleteOrder(this);
        }

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

        public List<Order> GetOrders(string pattern)
        {
            var orderList = new OrderBizDAO().GetOrders(pattern);
            return orderList;
        }

        public string generateOrderID(int seq,int time)
        {            
            return "ord"+DateTime.Now.ToString()+time;
        }

        public bool fieldsValidation()
        {
            return false;
        }
    }
}
