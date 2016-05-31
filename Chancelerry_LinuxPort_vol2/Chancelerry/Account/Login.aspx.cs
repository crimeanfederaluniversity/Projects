﻿using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using Npgsql;

namespace Chancelerry.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
     
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
            
                ChancelerryDb datacontext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

                var user = (from u in datacontext.Users where 
                            u.Login == Email.Text && 
                            u.Password == Password.Text && 
                            u.Active
                            select u).FirstOrDefault();

                if (user != null)
                {
                    Session["userID"] = user.UserID;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    FailureText.Text = "Неверный адрес электронной почты или пароль.";
                    ErrorMessage.Visible = true;
                }


            }
        }
    }
}