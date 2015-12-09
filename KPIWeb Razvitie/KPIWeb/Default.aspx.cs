using System;
using System.Collections.Generic;
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

            int accessLevel = (int)user.AccessLevel;
            if (accessLevel == 10)
            {
                Response.Redirect("~/AutomationDepartment/Main.aspx");
            }
            else if (accessLevel == 9)
            {
                Response.Redirect("~/StatisticsDepartment/MonitoringMain.aspx");
            }
            else if (accessLevel == 5)
            {
                Response.Redirect("~/Rector/RectorMain.aspx");
            }
            else if (accessLevel == 3)
            {
                Response.Redirect("~/FinKadr/OtdelChooseReport.aspx");
            }
            else if (accessLevel == 2)
            {
                Response.Redirect("~/Decan/DecMain.aspx");
            }
            else if (accessLevel == 4)
            {
                Response.Redirect("~/Director/DMain.aspx");
            }
            else if (accessLevel == 7)
            {
                Response.Redirect("~/Rector/RMain.aspx");
            }
            else if (accessLevel == 8)
            {
                Response.Redirect("~/Head/HeadMain.aspx");
            }
            else if (accessLevel == 0)
            {
                Response.Redirect("~/Reports_/ChooseReport.aspx");
            }
            else if (accessLevel == 1)
            {
                Response.Redirect("~/Reports_/ChooseReport.aspx");
            }
            else //если входим сюда то что то не так) скорей всего пользователю не присвоен уровень в UsersTable
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/UserLogin.aspx");
            }
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