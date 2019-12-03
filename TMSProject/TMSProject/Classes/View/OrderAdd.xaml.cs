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
using MySql.Data.MySqlClient;

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for OrderAdd.xaml
	/// </summary>
	public partial class OrderAdd : UserControl
	{
        //Sequence number
        int ordSeq = 1;
        string tempBuffer = "";

        public OrderAdd(string cusName, string cusID)
		{
			InitializeComponent();
            
            Order order = new Order();
            // Generate new order
            // Check if orderID exist or not
            if (order.orderID != order.GetLastId())
            {
                order.orderID = order.NewOrderID(ordSeq);
            }
            else if (order.orderID == order.GetLastId())
            {
                order.command = "INSERT";
                string buffer = order.GetLastId();
                // Get the last character in the last OrderID
                char last = buffer[buffer.Length - 3];
                // Convert it into INT
                int temp = (int)last;
                // Add by 1
                temp += 1;
                //Delete the last character of the buffer
                string newBuffer = RemoveLastChar(buffer);
                // Add with new temp
                order.orderID = newBuffer + String.Format("{0:D3}", temp);
            }

            order.orderDate = order.NewOrderDate();
            txtOrderID.Text = order.orderID;
            txtOrderDate.Text = order.orderDate;

            // Assign customerID & customer Name
            tempBuffer = cusID;
            txtName.Text = cusName;
        }

        /// \brief This method RemoveLastChar for ID 
        /// \details <b>Details</b>
        /// This method will remove the last character of a string
        /// \return  string
        private string RemoveLastChar(string str)
        {
            return str.Substring(0, str.Length - 3);
        }

        /// \brief This method validationOrderAdd  
        /// \details <b>Details</b>
        /// This method will do some validation for customer details
        /// \return  string
        private bool validationOrderAdd()
        {
            bool retCode = true;
            // Check if edit field is blank
            if (txtCompany.Text == null && txtName.Text == null && txtTelPhone.Text == "" && txtCity.Text == null &&
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
            else if (Regex.IsMatch(txtName.Text, @"^[0-9]*$"))
            {
                MessageBox.Show("Invalid first name, try(ex: John)");
                retCode = false;
            }
            else if (!Regex.IsMatch(txtTelPhone.Text, @"^[0-9]*$"))
            {
                MessageBox.Show("Invalid Telephone Number!!");
                retCode = false;
            }
            else if (!Regex.IsMatch(txtAddress.Text, @"^\d+\s[A-z]+\s[A-z]+"))
            {
                MessageBox.Show("Invalid Address, try (ex: 61 Park Street)");
                retCode = false;
            }
            else if (!Regex.IsMatch(txtPostalCode.Text, @"^^(\d{5}(-\d{4})?|[A-Z]\d[A-Z] ?\d[A-Z]\d)$$"))
            {
                MessageBox.Show("Invalid Postal Code, try (ex: N2P 0C7)");
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

            if (validationOrderAdd())
            {
                // Get customer information
                Customer customer = new Customer();
                customer.customerName = customer.GetLastCusName();
                customer.customerCompany = txtCompany.Text;
                customer.customerName = txtName.Text;
                customer.customerCity = txtCity.SelectedValue.ToString();
                customer.customerProvince = txtProvince.SelectedValue.ToString();
                customer.telno = txtTelPhone.Text;
                customer.zipcode = txtPostalCode.Text;
                customer.address = txtAddress.Text;
                // Save customer ID into database 
                customer.customerID = tempBuffer;

                customer.Save();

                if (MessageBox.Show("Are you sure to continue?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff

                }
                else
                {
                    //do yes stuff
                    UserControl usc = null;
                    usc = new ShippingInfo();
                    GridOrder.Children.Add(usc);
                }                
            }            
		}

	}
}
