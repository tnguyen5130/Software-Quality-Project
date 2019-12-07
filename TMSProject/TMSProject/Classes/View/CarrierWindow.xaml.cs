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

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for CarrierWindow.xaml
    /// </summary>
    public partial class CarrierWindow : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Sample;Integrated Security=true;");
        SqlCommand cmd;
        public CarrierWindow()
        {
            InitializeComponent();
            displayData();
        }
        int ID = 0;
        private void Button_Insert(object sender, RoutedEventArgs e)
        {

            if (Text_Name.Text != "" && Text_DCity.Text != "")
            {
                cmd = new SqlCommand("insert into products(carrierName,depotCity,ftlAvail, ltlAvail, ftlRate, ltlRate, reeferCharge)" +
                    " values(@depotCity, @carrierName, @ftlAvail, @ltlAvail, @ftlRate, @ltlRate, @reeferCharge)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@carrierName", Text_Name.Text);
                cmd.Parameters.AddWithValue("@depotCity", Text_DCity.Text);
                cmd.Parameters.AddWithValue("@ftlAvail", Text_ftlA.Text);
                cmd.Parameters.AddWithValue("@ltlAvail", Text_ltlA.Text);
                cmd.Parameters.AddWithValue("@ftlRate", Text_ftlR.Text);
                cmd.Parameters.AddWithValue("@ltlRate", Text_ltlR.Text);
                cmd.Parameters.AddWithValue("@reeferCharge", charge.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                displayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }


        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            if (Text_Name.Text != "" && Text_DCity.Text != "")
            {
                cmd = new SqlCommand("update into products(carrierName,depotCity,ftlAvail, ltlAvail, ftlRate, ltlRate, reeferCharge)" +
                    " values(@depotCity, @carrierName, @ftlAvail, @ltlAvail, @ftlRate, @ltlRate, @reeferCharge)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@carrierID", ID);
                cmd.Parameters.AddWithValue("@carrierName", Text_Name.Text);
                cmd.Parameters.AddWithValue("@depotCity", Text_DCity.Text);
                cmd.Parameters.AddWithValue("@ftlAvail", Text_ftlA.Text);
                cmd.Parameters.AddWithValue("@ltlAvail", Text_ltlA.Text);
                cmd.Parameters.AddWithValue("@ftlRate", Text_ftlR.Text);
                cmd.Parameters.AddWithValue("@ltlRate", Text_ltlR.Text);
                cmd.Parameters.AddWithValue("@reeferCharge", charge.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Updated Successfully");
                displayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }

        }
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete products where carrierID = @carrierID;", con);
                con.Open();
                cmd.Parameters.AddWithValue("@carrierID", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                displayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }

        }
         
        //Clear Data  
        private void ClearData()
        {
            Text_Name.Text = "";
            Text_DCity.Text = "";
            Text_ftlA.Text = "";
            Text_ltlA.Text = "";
            Text_ftlR.Text = "";
            Text_ltlR.Text = "";
            charge.Text = "";
            ID = 0;
        }


        private void displayData()
        {
            CarrierBizDAO carrierBiz = new CarrierBizDAO();
            carrierBiz.displayData(Data_grid);
            

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedItemRow = grid.SelectedItem as DataRowView;
            if (selectedItemRow != null)
            {
                Text_Name.Text = selectedItemRow["carrierName"].ToString();
                Text_DCity.Text = selectedItemRow["depotCity"].ToString();
                Text_ftlA.Text = selectedItemRow["ftlAvail"].ToString();
                Text_ltlA.Text = selectedItemRow["ltlAvail"].ToString();
                Text_ftlR.Text = selectedItemRow["ftlRate"].ToString();
                Text_ltlR.Text = selectedItemRow["ltlRate"].ToString();
                charge.Text = selectedItemRow["reeferCharge"].ToString();
            }
        }
    }
}
