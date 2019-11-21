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
using System.Text.RegularExpressions;

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

        private bool validationOrderAdd()
        {
            bool retCode = true;
            // Check if edit field is blank
            if (txtCompany.Text == null && txtFirstName.Text == null && txtLastName.Text == null && txtTelPhone.Text == "" && txtCity.Text == null &&
                txtCity.SelectedValue.ToString() == null && txtProvince.SelectedValue.ToString() == null && txtPostalCode.Text == null && txtAddress.Text == null)
            {
                MessageBox.Show("Some Fields can not be blank");
                retCode = false;
            }
            // check if these fields contains numbers
            else if (Regex.IsMatch(txtCompany.Text, @"^[0-9]*$"))
            {
                MessageBox.Show("Invalid Company Name, try (ex: DHL)");
                retCode = false;
            }
            else if (Regex.IsMatch(txtFirstName.Text, @"^[0-9]*$"))
            {
                MessageBox.Show("Invalid first name, try(ex: John)");
                retCode = false;
            }
            else if (Regex.IsMatch(txtLastName.Text, @"^[0-9]*$"))
            {
                MessageBox.Show("Invalid last name name, try(ex: Smith)");
                retCode = false;
            }
            else if (!Regex.IsMatch(txtTelPhone.Text, @"^[0-9]*$"))
            {
                MessageBox.Show("Invalid Telephone Number!!");
                retCode = false;
            }
            else if (!Regex.IsMatch(txtPostalCode.Text, @"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$"))
            {
                MessageBox.Show("Invalid Postal Code, try (ex: N2P 0C7)");
                retCode = false;
            }
            else if (!Regex.IsMatch(txtAddress.Text, @"^\d+\s[A-z]+\s[A-z]+"))  
            {
                MessageBox.Show("Invalid Address, try (ex: 61 Park Street)");
                retCode = false;
            }
            else if (txtCity.SelectedIndex <= -1) // nothing selected
            {
                MessageBox.Show("City Box must be selected");
                retCode = false;
            }
            else if (txtProvince.SelectedIndex <= -1) // nothing selected
            {
                MessageBox.Show("Province Box must be selected");
                retCode = false;
            }
            return retCode;
        }

		public void btn_Order_Add(object sender, RoutedEventArgs e)
		{
            Order order = new Order();
            // get the orderID & orderDate and show it
            txtOrderID.Text = order.orderID;
            txtDate.Text = order.orderDate;

            if (validationOrderAdd())
            {
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
}
