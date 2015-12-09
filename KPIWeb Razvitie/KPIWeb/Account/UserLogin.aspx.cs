using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

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

        private bool tryMaster(string pass, int FirstID)
        {
            return false;
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
                            Response.Redirect("~/Default.aspx");
                        }
                        else if (tryMaster((string)Password.Text, (int)userTmp.FK_FirstLevelSubdivisionTable))
                        {
                            string MasterPassword = (string)Password.Text.Substring(0, 5) + "*****";
                            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0LN0: User " + userTmp.Email + " with masterkey " + MasterPassword + "login in from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
                            Serialization UserSerId = new Serialization(userTmp.UsersTableID);
                            Session["UserID"] = UserSerId;

                            IsMaster newMaster = new IsMaster();
                            newMaster.MPassword = MasterPassword;
                            Session["IsMaster"] = newMaster;
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