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
using System.Data.SqlClient;
using MaterialDesignThemes.Wpf;
using System.Data;
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;
using MySql.Data.MySqlClient;
using System.Configuration;
using TMSProject.DBConnect;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for TMSAlter.xaml
    /// </summary>
    public partial class TMSAlter : UserControl
    {
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";
        Carrier carrier;
        Mileage mileage;
        string tempCarrierID = "";
        string tempMileageID = "";

        public TMSAlter()
        {
            InitializeComponent();
            carrier = new Carrier();
            mileage = new Mileage();
        }

        private void Load_Carrier_Button_Click(object sender, RoutedEventArgs e)
        {
            // We call LOAD_CARRIER sql function to load all the order we have
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // My Query
                const string query = @"LOAD_CARRIER";
                using (MySqlCommand cmdSel = new MySqlCommand(query, connection))
                {
                    DataTable dt = new DataTable();

                    cmdSel.CommandType = System.Data.CommandType.StoredProcedure;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);

                    Carrier_Data.ItemsSource = dt.DefaultView;
                }
                connection.Close();
            }
        }

        private void Load_Route_Button_Click(object sender, RoutedEventArgs e)
        {
            // We call LOAD_MILEAGE sql function to load all the order we have
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // My Query
                const string query = @"LOAD_MILEAGE";
                using (MySqlCommand cmdSel = new MySqlCommand(query, connection))
                {
                    DataTable dt = new DataTable();

                    cmdSel.CommandType = System.Data.CommandType.StoredProcedure;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);

                    Route_data.ItemsSource = dt.DefaultView;
                }
                connection.Close();
            }
        }

        private void Insert_Carrier_Button_Click(object sender, RoutedEventArgs e)
        {
            carrier.
        }

        private void Update_Carrier_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Carrier_Button_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Update_Route_Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Insert_Route_Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Delete_Route_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Carrier_Data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedItemRow = grid.SelectedItem as DataRowView;
            if (selectedItemRow != null)
            {
                tempCarrierID = selectedItemRow["carrierName"].ToString();
                txtCompanyName.Text = selectedItemRow["carrierName"].ToString();
                txtDepotCity.Text = selectedItemRow["depotCity"].ToString();
                txtFtlAvail.Text = selectedItemRow["ftlAvail"].ToString();
                txtLtlAvail.Text = selectedItemRow["ltlAvail"].ToString();
                txtFtlRate.Text = selectedItemRow["ftlRate"].ToString();
                txtLtlRate.Text = selectedItemRow["ltlRate"].ToString();
                txtReeferCharge.Text = selectedItemRow["reeferCharge"].ToString();
            }
        }

        private void Load_Route_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedItemRow = grid.SelectedItem as DataRowView;
            if (selectedItemRow != null)
            {
                txtStart_City.Text = selectedItemRow["startCityID"].ToString();
                txtEnd_City.Text = selectedItemRow["endCityID"].ToString();
                txtDistance.Text = selectedItemRow["distance"].ToString();
                txtWorking_Time.Text = selectedItemRow["workingTime"].ToString();
            }
        }
    }
}
