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
        //Sequence number
        int seq = 1;
        public OrderAdd()
		{
			InitializeComponent();
		}

		public void btn_Order_Add(object sender, RoutedEventArgs e)
		{
            Order order = new Order();
            // get the orderID & orderDate and show it
            txtOrderID.Text = order.orderID;
            txtDate.Text = order.orderDate;            
            
            // Get customer information
            Customer customer = new Customer();
            customer.customerCompany = txtCompany.Text;
            customer.customerName = txtFirstName.Text + " " + txtLastName.Text;
            customer.customerCity = txtCity.SelectedValue.ToString();
            customer.customerProvince = txtProvince.SelectedValue.ToString();
            customer.telno = txtTelPhone.Text;
            customer.zipcode = txtPostalCode.Text;
            customer.address = txtAddress.Text;
            // Save customer ID into database 
            customer.customerID = customer.newCustomerID(seq);
            customer.Save();

            seq += 1;
            UserControl usc = null;
			usc = new ShippingInfo();
			GridOrder.Children.Add(usc);
		}
	}
}
