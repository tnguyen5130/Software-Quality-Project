using System;
using System.Collections;
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
using TMSProject.Classes.View;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for PlannerInfo.xaml
    /// </summary>
    public partial class OrderDetails : UserControl
    {
        public OrderDetails(Order order)
        {   
            InitializeComponent();
            var cityNames = order.GetOrderWithID(order.orderID);
            order.startCityName = cityNames[0].startCityName;
            order.endCityName = cityNames[0].endCityName;

            var orderDetail = order.GetOrderDetail(order.startCityName, order.endCityName);
            CustomerName.Text = orderDetail[0].customerName;
            Order.Text = orderDetail[0].orderID;
            OrderDate.Text = orderDetail[0].orderDate;
            StartCity.Text = orderDetail[0].startCityName;
            EndCity.Text = orderDetail[0].endCityName;
            VanType.Text = orderDetail[0].vanType.ToString();
            JobType.Text = orderDetail[0].jobType.ToString();
            Quality.Text = orderDetail[0].quantity.ToString();

        }

        private void Load_Carrier_Btn_Click(object sender, RoutedEventArgs e)
        {
            Carrier carrier = new Carrier();
            resultDataGrid.ItemsSource = carrier.GetCarriers(StartCity.Text);

            Order order = new Order();
            order.orderID = Order.Text;
            UserControl usc = null;
            usc = new Test(order);
            //GridMain.Children.Add(usc);


            //GridMain.Children.Add(usc);

        }

        private void Confirm_Carrier_Btn_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Order order = new Order();
            order.orderID = Order.Text;
            order.command = "UPDATE";

            if (MessageBox.Show("Do you want to select this carrier company?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                flag = order.Save();

                if(flag==true)
                {
                    MessageBox.Show("Carreir selected successfully.", "Select Carrier");
                    //Open the Planning & Billing page
                    BillingAndPlanning OP = new BillingAndPlanning(order);
                    var host = new Window();
                    host.Content = OP;
                    host.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Carreir didn't select. You need to check this.", "Fail Select Carrier");
                }
            }
            else
            {
                // Stay in the same window
            }
        }

        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                IList rows = resultDataGrid.SelectedItems;
                Carrier carrier = new Carrier();
                Rate.Text = carrier.ltlRate.ToString();
                CompanyName.Text = carrier.carrierName;
                Availability.Text = carrier.ftlAvail.ToString();
                RefeerChanger.Text = carrier.reeferCharge.ToString();
            }
        }
    }
}
