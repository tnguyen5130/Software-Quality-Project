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

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for OrderSummary.xaml
    /// </summary>
    public partial class OrderSummary : UserControl
    {
        private static readonly log4net.ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public OrderSummary()
        {
            InitializeComponent();
            IList<string> ListOfStatus = new List<string>();

            // list of provine in Canada
            ListOfStatus.Add("ALL");
            ListOfStatus.Add("Active");
            ListOfStatus.Add("Finish");
            All_Status.ItemsSource = ListOfStatus;
            All_Status.SelectedIndex = 0;
        }

        private void Order_Summary_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            IList<DataGridOrder> viewGrids = new List<DataGridOrder>();

            order.orderStatus = All_Status.Text;
            //retrieve carrier data
            var orderResult = order.GetOrdersSummary(order.orderStatus, Start_Order_Date.Text, End_Order_Date.Text);

            for (int i = 0; i < orderResult.Count; i++)
            {
                viewGrids.Add(new DataGridOrder
                {
                    orderID = orderResult[i].orderID,
                    contractID = orderResult[i].contractID,
                    customerName = orderResult[i].customerName,
                    orderDate = orderResult[i].orderDate,
                    startCityName = orderResult[i].startCityName,
                    endCityName = orderResult[i].endCityName,
                    carrierName = orderResult[i].carrierName,
                    jobType = orderResult[i].jobType,
                    quantity = orderResult[i].quantity,
                    vanType = orderResult[i].vanType,
                });
            }

            resultDataGrid.ItemsSource = viewGrids;
        }

        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Order order = new Order();

            if (sender != null)
            {
                IList rows = resultDataGrid.SelectedItems;

                DataGridOrder dataGrid = new DataGridOrder();
                dataGrid = (DataGridOrder)rows[0];
                order.orderID = dataGrid.orderID;
                order.startCityName = dataGrid.startCityName;
                order.endCityName = dataGrid.endCityName;
            }

            //Go to the TripTrackter page
            UserControl usc = new TripTracker(order);
            GridMain.Children.Add(usc);
        }
    }
}