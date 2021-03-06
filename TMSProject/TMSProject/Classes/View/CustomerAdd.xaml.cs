﻿using System;
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
	public partial class CustomerAdd : UserControl
	{
        string tempBuffer = "";
        string currentStatus = "";
        string cmpOrigin = "";
        string cmpDestination = "";
        string cmpQuantity = "";
        string cmpJobType = "";
        string cmpVanType = "";

        // Object
        Order order;
        Customer customer;

        public CustomerAdd(string cusName, string cusID, string origin, string destination, string quantity, string jobType, string vanType, string status)
		{
			InitializeComponent();

            // Generate new orderID and OrderDate
            order = new Order();
            
            // Check if orderID exist or not, if not generate new OrderID
            if (order.orderID != order.GetLastId() && order.orderID != null || order.GetLastId() == null)
            {
                order.orderID = order.NewOrderID(1);
            }
            else if (order.orderID == order.GetLastId() || order.orderID == null)
            {
                // Get last orderID to string
                string buffer = order.GetLastId();
                // Get the last character in the last OrderID
                string last = buffer.Substring(buffer.Length - 3);
                // Convert it into INT
                int temp = Convert.ToInt32(last);
                // Add by 1
                temp += 1;
                //Delete the last character of the buffer
                string newBuffer = RemoveLastChar(buffer);
                // Add with new temp
                order.orderID = newBuffer + String.Format("{0:D3}", temp);
            }
            // Print to Screen (OrderID and OrderDate)
            order.orderDate = order.NewOrderDate();
            txtOrderID.Text = order.orderID;
            txtOrderDate.Text = order.orderDate;

            customer = new Customer();
            // Assign customerID & customer Name
            if (status == "NEW")
            {
                tempBuffer = cusID;
                txtName.Text = cusName;
                cmpOrigin = origin;
                cmpDestination = destination;
                cmpQuantity = quantity;
                cmpJobType = jobType;
                cmpVanType = vanType;
            }            

            // Check if status is EXIST - existing customer
            // Or NEW - new customer
            if (status == "EXIST")
            {
                // Append to each one
                txtName.Text = customer.GetCustomerDetailsOnly(cusName).customerName;
                txtAddress.Text = customer.GetCustomerDetailsOnly(cusName).address;
                txtCity.Text = customer.GetCustomerDetailsOnly(cusName).customerCity;
                txtPostalCode.Text = customer.GetCustomerDetailsOnly(cusName).zipcode;
                txtProvince.Text = customer.GetCustomerDetailsOnly(cusName).customerProvince;
                txtTelPhone.Text = customer.GetCustomerDetailsOnly(cusName).telno;
                txtCompany.Text = customer.GetCustomerDetailsOnly(cusName).customerCompany;
                
                btnUpdateCustomer.Visibility = Visibility.Visible;
                customer.customerName = txtName.Text;
                cmpOrigin = origin;
                cmpDestination = destination;
                cmpQuantity = quantity;
                cmpJobType = jobType;
                cmpVanType = vanType;
            }
            // Get the current status
            currentStatus = status;
        }

        /// \brief This method RemoveLastChar for ID 
        /// \details <b>Details</b>
        /// This method will remove the last three character of a string
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

        /// \brief This method btn_Order_Add 
        /// \details <b>Details</b>
        /// This method will be a trigger for adding new customer details
        /// \return  string
		public void btn_To_ShippingInfo(object sender, RoutedEventArgs e)
		{           
            if (currentStatus == "EXIST")
            {
                if (MessageBox.Show("Are you sure to continue?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    // Navigation to Shipping Info to change order details
                    UserControl usc = null;
                    usc = new ShippingInfo(txtOrderID.Text, txtOrderDate.Text, txtName.Text, cmpOrigin, cmpDestination, cmpQuantity, cmpJobType, cmpVanType);
                    GridOrder.Children.Add(usc);
                }
            }
            else if (currentStatus == "NEW")
            {
                if (validationOrderAdd())
                {
                    // Get customer information
                    customer = new Customer();
                    // Name
                    //customer.customerName = customer.GetLastCusName();                
                    customer.customerName = txtName.Text;
                    // Company
                    customer.customerCompany = txtCompany.Text;
                    // City
                    customer.customerCity = txtCity.SelectedValue.ToString();
                    // Province
                    customer.customerProvince = txtProvince.SelectedValue.ToString();
                    // Phone Number
                    customer.telno = txtTelPhone.Text;
                    // Postal Code
                    customer.zipcode = txtPostalCode.Text;
                    // Address
                    customer.address = txtAddress.Text;
                    // Save customer ID into database 
                    customer.customerID = tempBuffer;
                    // Command
                    customer.command = "INSERT";

                    if (MessageBox.Show("Are you sure to continue?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff
                    }
                    else
                    {
                        //do yes stuff
                        customer.Save();
                        // Navigation to Shipping Info to change order details
                        UserControl usc = null;
                        usc = new ShippingInfo(txtOrderID.Text, txtOrderDate.Text, txtName.Text, cmpOrigin, cmpDestination, cmpQuantity, cmpJobType, cmpVanType);
                        GridOrder.Children.Add(usc);
                    }
                }
            }
		}

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (validationOrderAdd())
            {
                
                // Company
                customer.customerCompany = txtCompany.Text;
                // City
                customer.customerCity = txtCity.SelectedValue.ToString();
                // Province
                customer.customerProvince = txtProvince.SelectedValue.ToString();
                // Phone Number
                customer.telno = txtTelPhone.Text;
                // Postal Code
                customer.zipcode = txtPostalCode.Text;
                // Address
                customer.address = txtAddress.Text;
                // Command
                customer.command = "UPDATE";

                if (MessageBox.Show("Are you sure to continue?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    //do yes stuff
                    customer.Save();
                    MessageBox.Show("Updated Successful");
                }
            }         
        }
    }
}
