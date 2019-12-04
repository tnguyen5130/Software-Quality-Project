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
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for ShippingInfo.xaml
	/// </summary>
	public partial class ShippingInfo : UserControl
	{
		public ShippingInfo(string OrderID, string OrderDate)
		{
			InitializeComponent();
            fillFromToComboBox();

            txtOrderID.Text = OrderID;
            txtOrderDate.Text = OrderDate;
		}

        private void fillFromToComboBox()
        {
            CityBizDAO cityBiz = new CityBizDAO();
            cityBiz.getCityNameList(boxFrom);
            cityBiz.getCityNameList(boxTo);
        }

		public void btn_OK(object sender, RoutedEventArgs e)
		{
            Order order = new Order();
            // OrderID
            order.orderID = txtOrderID.Text;
            // ContractID
            Contract contract = new Contract();
            order.contractID = contract.GetLastId();
            // Customer ID
            Customer customer = new Customer();            
            order.customerID = customer.GetLastCusID(); 
            // Order Date
            order.orderDate = txtOrderDate.Text;
            // Original City ID
            order.originalCityID = boxFrom.SelectedItem.ToString();
            // Destination CIty ID
            order.desCityID = boxTo.SelectedItem.ToString();
            order.command = "INSERT";
            // Order Status
            order.orderStatus = "PENDING";
            // Quantity
            order.quantity = txtPallet.Text;
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

            if (MessageBox.Show("Confirm the Order?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff

            }
            else
            {
                // Save to DB
                order.Save();
            }
		}
	}
}
