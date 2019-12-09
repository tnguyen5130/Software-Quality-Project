using log4net;
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
        private static readonly log4net.ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            IList<DataGridCarrier> viewGrids = new List<DataGridCarrier>();

            //retrieve carrier data
            var carrierResult = carrier.GetCarriers(StartCity.Text);

            for (int i = 0; i < carrierResult.Count; i++)
            {
                viewGrids.Add(new DataGridCarrier
                {
                    carrierID = carrierResult[i].carrierID,
                    carrierName = carrierResult[i].carrierName,
                    depotCityName = StartCity.Text,
                    ftlAvail = carrierResult[i].ftlAvail.ToString("0.##"),
                    ltlAvail = carrierResult[i].ltlAvail.ToString("0.##"),
                    ftlRate = carrierResult[i].ftlRate.ToString("0.##"),
                    ltlRate = carrierResult[i].ltlRate.ToString("0.##"),
                    reeferCharge = carrierResult[i].reeferCharge.ToString("0.##"),
                });
            }

            resultDataGrid.ItemsSource = viewGrids;
        }

        private void Confirm_Carrier_Btn_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Order order = new Order();
            order.orderID = Order.Text;
            order.carrierID = CarrierID.Text;
            order.command = "UPDATE";

            if (MessageBox.Show("Do you want to select this carrier company?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                flag = order.Save();
               
                if(flag==true)
                {
                    MessageBox.Show("Carreir selected successfully.", "Select Carrier");
                    //Open the Planning & Billing page
                    BillingAndPlanning OP = new BillingAndPlanning(order);
                    
                    UserControl usc = new BillingAndPlanning(order);
                    GridMain.Children.Add(usc);
                   
                }
                else
                {
                    MessageBox.Show("Carreir didn't select. You need to check this.", "Fail Select Carrier");
                }
            }
            else
            {
                MessageBox.Show("1111Carreir didn't select. You need to check this.", "Fail Select Carrier");
            }
        }

        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                IList rows = resultDataGrid.SelectedItems;
                Carrier carrier = new Carrier();
                DataGridCarrier dataGrid = new DataGridCarrier();

                dataGrid = (DataGridCarrier)rows[0];

                if (JobType.Text == "0")
                {
                    Rate.Text = dataGrid.ftlRate;
                    Availability.Text = dataGrid.ftlAvail;
                }
                else
                {
                    Rate.Text = dataGrid.ltlRate;
                    Availability.Text = dataGrid.ltlAvail;
                }

                CompanyName.Text = dataGrid.carrierName;
                RefeerChanger.Text = dataGrid.reeferCharge;
                CarrierID.Text = dataGrid.carrierID;
            }
        }
    }
}
