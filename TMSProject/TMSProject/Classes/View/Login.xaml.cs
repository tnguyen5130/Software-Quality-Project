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

using TMSProject.DBConnect;

namespace TMSProject.Classes.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

		private void Login_Click(object sender, RoutedEventArgs e)
		{
			string username = usernameBox.Text;
			string password = passwordBox.Password;
			// check if username textbox or password field empty
			if (username.Length == 0)
			{
				MessageBox.Show("Please enter your user ID");
				usernameBox.Focus();
			}
			else if(passwordBox.Password.ToString() == "")
			{
				MessageBox.Show("Please enter your password");
				passwordBox.Focus();
			}
			else
			{
				DBHandler db = new DBHandler();
				int status = 1; 
				if (BuyerRadioButton.IsChecked == true)
				{
					status = 0; // sign in as Buyer
				}
				else if (PlannerRadioButton.IsChecked == true)
				{
					status = 1; // default sign in as Planner
				}
                else if (AdminRadioButton.IsChecked == true)
                {
                    status = 2;
                }
				else
				{
					MessageBox.Show("Please choose to sign in as ...");
				}

				bool input = db.validate_login(username, password, status);
				if (input && status == 0) // login as Buyer
				{
					BuyerWindow buyer = new BuyerWindow();
					this.Visibility = Visibility.Collapsed; // hide window
					buyer.ShowDialog();
					this.Visibility = Visibility.Visible; // show window
				}
				else if (input && status == 1) // login as planner
				{
					PlannerWindow planner = new PlannerWindow();
					this.Visibility = Visibility.Collapsed; // hide window
					planner.ShowDialog();
					this.Visibility = Visibility.Visible; // show window
				}
                else if (input && status == 2)
                {

                }
				else 
				{
					MessageBox.Show("Incorrect Login Credentials");
				}
			}
		} // end Login_Click

        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
