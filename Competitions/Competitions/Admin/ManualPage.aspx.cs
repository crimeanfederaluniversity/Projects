using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ManualPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActionPRManual.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndicatorManual.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaskPRManual.aspx");
        }
    }
}