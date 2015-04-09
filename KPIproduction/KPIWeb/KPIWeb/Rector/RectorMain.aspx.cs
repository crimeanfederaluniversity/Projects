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
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
            "document.location = '../Rector/doc/1.pdf';", true);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/Document.aspx");
        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}