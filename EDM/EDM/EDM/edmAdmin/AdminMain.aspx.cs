using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edmAdmin
{
    public partial class AdminMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void NewUserButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/CreateNewUser.aspx");
        }

        protected void WatchUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/EditUser.aspx");
        }

        protected void WatchStructure_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/StructureForm.aspx");
        }

        protected void WatchSubmitersClick(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/ChangeSubmiters.aspx");
        }
    }
}