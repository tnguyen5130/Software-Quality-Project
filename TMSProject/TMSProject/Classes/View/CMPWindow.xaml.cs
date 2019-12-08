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
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;
using System.Data;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for CMPWindow.xaml
    /// </summary>
    public partial class CMPWindow : UserControl
    {
        // Object
        Contract contract;
        ContractMarketPlace cmp;
        Customer customer;
        string status = "";

        public CMPWindow()
        {
            InitializeComponent();
            // Load the CMP Database
            LoadCMPList();
            customer = new Customer();
            if (customer.GetCustomerIDbyName(txtClientName.Text) != null)
            {
                txtEndDate.Visibility = Visibility.Hidden;
            }
        }

        /// \brief This method LoadCMPList for user 
        /// \details <b>Details</b>
        /// This method will Load the CMP Data from DB and display into DataGrid
        /// \return  void
        private void LoadCMPList()
        {
            CMPBizDAO CMPBiz = new CMPBizDAO();
            CMPBiz.LoadCMPList(CMPList);
        }        

        /// \brief This method RemoveLastChar for ID 
        /// \details <b>Details</b>
        /// This method will remove the last three character of a string
        /// \return  string
        private string RemoveLastChar(string str)
        {
            return str.Substring(0, str.Length - 3);
        }

        /// \brief This method CMPList_SelectionChanged 
        /// \details <b>Details</b>
        /// This method will Get the selected value of the Data Grid
        /// \return  void
        private void CMPList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedItemRow = grid.SelectedItem as DataRowView;
            if(selectedItemRow != null)
            {
                txtClientName.Text = selectedItemRow["Client_Name"].ToString();
                txtJobType.Text = selectedItemRow["Job_Type"].ToString();
                txtDestination.Text = selectedItemRow["Destination"].ToString();
                txtOrigin.Text = selectedItemRow["Origin"].ToString();
                txtQuantity.Text = selectedItemRow["Quantity"].ToString();
                txtVanType.Text = selectedItemRow["Van_Type"].ToString();
            }          
        }

        /// \brief This method btn_Next_Click 
        /// \details <b>Details</b>
        /// This method will set up DB for get data from CMP table and insert into local DB
        /// \return  void
        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {  
            if (MessageBox.Show("Are you sure to continue?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {            
                
                // Check if it is existing customer
                if (customer.GetCustomerIDbyName(txtClientName.Text) != null)
                {
                    if (MessageBox.Show("Existing Customer Founded! Continue?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff
                    }
                    else
                    {
                        // do yes stuff
                        // Navigation To Add CustomerDetails

                        UserControl usc = null;
                        status = "EXIST";
                        usc = new CustomerAdd(txtClientName.Text, customer.GetCustomerIDbyName(txtClientName.Text), txtOrigin.Text, txtDestination.Text, txtQuantity.Text, txtJobType.Text, txtVanType.Text, status);
                        GridCMP.Children.Add(usc);
                    }
                }                
                else
                {
                    // if missing, show warning
                    if (txtEndDate.SelectedDate == null)
                    {
                        MessageBox.Show("You must choose the End Date for the Contract");
                    }
                    else
                    {
                        /* =============================================== CONTRACT ========================================================== */
                        contract = new Contract();
                        // Contract Start Date
                        contract.startDate = contract.NewContractDate();
                        // Contract completeStatus
                        contract.completeStatus = "UNPAID";
                        // Contract command
                        contract.command = "INSERT";
                        // Check if the contractID is exist, if not create a new contractID
                        if (contract.contractID != contract.GetLastId() && contract.contractID != null || contract.GetLastId() == null)
                        {
                            contract.contractID = contract.NewContractID(1);
                        }
                        // If exist, get the last contract ID and increase it by 1
                        else if (contract.contractID == contract.GetLastId() || contract.contractID == null)
                        {
                            string buffer = contract.GetLastId();
                            // Get the last character in the last OrderID
                            string last = buffer.Substring(buffer.Length - 3);
                            // Convert it into INT
                            int temp = Convert.ToInt32(last);
                            // Add by 1
                            temp += 1;
                            //Delete the last character of the buffer
                            string newBuffer = RemoveLastChar(buffer);
                            // Add with new temp
                            contract.contractID = newBuffer + String.Format("{0:D3}", temp);
                        }
                        // Get the Date for the End Date
                        contract.endDate = txtEndDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd");

                        // Check if client name is missing
                        if (txtClientName.Text != null)
                        {
                            contract.initiateBy = "BUYER";
                        }

                        /* =============================================== CONTRACT MARKET PLACE ========================================================== */
                        cmp = new ContractMarketPlace();

                        // CMP contractID
                        cmp.contractID = contract.contractID;

                        // CMP jobType
                        cmp.jobType = Convert.ToInt32(txtJobType.Text);

                        // CMP Quantity
                        cmp.quantity = Convert.ToInt32(txtQuantity.Text);

                        // CMP Origin
                        cmp.origin = txtOrigin.Text;

                        // CMP Destination
                        cmp.destination = txtDestination.Text;
                        // CMP VanType
                        cmp.vanType = Convert.ToInt32(txtVanType.Text);

                        // CMP CustomerID
                        if (cmp.customerID != cmp.GetLastCusID() && cmp.customerID != null || cmp.GetLastCusID() == null)
                        {
                            cmp.customerID = cmp.NewCustomerID(1);
                        }
                        else if (cmp.customerID == cmp.GetLastCusID() || cmp.customerID == null)
                        {
                            string buffer = cmp.GetLastCusID();
                            // Get the last character in the last OrderID
                            string last = buffer.Substring(buffer.Length - 3);
                            // Convert it into INT
                            int temp = Convert.ToInt32(last);
                            // Add by 1
                            temp += 1;
                            //Delete the last character of the buffer
                            string newBuffer = RemoveLastChar(buffer);
                            // Add with new temp
                            cmp.customerID = newBuffer + String.Format("{0:D3}", temp);
                        }

                        // CMP Command 
                        cmp.command = "INSERT";

                        // Save to Database (Contract and CMP Table)                    
                        cmp.Save();
                        contract.Save();

                        // Navigation To Add CustomerDetails
                        UserControl usc = null;
                        status = "NEW";
                        usc = new CustomerAdd(txtClientName.Text, cmp.customerID, txtOrigin.Text, txtDestination.Text, txtQuantity.Text, txtJobType.Text, txtVanType.Text, status);
                        GridCMP.Children.Add(usc);
                    }                  
                }               
            }
        }


    }
}
