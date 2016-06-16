using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class ChooseCntrlTempl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ControlLink1_Click(object sender, EventArgs e)
        {
            Response.Redirect("cntrlTemplates/cntrl1.aspx");
        }

        protected void ControlLink2_Click(object sender, EventArgs e)
        {
            Response.Redirect("cntrlTemplates/cntrl2.aspx");
        }

        protected void ControlLink3_Click(object sender, EventArgs e)
        {
            Response.Redirect("cntrlTemplates/cntrl3.aspx");
        }

        protected void ControlLink4_Click(object sender, EventArgs e)
        {
            Response.Redirect("cntrlTemplates/cntrl4.aspx");
        }
    }
}