using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            /////////////////////////////////////////////////////////////////////
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["direction"] = 1;
            Response.Redirect("Dashboard.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["direction"] = 0;
            Response.Redirect("Dashboard.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["direction"] = 2;
            Response.Redirect("Dashboard.aspx");
        }
    }
}