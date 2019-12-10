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
using System.Windows.Shapes;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for PlannerWindow.xaml
    /// </summary>
    public partial class PlannerWindow : Window
    {
        public PlannerWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            //Page page = null;
            GridMain.Children.Clear();

            Order order = new Order();
            order.orderID = "ORD20191120001";

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemOrder":
                    usc = new PlannerHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemInvoice":
                    usc = new InvoiceWindow();
                    GridMain.Children.Add(usc);
                    break;
                case "OrderSummary":
                    usc = new OrderSummary();
                    GridMain.Children.Add(usc);
                    break;
                case "InvoiceSummary":
                    //usc = new InvoiceSummary();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemExit":
                    //usc = new Test(order);

                    this.Close();
                    break;
                default:
                    break;
            }
        }
    }
}
