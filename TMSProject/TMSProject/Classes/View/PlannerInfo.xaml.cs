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
	/// Interaction logic for PlannerInfo.xaml
	/// </summary>
	public partial class PlannerInfo : UserControl
	{
		public PlannerInfo()
		{
			InitializeComponent();
            loadBillingInfo();
        }

        private void loadBillingInfo()
        {
            Order order = new Order();
            var customer = new Customer().GetById(order.orderID);

            NameTBlock.Text = customer.customerName;
            CompanyTBlock.Text = customer.customerCompany;
            PhoneTBlock.Text = customer.telno;
            AddressTBlock.Text = customer.address;
            CityTBlock.Text = customer.customerCity;
            ProvinceTBlock.Text = customer.customerProvince;
            PostalCodeTBlock.Text = customer.zipcode;
        }
    }
}
