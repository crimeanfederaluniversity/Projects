using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using Npgsql;

namespace Chancelerry
{ 
    public partial class _Default : Page
    {
       

        public void Directions(Users user)
        {
               FormsAuthentication.SetAuthCookie(user.Name, true);
               Response.Redirect("~/kanz/Dashboard.aspx"); 
        }

        protected void Page_Load(object sender, EventArgs e) 
        {
            var UserSer = Session["userID"];

            if (UserSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }

            ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

            Users user = (from u in dataContext.Users
                               where u.UserID == (int)UserSer
                               select u).FirstOrDefault();

            if (user != null)
            {
                Session["pageCntrl"] = 0;
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