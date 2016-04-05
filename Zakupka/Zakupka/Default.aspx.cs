using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userSer = Session["userID"];

            if (userSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
            ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
            var user = (from u in zakupkaDB.Users
                        where u.userID == (int)userSer
                        select u).FirstOrDefault();

            if (user != null)
            {
                Response.Redirect("~/Event/EventPage.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}