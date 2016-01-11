using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class Rstat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button222_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
    }
}