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

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for BuyerWindow.xaml
	/// </summary>
	public partial class BuyerWindow : Window
	{
		public BuyerWindow()
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

        private void btn_Order_Begin(object sender, RoutedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            usc = new CMPWindow();
            GridMain.Children.Add(usc);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UserControl usc = null;
			GridMain.Children.Clear();

			switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
			{
				case "ItemOrder":
					usc = new CMPWindow();
					GridMain.Children.Add(usc);
					break;
				case "ItemExit":
					this.Close();
					break;
				default:
					break;
			}
		}
	}
}
