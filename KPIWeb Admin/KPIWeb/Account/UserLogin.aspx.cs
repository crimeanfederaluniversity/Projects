using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Web.Security;

namespace KPIWeb.Account
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer != null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
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

            if (userRights.CanUserSeeThisPage(user.UsersTableID, 19, 0, 0))
            {
                Response.Redirect("~/PersonalPagesAdmin/PersonalMainPage.aspx");
            }
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    UsersTable userTmp = (from usersTables in KPIWebDataContext.UsersTable
                                          where ((usersTables.Login == UserName.Text) || (usersTables.Email == UserName.Text))
                                          && usersTables.Active == true
                                          select usersTables).FirstOrDefault();
                    if (userTmp != null)
                    {

                        UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                                           where
                                           ((usersTables.Login == UserName.Text) || (usersTables.Email == UserName.Text))
                                            &&
                                           (usersTables.Password == Password.Text) &&
                                           usersTables.Active == true
                                           select usersTables).FirstOrDefault();
                        if (user != null)
                        {
                            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0LN0: User " + user.Email + " login in from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
                            Serialization UserSerId = new Serialization(user.UsersTableID);
                            Session["UserID"] = UserSerId;
                            Session["IsMaster"] = null;
                            Directions(user);
                            Response.Redirect("~/Default.aspx");
                        }

                        else
                        {
                            FailureText.Text = "Неверный адрес электронной почты или пароль.";
                            ErrorMessage.Visible = true;
                            // LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Неудачная попытка авторизации " + user.Login);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHandler.LogWriter.WriteError(ex);
            }

        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
        }

    }
}