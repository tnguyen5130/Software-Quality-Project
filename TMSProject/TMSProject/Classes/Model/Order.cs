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

        public Order() { }

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

        public void Delete()
        {
            new OrderBizDAO().DeleteOrder(this);
        }

        public Order GetById(string orderID)
        {
            var orders = new OrderBizDAO().GetOrders(orderID);
            return orders[0];
        }

        public List<Order> GetOrders(string pattern)
        {
            var orderList = new OrderBizDAO().GetOrders(pattern);
            return orderList;
        }

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
