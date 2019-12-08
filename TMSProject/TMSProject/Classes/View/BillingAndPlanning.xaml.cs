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
    /// Interaction logic for BillingPage.xaml
    /// </summary>
    public partial class BillingAndPlanning : Page
    {
        IList<Mileage> selectedMileage = new List<Mileage>();

        public BillingAndPlanning(Order order)
        {
            InitializeComponent();
            var orderList = order.GetOrderDetailWithID(order.orderID);

            txtCustomerName.Text = orderList[0].customerName;
            txtOrderID.Text = orderList[0].orderID;
            txtStartCity.Text = orderList[0].startCityName;
            txtEndCity.Text = orderList[0].endCityName;

            Mileage mileage = new Mileage();

            mileage.startCityName = txtStartCity.Text;
            mileage.endCityName = txtEndCity.Text;

            var mileages = mileage.GetCitiesMileages(mileage.startCityName, mileage.endCityName);

            for (int i = 0; i < mileages.Count; i++)
            {
                selectedMileage.Add(new Mileage
                {
                    mileageID = mileages[i].mileageID,
                    startCityName = mileages[i].startCityName,
                    endCityName = mileages[i].endCityName,
                    distance = mileages[i].distance,
                    workingTime = mileages[i].workingTime
                });
            }

            resultDataGrid.ItemsSource = selectedMileage;

        }

        private void Load_Billing_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load_Planning_Click(object sender, RoutedEventArgs e)
        {
            Mileage mileage = new Mileage();

            mileage.startCityName = txtStartCity.Text;
            mileage.endCityName = txtEndCity.Text;

            var mileages = mileage.GetCitiesMileages(mileage.startCityName, mileage.endCityName);

            for (int i = 0; i < mileages.Count; i++)
            {
                selectedMileage.Add(new Mileage
                {
                    mileageID = mileages[i].mileageID,
                    startCityName = mileages[i].startCityName,
                    endCityName = mileages[i].endCityName,
                    distance = mileages[i].distance,
                    workingTime = mileages[i].workingTime
                });
            }

            resultDataGrid.ItemsSource = selectedMileage;
            //resultDataGrid.ItemsSource = mileage.GetCitiesMileages(mileage.startCityName, mileage.endCityName);
        }

        private void Change_Trip_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Trip_Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;

            //tripID
            Trip trip = new Trip();
            //get the cityID 
            City city = new City();
            var trips = trip.newTripID();

            //get the cityID 
            city.cityName = txtStartCity.Text;
            var startCityResult = city.GetCityName(city.cityName);
            trip.startCityID = startCityResult[0].cityID;
            city.cityName = txtEndCity.Text;

            var endCityResult = city.GetCityName(city.cityName);
            trip.endCityID = endCityResult[0].cityID;

            //retreive order data
            var viewTrips = trip.GetTripsST(trip.startCityID, trip.endCityID);

            for (int i = 0; i < viewTrips.Count; i++)
            {
                trip.tripID = trips[0].tripID;
                trip.orderID = txtOrderID.Text;
                trip.startCity = viewTrips[i].startCityID;
                trip.endCity = viewTrips[i].endCityID;
                //trip.startCityName = selectedMileage[i].startCityName;
                //trip.endCityName = selectedMileage[i].endCityName;

                if (viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].startCityID == viewTrips[i].originalCityID)
                {
                    trip.tripStatus = "loading";
                }
                else if ((viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].endCityID == viewTrips[i].desCityID))
                {
                    trip.tripStatus = "unloading";
                }
                else
                {
                    trip.tripStatus = "driving";
                }

                flag = trip.Save();

            }
        }

        private void Delete_Trip_Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (selectedMileage.Contains(Mileag(txtMileageID.Text.ToString()))
            {
                selectedMileage.Remove(txtMileageID.Text);
            }

            resultDataGrid.ItemsSource = selectedMileage;
            */
        }

        private void Confirm_Plan_Bill_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                IList rows = resultDataGrid.SelectedItems;

                Mileage mileage = new Mileage();
                mileage = (Mileage)rows[0];

                txtStartCityName.Text = mileage.startCityName;
                txtEndCityName.Text = mileage.endCityName;
                txtDistance.Text = mileage.distance.ToString();
                txtWorkingTime.Text = mileage.workingTime.ToString();
                txtStatus.Text = mileage.status;

                //OrderLineList orderline = new OrderLineList(order);
            }
        }

    }
}
