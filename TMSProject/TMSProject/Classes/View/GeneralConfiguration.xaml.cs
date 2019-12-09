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
using TMSProject.DBConnect;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for GeneralConfiguration.xaml
    /// </summary>
    public partial class GeneralConfiguration : UserControl
    {
        CMPConfigs configsManager;
        public GeneralConfiguration()
        {
            InitializeComponent();
            FillConfigsData();            
        }

        private void FillConfigsData()
        {
            configsManager = new CMPConfigs();
            List<String> configurationDetails = new List<String>();
            configurationDetails = configsManager.CallConfiguration();
            for (int i = 0; i < configurationDetails.Count; i++)
            {
                txtDBServerCurr.Text = configurationDetails[0];
                txtPortCurr.Text = configurationDetails[1];
                txtUIDCurr.Text = configurationDetails[2];
                txtPassCurr.Text = configurationDetails[3];
                txtDBCurr.Text = configurationDetails[4];
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            configsManager = new CMPConfigs();
            configsManager.DbServer = txtDbServer.Text;
            configsManager.DbPort = txtPort.Text;
            configsManager.DbUID = txtUserID.Text;
            configsManager.DbPassword = txtPassword.Text;
            configsManager.DbDatabase = txtDatabase.Text;

            MessageBox.Show("UPDATE NEW CONFIGURATION DETAILS SUCCESSFUL");
            FillConfigsData();
        }
    }    
}
