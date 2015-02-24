using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using KPIWeb.Models;
using log4net;
using WebApplication3;
using Page = System.Web.UI.Page;

namespace KPIWeb.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {         
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer != null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {                    
            try
            {
                if (IsValid)
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                                       where usersTables.Login == UserName.Text &&
                                       usersTables.Password == Password.Text
                                       select usersTables).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Login, true);
                        LogHandler.LogWriter.WriteLog(LogCategory.INFO,"Пользователь " + user.Login + " вошел в систему "); 
                        Serialization UserSerId = new Serialization(user.UsersTableID);
                        Session["UserID"] = UserSerId;
                        Response.Redirect("~/Default.aspx");                   
                        }            
                        else
                        {
                            LogHandler.LogWriter.WriteLog(LogCategory.INFO,"Неудачная попытка авторизации " + user.Login); 
                            FailureText.Text = "Неверное имя пользователя или пароль.";
                            ErrorMessage.Visible = true;
                        }
                    }
                }
                catch(Exception ex)
                {
                    LogHandler.LogWriter.WriteError(ex);
                   
                }
            }
    }
}