using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for InvoiceWindow.xaml
	/// </summary>
	public partial class InvoiceWindow : UserControl
	{
		public InvoiceWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.IsEnabled = false;
				PrintDialog printDialog = new PrintDialog();
				if (printDialog.ShowDialog() == true)
				{
					printDialog.PrintVisual(print, "invoice");
				}
			}
			finally
			{
				this.IsEnabled = true;
			}
		}

        private void Invoice_Retreive_Click(object sender, RoutedEventArgs e)
        {
            Invoice invoice = new Invoice();
            invoice.invoiceID = txtInputInvoice.Text;

            var invoiceResult = invoice.GetInvoiceID(invoice.invoiceID);

            if(invoiceResult.Count != 0)
            {
                invoice.invoiceID = invoiceResult[0].invoiceID;
                invoice.billingID = invoiceResult[0].billingID;
                invoice.orderID = invoiceResult[0].orderID;

                ShippingDetail_Info(invoice.invoiceID, invoice.billingID, invoice.orderID);
            }
            else
            {
                MessageBox.Show("There is no Invoice Data. You need to check your invoice Data.", "Fail to Retrieve your invoice.");
            }

        }

        private void ShippingDetail_Info(string invoiceID, string billingID, string orderID)
        {
            //display invoice Details
            Invoice invoice = new Invoice();
            var invoiceResult = invoice.ViewInvoices(billingID, orderID);

            //assign values for DataGrid
            //assign Shipping Data
            IList<DataGridRoute> selectedTrips = new List<DataGridRoute>();

            //Display information
            txtOrderDate.Text = invoiceResult[0].orderDate;
            txtOrderID.Text = invoiceResult[0].orderID;
            txtInvoiceNo.Text = invoiceResult[0].invoiceID; ;
            txtStartEndCities.Text = invoiceResult[0].startCityName + "--" + invoiceResult[0].endCityName;

            txtCustomerName.Text = invoiceResult[0].customerName;
            txtCompanyName.Text = invoiceResult[0].customerCompany;
            txtAddress.Text = invoiceResult[0].address;
            txtLocation.Text = invoiceResult[0].customerCity + "," + invoiceResult[0].customerProvince;
            txtPostalCode.Text = invoiceResult[0].zipcode;

            if (invoiceResult[0].jobType.ToString()=="0")
            {
                txtJobType.Text = "FTL";
            }
            else
            {
                txtJobType.Text = "LTL";

            }

            //get Shipping Route
            Trip trip = new Trip();
            trip.orderID = orderID;
            var billingTripData = trip.GetTripBilling(trip.orderID);

            trip.carrierID = billingTripData[0].carrierID;
            var shippingRoute = trip.GetShowTripsForBillings(trip.orderID, trip.carrierID);

            for (int i = 0; i < shippingRoute.Count; i++)
            {
                trip.tripID = shippingRoute[i].tripID;
                trip.startCityName = shippingRoute[i].startCityName;
                trip.endCityName = shippingRoute[i].endCityName;
                trip.distance = shippingRoute[i].distance;
                trip.workingTime = shippingRoute[i].workingTime;

                //FTL
                if (txtJobType.Text == "FTL")
                {
                    trip.price = 4.985 * shippingRoute[i].distance +
                                            ((4.985 * shippingRoute[i].distance) * shippingRoute[i].ftlRate / 100);
                    trip.rate = shippingRoute[i].ftlRate;

                    invoiceTotalAmount.Text = (Convert.ToDouble(invoiceTotalAmount.Text) + trip.price).ToString();
                }
                //LTL
                else if (txtJobType.Text == "LTL")
                {
                    trip.price = 0.2995 * shippingRoute[i].quantity * shippingRoute[i].distance +
                                            ((0.2995 * shippingRoute[i].quantity * shippingRoute[i].distance) * shippingRoute[i].ltlRate / 100);
                    trip.rate = shippingRoute[i].ltlRate;
                    invoiceTotalAmount.Text = (Convert.ToDouble(invoiceTotalAmount.Text) + trip.price).ToString();
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

            invoiceTotalAmount.Text = "$ " + invoiceTotalAmount.Text;

            billingDataGrid.ItemsSource = selectedTrips;
            billingDataGrid.Items.Refresh();
        }

	}
}
