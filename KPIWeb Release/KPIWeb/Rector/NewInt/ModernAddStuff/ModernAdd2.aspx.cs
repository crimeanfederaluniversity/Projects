﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector.NewInt.ModernAddStuff
{
    public partial class ModernAdd2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/NewInt/ModernAdd.aspx");
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
            
        }
    }
}