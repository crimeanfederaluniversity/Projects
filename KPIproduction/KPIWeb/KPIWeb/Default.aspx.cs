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

                int accessLevel = (int) user.AccessLevel;
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
                else if ( accessLevel == 0)
                {
                    Response.Redirect("~/Reports/ChooseReport.aspx");
                }
                else //если входим сюда то что то не так) скорей всего пользователю не присвоен уровень в UsersTable
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    Response.Redirect("~/Account/UserLogin.aspx");
                }
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