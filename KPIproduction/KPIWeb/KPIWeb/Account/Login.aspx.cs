using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using KPIWeb.Models;
using WebApplication3;

namespace KPIWeb.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        /*    RegisterHyperLink.NavigateUrl = "Register";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }*/
        }

        protected void LogIn(object sender, EventArgs e)
        { // UserName.Text, Password.Text

            Users user;
            List<Users> listFirstStages = DataBaseCommunicator.GetUsersTable();
            user = DataBaseCommunicator.Get_User(listFirstStages, UserName.Text, Password.Text);

           if (IsValid)
            {
               
                if (user != null)
                {
                    Session["sessionKPI"] = user;
                    Response.Redirect("WebForm1.aspx");
              
                }
                else
                {
                    FailureText.Text = "Неверное имя пользователя или пароль.";
                    ErrorMessage.Visible = true;
                }

            }
        }
    }
}