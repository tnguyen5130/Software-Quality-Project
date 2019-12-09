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

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for OrderSummary.xaml
    /// </summary>
    public partial class OrderSummary : UserControl
    {
        public OrderSummary()
        {
            InitializeComponent();

            IList<string> ListOfStatus = new List<string>();

            // list of provine in Canada
            ListOfStatus.Add("All");
            ListOfStatus.Add("Active");
            ListOfStatus.Add("Finish");


            All_Status.ItemsSource = ListOfStatus;
            All_Status.SelectedIndex = 0;
        }

        private void Load_Order_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
