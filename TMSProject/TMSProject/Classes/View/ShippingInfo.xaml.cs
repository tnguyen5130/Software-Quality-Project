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
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for ShippingInfo.xaml
	/// </summary>
	public partial class ShippingInfo : UserControl
	{
        string currentStatus = "";
        string currentCustomerName = "";
        public ShippingInfo(string OrderID, string OrderDate,string customerName, string origin, string destination, string quantity, string jobType, string vanType)
		{
			InitializeComponent();
            currentCustomerName = customerName;
            // Choose between change the order details or not
            if (MessageBox.Show("EDIT ORDER DETAILS?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                currentStatus = "NOT CHANGE";
                txtOrderID.Text = OrderID;
                txtOrderDate.Text = OrderDate;
                boxFrom.Visibility = Visibility.Hidden;
                boxTo.Visibility = Visibility.Hidden;
                txtOriginalCity.Text = origin;
                txtDestinationCity.Text = destination;
                if (jobType == "0")
                {
                    boxFTL.IsChecked = true;
                    boxLTL.Visibility = Visibility.Hidden;
                }
                else if (jobType == "1")
                {
                    boxLTL.IsChecked = true;
                    boxFTL.Visibility = Visibility.Hidden;
                }
                txtVanType.Text = vanType;
                txtPallet.Text = quantity;
            }
            else
            {
                currentStatus = "CHANGE";
                // do yes stuff
                fillFromToComboBox();
                // Print to screen
                txtOrderID.Text = OrderID;
                txtOrderDate.Text = OrderDate;
            }
        }

        /// \brief This method fillFromToComboBox
        /// \details <b>Details</b>
        /// This method will fill the data into Combobox for City details
        /// \return  string
        private void fillFromToComboBox()
        {
            CityBizDAO cityBiz = new CityBizDAO();
            // Fill in the combo box
            cityBiz.GetCityNameList(boxFrom);
            cityBiz.GetCityNameList(boxTo);
        }

        /// \brief This method RemoveLastChar for ID 
        /// \details <b>Details</b>
        /// This method will remove the last character of a string
        /// \return  string
        private string RemoveLastChar(string str)
        {
            return str.Substring(0, str.Length - 3);
        }

        public void btn_Send_Order(object sender, RoutedEventArgs e)
		{
            if (currentStatus == "CHANGE")
            {
                Order order = new Order();
                // Job Type
                if (boxFTL.IsChecked ?? false)
                {
                    order.jobType = 0;
                }
                else if (boxLTL.IsChecked ?? false)
                {
                    order.jobType = 1;
                }
                else
                {
                    MessageBox.Show("No Job Type Selection!!!");
                }

                if (txtVanType.Text == "")
                {
                    MessageBox.Show("Please insert van type");
                }
                else if (txtPallet.Text == "")
                {
                    MessageBox.Show("Please insert pallet");
                }

                if ((boxFTL.IsChecked ?? false) || (boxLTL.IsChecked ?? false))
                {
                    // OrderID
                    order.orderID = txtOrderID.Text;
                    // ContractID
                    Contract contract = new Contract();
                    order.contractID = contract.GetLastId();
                    // Customer ID
                    Customer customer = new Customer();
                    order.customerID = customer.GetLastCusID();
                    // Quantity
                    order.quantity = Convert.ToInt32(txtPallet.Text);
                    // Van Type
                    order.vanType = Convert.ToInt32(txtVanType.Text);
                    // Order Date
                    order.orderDate = txtOrderDate.Text;
                    // Original City ID            
                    order.originalCityID = order.GetOriginalID(boxFrom.SelectedItem.ToString());
                    // Destination City ID
                    order.desCityID = order.GetDestinateID(boxTo.SelectedItem.ToString());
                    // Command
                    order.command = "INSERT";
                    // Order Status
                    order.orderStatus = "ACTIVE";                    
                    // CarrierID
                    Carrier carrier = new Carrier();
                    order.carrierID = carrier.GetCarrierIDbyDepotCity(order.originalCityID);                    

                    if (MessageBox.Show("Confirm the Order?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff

                    }
                    else
                    {
                        // Save to DB
                        order.Save();
                        MessageBox.Show("Order Successful\nOrder Details:\nOrderID:" + order.orderID + "\nOrder Date: " + order.orderDate + "\nFrom: " + boxFrom.SelectedItem.ToString() + "\nTo: " + boxTo.SelectedItem.ToString());
                    }
                }
            }
            else if (currentStatus == "NOT CHANGE")
            {
                Order order = new Order();
                // OrderID
                order.orderID = txtOrderID.Text;
                // ContractID
                ContractMarketPlace cmp = new ContractMarketPlace();
                order.contractID = cmp.GetContractIDbyCustomerName(currentCustomerName);
                // Customer ID
                Customer customer = new Customer();
                order.customerID = customer.GetCustomerIDbyName(currentCustomerName);
                // Quantity
                order.quantity = Convert.ToInt32(txtPallet.Text);
                // Van Type
                order.vanType = Convert.ToInt32(txtVanType.Text);
                // Order Date
                order.orderDate = txtOrderDate.Text;
                // Original City ID            
                order.originalCityID = order.GetOriginalID(txtOriginalCity.Text);
                // Destination City ID
                order.desCityID = order.GetDestinateID(txtDestinationCity.Text);
                // Command
                order.command = "INSERT";
                // Order Status
                order.orderStatus = "ACTIVE";
                // CarrierID
                Carrier carrier = new Carrier();
                order.carrierID = carrier.GetCarrierIDbyDepotCity(order.originalCityID);

                if (MessageBox.Show("Confirm the Order?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff

                }
                else
                {
                    // Save to DB
                    order.Save();
                    MessageBox.Show("Order Successful\nOrder Details:\nOrderID:" + order.orderID + "\nOrder Date: " + order.orderDate + "\nFrom: " + txtOriginalCity.Text + "\nTo: " + txtDestinationCity.Text);
                }
            }
		}

	}
}
