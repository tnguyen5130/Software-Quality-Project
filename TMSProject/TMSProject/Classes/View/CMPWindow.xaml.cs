﻿using System;
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
        // Sequence number
        int seq = 1;
        // Object
        Contract contract;
        ContractMarketPlace cmp;

        public CMPWindow()
        {
            InitializeComponent();
            // Load the CMP Database
            LoadCMPList();            
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

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {  
            if (MessageBox.Show("Are you sure to continue?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                contract = new Contract();
                contract.startDate = contract.NewContractDate();
                contract.completeStatus = "UNPAID";
                contract.command = "INSERT";

                if (contract.contractID != contract.GetLastId() && contract.contractID != null || contract.GetLastId() == null)
                {                    
                    contract.contractID = contract.NewContractID(seq);
                }
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

                // Check if client name is missing
                if (txtClientName.Text != null)
                {
                    contract.initiateBy = txtClientName.Text;
                }
                if (txtEndDate.SelectedDate == null)
                {
                    MessageBox.Show("You must choose the End Date for the Contract");
                }
                else
                {
                    // Get the Date for the End Date
                    contract.endDate = txtEndDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                }

                // Save data into CMP
                cmp = new ContractMarketPlace();
                cmp.contractID = contract.contractID;
                cmp.jobType = txtJobType.Text;
                cmp.quantity = Convert.ToInt32(txtQuantity.Text);
                cmp.origin = txtOrigin.Text;
                cmp.destination = txtDestination.Text;
                cmp.vanType = txtVanType.Text;
                if (cmp.customerID != cmp.GetLastCusID() && cmp.customerID != null || cmp.GetLastCusID() == null)
                {
                    cmp.customerID = cmp.NewCustomerID(seq);
                    cmp.command = "INSERT";
                }                
                if (cmp.customerID == cmp.GetLastCusID() || cmp.customerID == null)
                {
                    cmp.command = "INSERT";
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

                //Convert value of JobType, Quantity and VanType
                if (cmp.jobType == "0")
                {
                    cmp.jobType = "FTL";
                }
                else if (cmp.jobType == "1")
                {
                    cmp.jobType = "LTL";
                }

                if (cmp.vanType == "0")
                {
                    cmp.vanType = "R";
                }
                else if (cmp.vanType == "1")
                {
                    cmp.vanType = "F";
                }

                // Save to Database
                contract.Save();
                cmp.Save();

                // Navigation To Add CustomerDetails
                UserControl usc = null;
                usc = new OrderAdd(txtClientName.Text, cmp.customerID);
                GridCMP.Children.Add(usc);
            }
        }
    }
}
