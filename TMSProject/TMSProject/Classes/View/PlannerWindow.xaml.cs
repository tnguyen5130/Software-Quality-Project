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
using System.Windows.Shapes;

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
			GridMain.Children.Clear();

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
				case "ItemInfo":
					usc = new PlannerInfo();
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
