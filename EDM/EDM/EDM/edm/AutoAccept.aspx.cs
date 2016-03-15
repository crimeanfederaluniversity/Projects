﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class AutoAccept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ТОЛЬКО АДМИН!!
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            //////////////////////////////////////////////////

            EmailFuncs ef = new EmailFuncs();

            if (!Page.IsPostBack)
            {
                 ef.AutoAccept();
            }
        }
    }
}