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

namespace TMSProject.Program
{
	/// <summary>
	/// Interaction logic for PlannerHome.xaml
	/// </summary>
	public partial class PlannerHome : UserControl
	{
		public PlannerHome()
		{
			InitializeComponent();
		}

		public void btn_new_Order(object sender, RoutedEventArgs e)
		{
			UserControl usc = null;
			GridMain.Children.Clear();

			// check if the order is exist or not

			// show dialog that user is sure to continue 

			// accept the order and open OrderAdd screen
			usc = new OrderAdd();
			GridMain.Children.Add(usc);
		}

		public void btn_Cancel(object sender, RoutedEventArgs e)
		{

		}
	}
}
