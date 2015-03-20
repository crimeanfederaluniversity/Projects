﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                                       where
                                       (usersTables.Login == UserName.Text) || (usersTables.Email == UserName.Text)
                                        &&
                                       usersTables.Password == Password.Text &&
                                       usersTables.Active == true
                                       select usersTables).FirstOrDefault();
                    if (user != null)
                    {                     
                        LogHandler.LogWriter.WriteLog(LogCategory.INFO,"Пользователь " + user.Login + " вошел в систему "); 
                        Serialization UserSerId = new Serialization(user.UsersTableID);
                        Session["UserID"] = UserSerId;
                        Response.Redirect("~/Default.aspx");                   
                        }            
                        else
                        {
                            LogHandler.LogWriter.WriteLog(LogCategory.INFO,"Неудачная попытка авторизации " + user.Login); 
                            FailureText.Text = "Неверный адрес электронной почты или пароль.";
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