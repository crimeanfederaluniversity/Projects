using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonalPages;

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
            if (userRights.CanUserSeeThisPage(user.UsersTableID, 1, 0, 0))
            {
                Response.Redirect("~/AutomationDepartment/Main.aspx");
            }

            if (userRights.CanUserSeeThisPage(user.UsersTableID, 0, 2, 0))
            {
                Response.Redirect("~/StatisticsDepartment/MonitoringMain.aspx");
            }                   
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                Session["UserID"] = UserSerId;
            }
            // автологин
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("http://cfu-portal.ru");
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
                Response.Redirect("http://cfu-portal.ru");
            }
        }
    }
}