using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class _Default : Page
    {
        UsersTable user;

        public void Directions(UsersTable user)
        {
            if (user.Position == null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
            }
            else if (user.Position.Length > 2)
            {
                FormsAuthentication.SetAuthCookie(user.Position, true);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
            }

            UserRights userRights = new UserRights();
            if (userRights.CanUserSeeThisPage(user.UsersTableID, 8, 0, 0))
            {
                Response.Redirect("~/Decan/DecMain.aspx");
            }

            if (userRights.CanUserSeeThisPage(user.UsersTableID, 7, 0, 0))
            {
                Response.Redirect("~/Director/DMain.aspx");
            }

            if (userRights.CanUserSeeThisPage(user.UsersTableID, 9, 0, 0))
            {
                Response.Redirect("~/Reports_/ChooseReport.aspx");
            }



            //если входим сюда то что то не так) скорей всего пользователю не присвоен уровень в UsersTable
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/UserLogin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/UserLogin.aspx");
            }
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                               where usersTables.UsersTableID == UserSer.Id
                               select usersTables).FirstOrDefault();
            if (user != null)
            {
                List<MultiUser> MultiuserList = (from a in KPIWebDataContext.MultiUser
                                                 where a.Active == true
                                                 && a.FK_UserCanAccess == user.UsersTableID
                                                 select a).ToList();
                if (MultiuserList.Count()>0)
                {
                    Response.Redirect("~/MultiUser.aspx");
                }
                Directions(user);
                
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/UserLogin.aspx");
            }
        }
    }
}