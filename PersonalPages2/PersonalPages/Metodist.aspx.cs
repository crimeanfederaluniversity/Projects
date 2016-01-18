using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class Metodist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("DocsImport.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllStudGroups.aspx");
        }
    }
}