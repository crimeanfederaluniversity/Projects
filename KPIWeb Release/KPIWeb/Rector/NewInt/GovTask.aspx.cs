using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector.NewInt
{
    public partial class GovTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Button255_Click(object sender, EventArgs e)
        {
            Response.Redirect("science.pdf");
        }

        protected void Button256_Click(object sender, EventArgs e)
        {
            Response.Redirect("specpart.pdf");
        }
    }
}