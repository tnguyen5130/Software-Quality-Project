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

        private void AutoLoadCarrierTable()
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

        private void AutoLoadMileageTable()
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

        /// \brief This method RemoveLastChar for ID 
        /// \details <b>Details</b>
        /// This method will remove the last character of a string
        /// \return  string
        private string RemoveLastChar(string str)
        {
            return str.Substring(0, str.Length - 3);
        }

        private void Insert_Carrier_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("ADD NEW CARRIER?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // do no stuff
            }
            else
            {
                // do yes stuff
                // Validation
                if (carrier.GetCarrierIDbyDepotCity(txtDepotCity.Text) != null)
                {
                    MessageBox.Show("Carrier exist, can not insert!");
                }
                else
                {
                    if (carrier.carrierID != carrier.GetLastCarrierID() && carrier.carrierID != null || carrier.GetLastCarrierID() == null)
                    {
                        carrier.carrierID = carrier.NewCarrierID(1);
                    }
                    else if (carrier.carrierID == carrier.GetLastCarrierID() || carrier.carrierID == null)
                    {
                        string buffer = carrier.GetLastCarrierID();
                        // Get the last character in the last OrderID
                        string last = buffer.Substring(buffer.Length - 3);
                        // Convert it into INT
                        int temp = Convert.ToInt32(last);
                        // Add by 1
                        temp += 1;
                        //Delete the last character of the buffer
                        string newBuffer = RemoveLastChar(buffer);
                        // Add with new temp
                        carrier.carrierID = newBuffer + String.Format("{0:D3}", temp);
                    }

                    carrier.depotCity = txtDepotCity.Text;
                    carrier.carrierName = txtCompanyName.Text;
                    carrier.ftlAvail = Convert.ToDouble(txtFtlAvail.Text);
                    carrier.ltlAvail = Convert.ToDouble(txtLtlAvail.Text);
                    carrier.ftlRate = Convert.ToDouble(txtFtlRate.Text);
                    carrier.ltlAvail = Convert.ToDouble(txtLtlRate.Text);
                    carrier.reeferCharge = Convert.ToDouble(txtReeferCharge.Text);

                    carrier.command = "INSERT";
                    carrier.Save();
                    MessageBox.Show("INSERT SUCCESSFUL");
                    ClearCarrierDetail();
                    AutoLoadCarrierTable();
                }
               
            }                
        }

        private void Update_Carrier_Button_Click(object sender, RoutedEventArgs e)
        {
            carrier.carrierID = tempCarrierID;
            carrier.depotCity = txtDepotCity.Text;
            carrier.carrierName = txtCompanyName.Text;
            carrier.ftlAvail = Convert.ToDouble(txtFtlAvail.Text);
            carrier.ltlAvail = Convert.ToDouble(txtLtlAvail.Text);
            carrier.ftlRate = Convert.ToDouble(txtFtlRate.Text);
            carrier.ltlAvail = Convert.ToDouble(txtLtlRate.Text);
            carrier.reeferCharge = Convert.ToDouble(txtReeferCharge.Text);

            carrier.command = "UPDATE";
            carrier.Save();
            MessageBox.Show("UPDATE SUCCESSFUL");
            ClearCarrierDetail();
            AutoLoadCarrierTable();
        }

        private void Delete_Carrier_Button_Click(object sender, RoutedEventArgs e)
        {
            tempCarrierID = carrier.carrierID;
            carrier.Delete();
            MessageBox.Show("DELETE SUCCESSFUL");
            ClearCarrierDetail();
            AutoLoadCarrierTable();
        }

        private void ClearCarrierDetail()
        {
            txtCompanyName.Text = "";
            txtDepotCity.Text = "";
            txtFtlAvail.Text = "";
            txtLtlAvail.Text = "";
            txtFtlRate.Text = "";
            txtLtlRate.Text = "";
            txtReeferCharge.Text = "";
        }

        private void ClearMileageDetail()
        {
            txtStart_City.Text = "";
            txtEnd_City.Text = "";
            txtDistance.Text = "";
            txtWorking_Time.Text = "";
        }


        private void Update_Route_Button_Click(object sender, RoutedEventArgs e)
        {
            mileage.mileageID = tempMileageID;
            mileage.startCityID = txtStart_City.Text;
            mileage.endCityID = txtEnd_City.Text;
            mileage.distance = Convert.ToDouble(txtDistance.Text);
            mileage.workingTime = Convert.ToDouble(txtWorking_Time.Text);
            mileage.command = "UPDATE";
            mileage.Save();
            MessageBox.Show("UPDATE SUCCESSFUL");
            ClearMileageDetail();
            AutoLoadMileageTable();
        }


        private void Insert_Route_Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("ADD NEW ROUTE?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // do no stuff
            }
            else
            {
                // do yes stuff
                if (mileage.mileageID != mileage.GetLastMileageID() && mileage.mileageID != null || mileage.GetLastMileageID() == null)
                {
                    mileage.mileageID = mileage.NewMileageID(1);
                }
                else if (mileage.mileageID == mileage.GetLastMileageID() || mileage.mileageID == null)
                {
                    string buffer = mileage.GetLastMileageID();
                    // Get the last character in the last OrderID
                    string last = buffer.Substring(buffer.Length - 3);
                    // Convert it into INT
                    int temp = Convert.ToInt32(last);
                    // Add by 1
                    temp += 1;
                    //Delete the last character of the buffer
                    string newBuffer = RemoveLastChar(buffer);
                    // Add with new temp
                    mileage.mileageID = newBuffer + String.Format("{0:D3}", temp);
                }
                mileage.startCityID = txtStart_City.Text;
                mileage.endCityID = txtEnd_City.Text;
                mileage.distance = Convert.ToDouble(txtDistance.Text);
                mileage.workingTime = Convert.ToDouble(txtWorking_Time.Text);
                mileage.command = "INSERT";
                mileage.Save();
                MessageBox.Show("INSERT SUCCESSFUL");
                ClearMileageDetail();
                AutoLoadMileageTable();
            }
        }


        private void Delete_Route_Button_Click(object sender, RoutedEventArgs e)
        {
            tempMileageID = mileage.mileageID;
            mileage.Delete();
            MessageBox.Show("DELETE SUCCESSFUL");
            ClearMileageDetail();
            AutoLoadMileageTable();
        }

        private void Carrier_Data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedItemRow = grid.SelectedItem as DataRowView;
            if (selectedItemRow != null)
            {
                tempCarrierID = selectedItemRow["carrierID"].ToString();
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
                tempMileageID = selectedItemRow["mileageID"].ToString();
                txtStart_City.Text = selectedItemRow["startCityID"].ToString();
                txtEnd_City.Text = selectedItemRow["endCityID"].ToString();
                txtDistance.Text = selectedItemRow["distance"].ToString();
                txtWorking_Time.Text = selectedItemRow["workingTime"].ToString();
            }
        }
    }
}
