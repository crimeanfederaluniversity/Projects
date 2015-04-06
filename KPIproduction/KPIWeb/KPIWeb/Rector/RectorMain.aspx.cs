using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RectorMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ParametrType paramType = new ParametrType(0);
            Session["paramType"] = paramType;
            Response.Redirect("~/Rector/RectorChooseReport.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            ParametrType paramType = new ParametrType(1);
            Session["paramType"] = paramType;
            Response.Redirect("~/Rector/RectorChooseReport.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/Documents.aspx");
        }
    }
}