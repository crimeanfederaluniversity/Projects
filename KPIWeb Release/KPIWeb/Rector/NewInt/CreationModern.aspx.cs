using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector.NewInt
{
    public partial class CreationModern : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/NewInt/SvedofResult.aspx");
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}