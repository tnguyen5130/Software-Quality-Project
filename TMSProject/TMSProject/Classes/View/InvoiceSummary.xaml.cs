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
    /// Interaction logic for InvoiceSummary.xaml
    /// </summary>
    public partial class InvoiceSummary : UserControl
    {
        public InvoiceSummary()
        {
            InitializeComponent();

            IList<string> ListOfCarrierName = new List<string>();

            // list of provine in Canada
            ListOfCarrierName.Add("All");
            ListOfCarrierName.Add("Planet Express");
            ListOfCarrierName.Add("Schooner's");
            ListOfCarrierName.Add("Tillman Transport");
            ListOfCarrierName.Add("We Haul");


            AllCarrier.ItemsSource = ListOfCarrierName;
            AllCarrier.SelectedIndex = 0;
        }

        

        private void Load_Invoice_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
