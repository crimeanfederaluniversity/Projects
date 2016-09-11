using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Registration.Account
{
    public partial class FinishPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Session["userid"];
                if(id == null)
            {
                Label1.Visible = true;
            }
            else
            {
                Label2.Visible = true;
            }
        }
    }
}