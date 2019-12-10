using log4net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for TripTrackerForOrder.xaml
    /// </summary>
    public partial class TripTracker : UserControl
    {
        private static readonly log4net.ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TripTracker(Order order)
        {
            InitializeComponent();
            IList<DataGridTracker> viewGrids = new List<DataGridTracker>();

            txtorderID.Text = order.orderID;
            StartCity.Text = order.startCityName;
            EndCity.Text = order.endCityName;

            //1. get the startCityID 
            City city = new City();
            var startCityResult = city.GetCityName(StartCity.Text);
            order.originalCityID = startCityResult[0].cityID;

            //2. get the endCityID
            var endCityResult = city.GetCityName(EndCity.Text);
            order.desCityID = endCityResult[0].cityID;

            var orderResult = order.GetTrackerTrip(order.orderID, order.originalCityID, order.desCityID);

            for (int i = 0; i < orderResult.Count; i++)
            {
                viewGrids.Add(new DataGridTracker
                {
                    customerName = orderResult[i].customerName,
                    carrierName = orderResult[i].carrierName,
                    orderDate = orderResult[i].orderDate,
                    startCityName = orderResult[i].startCityName,
                    endCityName = orderResult[i].endCityName,
                    orderStatus = orderResult[i].orderStatus,
                    tripStatus = orderResult[i].tripStatus,
                    startDate = orderResult[i].startDate,
                    endDate = orderResult[i].endDate,
                    tripComplete = orderResult[i].tripComplete,
                    jobType = orderResult[i].jobType,
                    quantity = orderResult[i].quantity,
                    vanType = orderResult[i].vanType,
                });
            }

            resultDataGrid.ItemsSource = viewGrids;

        }

        private void Trip_Tracker_Click(object sender, RoutedEventArgs e)
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

        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                IList rows = resultDataGrid.SelectedItems;
                Order order = new Order();
                DataGridOrder dataGrid = new DataGridOrder();

                dataGrid = (DataGridOrder)rows[0];

            }

        }
    }
}