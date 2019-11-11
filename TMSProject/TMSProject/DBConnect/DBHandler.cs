using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Log4net Library
using log4net;
//MYSQL Library
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;

namespace TMSProject.DBConnect
{
    class DBHandler
    {
		//Declare an instance for log4net
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private MySqlConnection connection;

		// Constructor
		public DBHandler()
		{
			Initialize();
		}

		/*
		 * Function: Initialize
		 * Description: initial connection with connection string
		 * Input: none
		 * Return: none
		 */
		public void Initialize()
		{
			string connectionString = "SERVER=" + Configs.dbServer + ";" + "DATABASE=" +
			Configs.dbDatabase + ";" + "UID=" + Configs.dbUID + ";" + "PASSWORD=" + Configs.dbPassword + ";";

			connection = new MySqlConnection(connectionString);
			if (connection == null)
			{
				Log.Fatal("Connection Error.");
			}
			else
			{
				Log.Fatal("Connection success.");
			}
		}

		/*
		 * Function: OpenConnection
		 * Description: act as open connection statement
		 * Input: 
		 * Return: true if success - false if fail
		 */
		private bool OpenConnection()
		{
			try
			{
				connection.Open();
				return true;
			}
			catch (MySqlException ex)
			{
				//When handling errors, you can your application's response based 
				//on the error number.
				//The two most common error numbers when connecting are as follows:
				//0: Cannot connect to server.
				//1045: Invalid user name and/or password.
				switch (ex.Number)
				{
					case 0:
						Log.Fatal("Cannot connect to server.  Contact administrator.");
						break;

					case 1045:
						Log.Fatal("Invalid username/password, please try again.");
						break;
				}
				return false;
			}
		}

		/*
		* Function: CloseConnection
		 * Description: act as close connection statement
		 * Input: 
		 * Return: true if success - false if fail
		 */
		private bool CloseConnection()
		{
			try
			{
				connection.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				Log.Fatal(ex.Message);
				return false;
			}
		}

		/*
		* Function: Insert
		* Description: act as insert statement
		* Input: query stirng
		* Return: none
		*/
		public void Insert(string query)
		{
			//open connection
			if (this.OpenConnection() == true)
			{
				//create command and assign the query and connection from the constructor
				MySqlCommand cmd = new MySqlCommand(query, connection);
				//Execute command
				cmd.ExecuteNonQuery();
				//close connection
				this.CloseConnection();
			}
		}

		/*
	 * Function: Update
	 * Description: act as update statement
	 * Input: query stirng
	 * Return: none
	 */
		public void Update(string query)
		{
			//Open connection
			if (this.OpenConnection() == true)
			{
				//create command and assign the query and connection from the constructor
				MySqlCommand cmd = new MySqlCommand(query, connection);
				//Execute command
				cmd.ExecuteNonQuery();
				//close connection
				this.CloseConnection();
			}
		}

		/*
		 * Function: Delete
		 * Description: act as delete statement
		 * Input: query stirng
		 * Return: none
		 */
		public void Delete(string query)
		{
			// open connection
			if (this.OpenConnection() == true)
			{   
				//create command and assign the query and connection from the constructor
				MySqlCommand cmd = new MySqlCommand(query, connection);
				//Execute command
				cmd.ExecuteNonQuery();
				//close connection
				this.CloseConnection();
			}
		}

		/*
		 * Function: validate_login()
		 * Description: to validate and sign in as Buyer or Planner
		 *				1: sign in as Planner
		 *				0: sign in as Buyer
		 * Input:	username string
		 *			password string
		 *			status int
		 * Return:	true - login successful
		 *			false -login fail
		 */
		public bool validate_login(string username, string password, int status)
		{
			bool retCode = false;
			// Query for sign in as Planner
			string queryPlanner = "Select * from tms.admin where PlannerID=@user and PlannerPassword=@pass";
			// Query for sign in as Buyer
			string queryBuyer = "Select * from tms.admin where BuyerID=@user and BuyerPassword=@pass";
			// open connection
			if (this.OpenConnection() == true)
			{
				// Planner sign in
				if (status == 0) 
				{
					MySqlCommand cmd = new MySqlCommand(queryPlanner, connection);
					// Prepared statement
					cmd.Parameters.AddWithValue("@user", username);
					cmd.Parameters.AddWithValue("@pass", password);
					cmd.Prepare();
					cmd.Connection = connection;
					// Retrieve data from database and execute
					MySqlDataReader rdr = cmd.ExecuteReader();
					if (rdr.Read())
					{
						this.CloseConnection(); // close connection
						retCode = true;
					}
					else
					{
						this.CloseConnection(); // close connection
						retCode = false;
					}
				}
				else if (status == 1)
				{
					MySqlCommand cmd = new MySqlCommand(queryBuyer, connection);
					// Prepared statement
					cmd.Parameters.AddWithValue("@user", username);
					cmd.Parameters.AddWithValue("@pass", password);
					cmd.Prepare();
					cmd.Connection = connection;
					// Retrieve data from database and execute
					MySqlDataReader rdr = cmd.ExecuteReader();
					if (rdr.Read())
					{
						this.CloseConnection(); // close connection
						retCode = true;
					}
					else
					{
						this.CloseConnection(); // close connection
						retCode = false;
					}
				} // end if status
			} // end open connection
			return retCode;
		}



	}
}
