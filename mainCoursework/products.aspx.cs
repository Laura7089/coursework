﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
	public partial class products1 : System.Web.UI.Page
	{
		productList productsDisplayList = new productList();
		Panel[] panels;
		bool initialized = false;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (initialized)
			{
				populatePage();
			}
			else
			{
				productsDisplayList.importAll();
				populatePage();
				initialized = true;
			}
		}

		private void populatePage()
		{
			panels = productsDisplayList.generateControls();
			for (int i = 0; i < panels.Length; i++)
			{
				productsListPanel.Controls.Add(panels[i]);
			}
		}

		protected void detailsButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			Session["productRedirectName"] = btn.CommandArgument;
			Server.Transfer("~/productView.aspx", false);
		}
	}
}