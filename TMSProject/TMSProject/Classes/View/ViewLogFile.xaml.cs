using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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


namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for ViewLogFile.xaml
    /// </summary>
    public partial class ViewLogFile : UserControl
    {
        string filenameDir = string.Empty;

        public ViewLogFile()
        {
            InitializeComponent();
            filenameDir = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                debugLog.Text = File.ReadAllText(openFileDialog.FileName);
                filenameDir = openFileDialog.FileName;
                currentPath.Text = filenameDir;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, debugLog.Text);


        }
    }
}
