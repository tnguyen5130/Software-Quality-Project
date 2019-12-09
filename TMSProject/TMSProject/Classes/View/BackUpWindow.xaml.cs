using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using TMSProject.DBConnect;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for BackUpWindow.xaml
    /// </summary>
    public partial class BackUpWindow : UserControl
    {
        private string connectionString = "server=" + Configs.dbServer + ";user id=" + Configs.dbUID + ";password=" + Configs.dbPassword + ";database=" + Configs.dbDatabase + ";SslMode=none";

        string filenameDir = string.Empty;

        public BackUpWindow()
        {
            InitializeComponent();
            filenameDir = string.Empty;
        }

        private void btnBackUp_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {               
                File.WriteAllText(saveFileDialog.FileName, debugLog.Text);
            }
          
            string file = saveFileDialog.FileName;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(file);
                        conn.Close();
                    }
                }
            }
            
                
            MessageBox.Show("Back Up successful");
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            string file = currentPath.Text;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(file);
                        conn.Close();
                    }
                }
            }

            MessageBox.Show("Restore successful");
        }

        private void btnGetfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                debugLog.Text = File.ReadAllText(openFileDialog.FileName);
                filenameDir = openFileDialog.FileName;
                currentPath.Text = filenameDir;
            }
            btnRestore.Visibility = Visibility.Visible;
        }
    }
}
