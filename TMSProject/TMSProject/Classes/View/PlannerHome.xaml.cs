using System;
using System.Collections.Generic;
using System.Data;
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
using TMSProject.DBConnect;

namespace TMSProject.Classes.View
{
	/// <summary>
	/// Interaction logic for PlannerHome.xaml
	/// </summary>
	public partial class PlannerHome : UserControl
	{
        string ItemSelect;

        public PlannerHome()
		{
			InitializeComponent();
            loadTableData();            
		}

        private void loadTableData()
        {
            OrderBizDAO orderBiz = new OrderBizDAO();
            orderBiz.loadOrderList(OrderList);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (OrderList.SelectedItem == null) return;
                DataRowView dr = OrderList.SelectedItem as DataRowView;
                DataRow newDr = dr.Row;
                //MessageBox.Show(Convert.ToString(newDr.ItemArray[1]));
                ItemSelect = Convert.ToString(newDr.ItemArray[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void btn_Confirm(object sender, RoutedEventArgs e)
		{
            txtMessage.Text = "Your OrderID selected is: " + ItemSelect;
		}

        public void btn_Proceed(object sender, RoutedEventArgs e)
        {

        }

        public void btn_Cancel(object sender, RoutedEventArgs e)
        {

        }
	}
}
