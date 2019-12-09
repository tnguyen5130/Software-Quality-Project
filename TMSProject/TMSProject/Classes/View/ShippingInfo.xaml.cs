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
using TMSProject.Classes.Controller;
using TMSProject.Classes.Model;

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for ShippingInfo.xaml
	/// </summary>
	public partial class ShippingInfo : UserControl
	{
		public ShippingInfo()
		{
			InitializeComponent();
            fillFromToComboBox();
		}

        private void fillFromToComboBox()
        {
            CityBizDAO cityBiz = new CityBizDAO();
            cityBiz.getCityNameList(boxFrom);
            cityBiz.getCityNameList(boxTo);
        }

		public void btn_OK(object sender, RoutedEventArgs e)
		{

		}
	}
}
