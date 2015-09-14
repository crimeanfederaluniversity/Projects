using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions
{
    public partial class _Default : Page
    {
        public void Directions(UsersTable user)
        {
            FormsAuthentication.SetAuthCookie(user.Email, true);
            
            int accessLevel = (int)user.AccessLevel;
            if (accessLevel == 0)
            {
                Response.Redirect("~/User/UserMainPage.aspx");
            }
            else if (accessLevel == 10)
            {
                Response.Redirect("~/Admin/Main.aspx");
            }
            else if (accessLevel == 5)
            {
                Response.Redirect("~/Expert/Main.aspx");
            }
            else if (accessLevel == 15)
            {
                Response.Redirect("~/Curator/CuratorMainPage.aspx");
            }
            else //если входим сюда то что то не так) скорей всего пользователю не присвоен уровень в UsersTable
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["UserID"];
            if (userId == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            UsersTable user = (from usersTables in competitionDataBase.UsersTable
                               where usersTables.ID == (int)userId
                               select usersTables).FirstOrDefault();
            
            if (user != null)
            {
                Session["UserID"] = user.ID;
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