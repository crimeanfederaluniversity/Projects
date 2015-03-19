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
                int l_0 = user.FK_ZeroLevelSubdivisionTable == null ? 0 : (int)user.FK_ZeroLevelSubdivisionTable;
                int l_1 = user.FK_FirstLevelSubdivisionTable == null ? 0 : (int)user.FK_FirstLevelSubdivisionTable;
                int l_2 = user.FK_SecondLevelSubdivisionTable == null ? 0 : (int)user.FK_SecondLevelSubdivisionTable;
                int l_3 = user.FK_ThirdLevelSubdivisionTable == null ? 0 : (int)user.FK_ThirdLevelSubdivisionTable;
                int l_4 = user.FK_FourthLevelSubdivisionTable == null ? 0 : (int)user.FK_FourthLevelSubdivisionTable;
                int l_5 = user.FK_FifthLevelSubdivisionTable == null ? 0 : (int)user.FK_FifthLevelSubdivisionTable;
                int userLevel = 5;

                userLevel = l_5 == 0 ? 4 : userLevel;
                userLevel = l_4 == 0 ? 3 : userLevel;
                userLevel = l_3 == 0 ? 2 : userLevel;
                userLevel = l_2 == 0 ? 1 : userLevel;
                userLevel = l_1 == 0 ? 0 : userLevel;
                userLevel = l_0 == 0 ? -1 : userLevel;

                string tmp = "";
                //  if (userLevel == 0) tmp = (from a in KPIWebDataContext.ZeroLevelSubdivisionTable where a.ZeroLevelSubdivisionTableID == user.FK_ZeroLevelSubdivisionTable select a.Name).FirstOrDefault();
                if (userLevel == 0) tmp = (from zero in KPIWebDataContext.ZeroLevelSubdivisionTable where zero.ZeroLevelSubdivisionTableID == user.FK_ZeroLevelSubdivisionTable select zero.Name).FirstOrDefault();
                if (userLevel == 1) tmp = (from b in KPIWebDataContext.FirstLevelSubdivisionTable where b.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable select b.Name).FirstOrDefault();
                if (userLevel == 2) tmp = (from c in KPIWebDataContext.SecondLevelSubdivisionTable where c.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable select c.Name).FirstOrDefault();
                if (userLevel == 3) tmp = (from d in KPIWebDataContext.ThirdLevelSubdivisionTable where d.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable select d.Name).FirstOrDefault();
                if (userLevel == 4) tmp = (from f in KPIWebDataContext.FourthLevelSubdivisionTable where f.FourthLevelSubdivisionTableID == user.FK_FourthLevelSubdivisionTable select f.Name).FirstOrDefault();
                if (userLevel == 5) tmp = (from g in KPIWebDataContext.FifthLevelSubdivisionTable where g.FifthLevelSubdivisionTableID == user.FK_FifthLevelSubdivisionTable select g.Name).FirstOrDefault();

                FormsAuthentication.SetAuthCookie(user.Login+"\n\r"+tmp, true);
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
                    Response.Redirect("~/Head/HeadChooseReport.aspx");
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