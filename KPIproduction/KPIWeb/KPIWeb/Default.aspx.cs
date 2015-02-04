﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class _Default : Page
    {
        UsersTable user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UsersTable)Session["user"];

            if (user == null)
                Response.Redirect("~/Account/Login.aspx");
        }
    }
}