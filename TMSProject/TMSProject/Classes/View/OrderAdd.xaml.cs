using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMSProject.Classes.Model;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for OrderAdd.xaml
	/// </summary>
	public partial class OrderAdd : UserControl
	{
        int seq = 1;
        public OrderAdd()
		{
			InitializeComponent();
		}

		public void btn_Order_Add(object sender, RoutedEventArgs e)
		{
            Order order = new Order();
            order.orderID = txtOrderID.Text;
            order.orderDate = txtDate.Text;

            Customer customer = new Customer();
            customer.customerCity = txtCity.SelectedItem.ToString();
            customer.telno = txtTelPhone.ToString();
            customer.zipcode = txtPostalCode.ToString();
            customer.address = txtAddress.ToString();
                               
            customer.customerID = customer.generateCustomerID(seq);
            customer.Save();

            seq += 1;
            UserControl usc = null;
			usc = new ShippingInfo();
			GridOrder.Children.Add(usc);
		}
	}
}
