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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        IList<DataGridTrips> selectedTrips = new List<DataGridTrips>();

        public UserControl1(Order order)
        {
            InitializeComponent();
            var orderList = order.GetOrderDetailWithID(order.orderID);

            txtCustomerName.Text = orderList[0].customerName;
            txtOrderID.Text = orderList[0].orderID;
            txtStartCity.Text = orderList[0].startCityName.ToString();
            txtEndCity.Text = orderList[0].endCityName.ToString();


            City city = new City();
            Trip trip = new Trip();

            //1. get the startCityID 
            city.cityName = txtStartCity.Text;
            var startCityResult = city.GetCityName(city.cityName);
            trip.startCityID = startCityResult[0].cityID;

            //2. get the endCityID
            city.cityName = txtEndCity.Text;
            var endCityResult = city.GetCityName(city.cityName);
            trip.endCityID = endCityResult[0].cityID;

            //retreive order data
            var viewTrips = trip.GetTripsST(trip.startCityID, trip.endCityID);

            for (int i = 0; i < viewTrips.Count; i++)
            {
                trip.orderID = txtOrderID.Text;
                trip.startCity = viewTrips[i].startCityID;
                trip.endCity = viewTrips[i].endCityID;

                if (viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].startCityID == viewTrips[i].originalCityID)
                {
                    viewTrips[i].tripStatus = "loading";
                }
                else if ((viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].endCityID == viewTrips[i].desCityID))
                {
                    viewTrips[i].tripStatus = "unloading";
                }
                else
                {
                    viewTrips[i].tripStatus = "driving";
                }

                selectedTrips.Add(new DataGridTrips
                {
                    TranNo = (i + 1).ToString(),
                    startCityName = viewTrips[i].startCityName,
                    endCityName = viewTrips[i].endCityName,
                    distance = viewTrips[i].distance,
                    workingTime = viewTrips[i].workingTime,
                    tripStatus = viewTrips[i].tripStatus
                }); ;

            }

            resultDataGrid.ItemsSource = selectedTrips;
            resultDataGrid.Items.Refresh();

        }

        private void Load_Billing_Btn_Click(object sender, RoutedEventArgs e)
        {
            //get Shipping Route
            Trip trip = new Trip();
            trip.orderID = billingOrderID.Text;
            var billingTripData = trip.GetTripBilling(trip.orderID);

            trip.carrierID = billingTripData[0].carrierID;
            
            var shippingRoute = trip.GetShowTripsForBillings(trip.orderID, trip.carrierID);
            //assign values for DataGrid
            IList<DataGridRoute> selectedTrips = new List<DataGridRoute>();

            for (int i = 0; i < shippingRoute.Count; i++)
            {
                trip.tripID = shippingRoute[i].tripID;
                trip.startCityName = shippingRoute[i].startCityName;
                trip.endCityName = shippingRoute[i].endCityName;
                trip.distance = shippingRoute[i].distance;
                trip.workingTime = shippingRoute[i].workingTime;

                //FTL
                if (billingJobType.Text == "FTL")
                {
                    trip.price = 4.985 * shippingRoute[i].distance +
                                            ((4.985 * shippingRoute[i].distance) * shippingRoute[i].ftlRate / 100);
                    trip.rate = shippingRoute[i].ftlRate;

                    billingTotalAmount.Text = (Convert.ToDouble(billingTotalAmount.Text) + trip.price).ToString();
                }
                //LTL
                else if(billingJobType.Text =="LTL")
                {
                    trip.price = 0.2995 * shippingRoute[i].quantity * shippingRoute[i].distance +
                                            ((0.2995 * shippingRoute[i].quantity * shippingRoute[i].distance) * shippingRoute[i].ltlRate / 100);
                    trip.rate = shippingRoute[i].ltlRate;
                    billingTotalAmount.Text = (Convert.ToDouble(billingTotalAmount.Text) + trip.price).ToString();
                }

                selectedTrips.Add(new DataGridRoute
                {
                    tripID = shippingRoute[i].tripID,
                    startCityName = shippingRoute[i].startCityName,
                    endCityName = shippingRoute[i].endCityName,
                    distance = shippingRoute[i].distance,
                    workingTime = shippingRoute[i].workingTime,
                    tripStatus = shippingRoute[i].tripStatus,
                    rate = trip.rate,
                    price = trip.price
                });
            }

            billingDataGrid.ItemsSource = selectedTrips;
            billingDataGrid.Items.Refresh();
        }

        private void Load_Planning_Click(object sender, RoutedEventArgs e)
        {
            selectedTrips = new List<DataGridTrips>();

            City city = new City();
            Trip trip = new Trip();

            //1. get the startCityID 
            city.cityName = txtStartCity.Text;
            var startCityResult = city.GetCityName(city.cityName);
            trip.startCityID = startCityResult[0].cityID;

            //2. get the endCityID
            city.cityName = txtEndCity.Text;
            var endCityResult = city.GetCityName(city.cityName);
            trip.endCityID = endCityResult[0].cityID;

            //retreive order data
            var viewTrips = trip.GetTripsST(trip.startCityID, trip.endCityID);

            for (int i = 0; i < viewTrips.Count; i++)
            {
                trip.orderID = txtOrderID.Text;
                trip.startCity = viewTrips[i].startCityID;
                trip.endCity = viewTrips[i].endCityID;

                if (viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].startCityID == viewTrips[i].originalCityID)
                {
                    viewTrips[i].tripStatus = "loading";
                }
                else if ((viewTrips[i].startCityID == viewTrips[i].endCityID &&
                    viewTrips[i].endCityID == viewTrips[i].desCityID))
                {
                    viewTrips[i].tripStatus = "unloading";
                }
                else
                {
                    viewTrips[i].tripStatus = "driving";
                }

                selectedTrips.Add(new DataGridTrips
                {
                    TranNo = (i + 1).ToString(),
                    startCityName = viewTrips[i].startCityName,
                    endCityName = viewTrips[i].endCityName,
                    distance = viewTrips[i].distance,
                    workingTime = viewTrips[i].workingTime,
                    tripStatus = viewTrips[i].tripStatus
                }); ;
            }
        }
        private void Change_Trip_Button_Click(object sender, RoutedEventArgs e)
        {   /*
            for (int i = 0; i < selectedTrips.Count; i++)
            {
                if (selectedTrips[i].TranNo == txtMileageID.Text)
                {
                    selectedTrips.RemoveAt(i);
                }
            }

            resultDataGrid.ItemsSource = selectedTrips;
            resultDataGrid.Items.Refresh();
            */
        }
        
        private void Insert_TripData(IList<DataGridTrips> selectedTrips)
        {
            bool flag = false;

            //tripID
            Trip trip = new Trip();
            //get the cityID 
            City city = new City();

            for (int i = 0; i < selectedTrips.Count; i++)
            {
                var trips = trip.newTripID();
                trip.tripID = trips[0].tripID;
                trip.orderID = txtOrderID.Text;

                //get startCityID
                city.cityName = selectedTrips[i].startCityName;
                var startCityResult = city.GetCityName(city.cityName);
                trip.startCityID = startCityResult[0].cityID;

                //get endCityID
                city.cityName = selectedTrips[i].endCityName;
                var endCityResult = city.GetCityName(city.cityName);
                trip.endCityID = endCityResult[0].cityID;
                trip.distance = selectedTrips[i].distance;
                trip.workingTime = selectedTrips[i].workingTime;
                trip.tripStatus = selectedTrips[i].tripStatus;

                //insert
                trip.command = "INSERT";

                flag = trip.Save();
            }

            if (flag == true)
            {
                MessageBox.Show("Trip is generated sucessfully.", "Create Trips");
                Save_Plan();
            }
            else
            {
                MessageBox.Show("Trip is not generated. You need to check this.", "Fail Create Trips");
            }
        }

        private void Save_Trip_Button_Click(object sender, RoutedEventArgs e)
        {
            //check if the same information with orderID in planinfo
            PlanInfo planinfo = new PlanInfo();
            var planList = planinfo.GetPlanID(txtOrderID.Text);
            if (planList.Count == 1)
            {
                MessageBox.Show("Trip's Data is alread existed. You need to check this.", "Duplication Trip Data"); 
            }
            else
            {
                Insert_TripData(selectedTrips);
            }
        }

        private void Save_Plan()
        {
            bool flag = false;
            //#### Make Plan
            //Planinfo 
            PlanInfo plan = new PlanInfo();
            var planResult = plan.GetPlanInfo(txtOrderID.Text);

            //set Order.carrierID with selected carrierID
            if (planResult[0] != null)
            {
                plan.command = "INSERT";

                //assign data from table
                plan.planID = "PLAN" + planResult[0].orderID;
                plan.orderID = planResult[0].orderID;
                plan.startCityID = planResult[0].startCityID;
                plan.endCityID = planResult[0].endCityID;
                plan.distance = planResult[0].distance;
                plan.workingTime = planResult[0].workingTime;

                flag = plan.Save();
            }
            else
            {
                flag = false;
            }

            //#### Check the plan
            if (flag == true)
            {
                MessageBox.Show("Plan is generated sucessfully.", "Create Plan");
                Save_Carrier_Availability();
            }
            else
            {
                MessageBox.Show("Plan is not generated. You need to check this.", "Fail Create Plans");
            }
        }

        private void Save_Carrier_Availability()
        {
            bool flag = false;

            Carrier carrier = new Carrier();
            Order order = new Order();

            carrier.orderID = txtOrderID.Text;
            var orderList = order.GetOrderDetailWithID(carrier.orderID);

            carrier.carrierID = orderList[0].carrierID;
            var carrierAvailabilityResult = carrier.GetAvailabilty(carrier.orderID, carrier.carrierID);

            //#### Make Plan
            //Planinfo 
            //set Order.carrierID with selected carrierID
            if (carrierAvailabilityResult[0] != null)
            {
                carrier.command = "UPDATE";

                //assign data from table
                carrier.carrierID = carrierAvailabilityResult[0].carrierID;
                carrier.depotCity = carrierAvailabilityResult[0].depotCity;
                carrier.carrierName = carrierAvailabilityResult[0].carrierName;
                carrier.jobType = carrierAvailabilityResult[0].jobType;
                carrier.quantity = carrierAvailabilityResult[0].quantity;
                carrier.vanType = carrierAvailabilityResult[0].vanType;
                carrier.ftlAvail = carrierAvailabilityResult[0].ftlAvail;
                carrier.ltlAvail = carrierAvailabilityResult[0].ltlAvail;
                carrier.ftlRate = carrierAvailabilityResult[0].ftlRate;
                carrier.ltlRate = carrierAvailabilityResult[0].ltlRate;
                carrier.reeferCharge = carrierAvailabilityResult[0].reeferCharge;
                //FTL
                if (carrier.jobType == 0)
                {
                    carrier.ftlAvail = carrier.ftlAvail - 1;
                }
                //LTL
                else
                {
                    carrier.ltlAvail = carrier.ltlAvail - carrier.quantity;
                }
            }

            flag = carrier.Save();

            //#### Check the carrierAvailability
            if (flag == true)
            {
                MessageBox.Show("Carrier Availability is update sucessfully.", "Update Carrier Availabitily.");
                Billing_Page_Setup(carrier.jobType, carrier.quantity, carrier.vanType);
            }
            else
            {
                MessageBox.Show("Carrier Availability is not updated. You need to check this.", "Update failed for Carrier Availability.");
            }
        }

        private void Billing_Page_Setup(int jobType, int quantity, int vanType)
        {
            billingCustomerName.Text = txtCustomerName.Text;
            billingOrderID.Text = txtOrderID.Text;
            
            if(jobType == 0)
            {
                billingJobType.Text ="FTL";
            }
            else
            {
                billingJobType.Text = "LTL";
            }

            billingQuality.Text =quantity.ToString();

            if (vanType == 0)
            {
                billingVanType.Text = "DRY";
            }
            else
            {
                billingVanType.Text = "ReeferCharge";
            }
        }

        private void Delete_Trip_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < selectedTrips.Count; i++)
            {
                if (selectedTrips[i].TranNo == txtMileageID.Text)
                {
                    selectedTrips.RemoveAt(i);
                }
            }

            resultDataGrid.ItemsSource = selectedTrips;
            resultDataGrid.Items.Refresh();

            txtMileageID.Text = "";
            txtStartCityName.Text = "";
            txtEndCityName.Text = "";
            txtDistance.Text = "";
            txtWorkingTime.Text = "";
            txtStatus.Text = "";
        }

        private void Confirm_Billing_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;

            //get planID with orderID 
            PlanInfo planinfo = new PlanInfo();
            planinfo.orderID = billingOrderID.Text;
            var planList = planinfo.GetPlanID(planinfo.orderID);
            planinfo.planID = planList[0].planID;

            //get Billing information
            Billing billing = new Billing();
            var billingResult = billing.GetBillings(planinfo.planID, planinfo.orderID);

            //set Order.carrierID with selected carrierID
            if (billingResult[0] != null)
            {
                billing.command = "INSERT";

                //assign data from table
                billing.billingID = "BILL" + billingResult[0].orderID;
                billing.orderID = billingResult[0].orderID;
                billing.planID = billingResult[0].planID;
                billing.customerID = billingResult[0].customerID;

                billing.jobType = billingResult[0].jobType;
                billing.quantity = billingResult[0].quantity;
                billing.vanType = billingResult[0].vanType;

                //FTL
                if (billing.jobType == 0)
                {
                    billing.totalAmount = 4.985 * billingResult[0].distance +
                                            ((4.985 * billingResult[0].distance) * billingResult[0].ftlRate / 100);

                }
                //LTL
                else
                {
                    billing.totalAmount = 0.2995 * billing.quantity * billingResult[0].distance +
                                            ((0.2995 * billing.quantity * billingResult[0].distance) * billing.ltlRate / 100);
                }
            }

            flag = billing.Save();

            //#### Check the carrierAvailability
            if (flag == true)
            {
                MessageBox.Show("Billing's Information is created sucessfully.", "Create Billing Information.");
                Confirm_Invoice(billing.planID, billing.orderID);
            }
            else
            {
                MessageBox.Show("Billing's Information is not created. You need to check this.", "Billing Information failed.");
            }
        }

        private void Confirm_Invoice(string planID, string orderID)
        {
            bool flag = false;

            //get Billing info
            Billing billing = new Billing();

            billing.orderID = orderID;
            billing.planID = planID;
            var billingResult = billing.GetBillingID(billing.planID, billing.orderID);
            billing.billingID = billingResult[0].billingID;

            //add Invoice
            Invoice invoice = new Invoice();
            invoice.billingID = billing.billingID;
            invoice.orderID = billing.orderID;
            var invoiceResult = invoice.GetInvoices(invoice.billingID, invoice.orderID);

            //set Order.carrierID with selected carrierID
            if (invoiceResult.Count != 0)
            {
                invoice.command = "INSERT";

                //assign data from table
                invoice.invoiceID = "IN" + invoiceResult[0].billingID;
                invoice.billingID = invoiceResult[0].billingID;
                invoice.contractID = invoiceResult[0].contractID;
                invoice.customerID = invoiceResult[0].customerID;
                invoice.completeStatus = "ISSUED";
                
                flag = invoice.Save();

                //#### Check the carrierAvailability
                if (flag == true)
                {
                    MessageBox.Show("Invoice's Data is created sucessfully.", "Create Invoice Information.");
                }
                else
                {
                    MessageBox.Show("Invoice's data is not created. You need to check this.", "It is failed for Invoice Information.");
                }
            }
            else
            {
                MessageBox.Show("Billing Data is not existed. You need to check this.", "There is no Billing Information.");
            }
        }


        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                IList rows = resultDataGrid.SelectedItems;

                DataGridTrips dTrip = new DataGridTrips();
                dTrip = (DataGridTrips)rows[0];

                txtMileageID.Text = dTrip.TranNo;
                txtStartCityName.Text = dTrip.startCityName;
                txtEndCityName.Text = dTrip.endCityName;
                txtDistance.Text = dTrip.distance.ToString();
                txtWorkingTime.Text = dTrip.workingTime.ToString();
                txtStatus.Text = dTrip.tripStatus;

            }
        }

    }
}
