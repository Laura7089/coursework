﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Web.UI;
using System.Data.OleDb;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		protected void submitCredentialsButton_Click(object sender, EventArgs e)
		{
			switch (attemptLogin(usernameBox.Text, passwordBox.Text, new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=main.accdb")))
			{
				case 0:
					returnLabel.Text = "Correct!";
					Session["loggedState"] = 1;
					Thread.Sleep(1000);
					Server.Transfer("Contact.aspx", true);
					break;
				case 1:
					returnLabel.Text = "The Username or Password is incorrect.";
					break;
			}
		}


		//Attempting login
		public int attemptLogin(string submittedUsername, string submittedPassword, OleDbConnection connection)
		{
			connection.Open();
			using (OleDbCommand getUserCredentials = new OleDbCommand(@"SELECT * FROM users WHERE username=@username AND password=@password", connection))
			{
				getUserCredentials.Parameters.AddWithValue("@username", submittedUsername);
				getUserCredentials.Parameters.AddWithValue("@password", submittedPassword);
				int check = Convert.ToInt32(getUserCredentials.ExecuteScalar());
				if (check > 0)
				{
					return 0;
				}
				else
				{
					return 1;
				}
			}

			//var acceptable = new List<loginCredentials>();
			//foreach (loginCredentials check in acceptable)
			//{
			//	if (submittedUsername == check.username)
			//	{
			//		if (submittedPassword == check.password)
			//		{
			//			return 0;
			//		}
			//		else
			//		{
			//			return 2;
			//		}
			//	}
			//	else
			//	{
			//		return 1;
			//	}
			//}
			//return 3;
		}
	}

	//Custom login credentials class for checking en masse
	//public class loginCredentials
	//{
	//	public string username;
	//	public string password;
	//	public int accessLevel;
	//}
}