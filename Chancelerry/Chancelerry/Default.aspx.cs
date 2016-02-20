using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chancelerry.Models;

namespace Chancelerry
{
    public partial class _Default : Page
    {
       

        public void Directions(User user)
        {
               FormsAuthentication.SetAuthCookie(user.name, true);
               Response.Redirect("~/kanz/Dashboard.aspx"); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var UserSer = Session["UserID"];

            if (UserSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }

            ChancelerryDBDataContext dataContext  = new ChancelerryDBDataContext();
            var user = (from u in dataContext.Users
                               where u.userID == (int)UserSer
                               select u).FirstOrDefault();

            if (user != null)
            {
                Directions(user);
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