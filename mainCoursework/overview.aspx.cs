﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mainCoursework
{
    public partial class overview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var checkLogin = Convert.ToBoolean(Session["loggedState"]);
            if (checkLogin == false)
            {
                Server.Transfer("login.aspx", true);
            }
        }
    }
}